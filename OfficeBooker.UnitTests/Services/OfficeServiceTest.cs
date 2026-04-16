using FluentAssertions;
using FluentValidation;
using Moq;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;
using OfficeBooker.Services;
using System.Linq.Expressions;

namespace OfficeBooker.UnitTests.Services
{
    public class OfficeServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly OfficeService _officeService;
        private readonly Mock<IOfficeRepository> _officeRepositoryMock;
        private readonly Mock<IValidator<OfficeCreateDTO>> _validatorMock;
        public OfficeServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<OfficeCreateDTO>>();
            _officeService = new OfficeService(_unitOfWorkMock.Object, _validatorMock.Object);
            _officeRepositoryMock = new Mock<IOfficeRepository>();
            _unitOfWorkMock.Setup(u => u.officeRepository).Returns(_officeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetOfficeByIdAsync_WhenOfficeExists_ShouldReturnOffice()
        {
            // Arrange
            var officeId = 1;
            var fakeOffice = new Office { Id = officeId, OfficeNumber = 999 };
            _officeRepositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<Office, bool>>>()))
                    .ReturnsAsync(fakeOffice);
            _unitOfWorkMock.Setup(u => u.officeRepository).Returns(_officeRepositoryMock.Object);
            // Act
            var result = await _officeService.GetOfficeByIdAsync(officeId);

            // Assert
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task GetOfficeByIdAsync_WhenOfficeDoesntExists_ShuldThrowKeyNotFoundException()
        {
            //Arrange
            var officeId = 99999;
            //Act
            Func<Task> act = async () => await _officeService.GetOfficeByIdAsync(officeId);
            //Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task CreateOfficeAsync_ShouldSucced_WhenDataIsValid()
        {
            // Arrange
            var office = new OfficeCreateDTO
            {
                Capacity = 10,
                FloorNumber = 212,
                OfficeNumber = 999,
                Equipment = "Test Equipment"
            };
            _validatorMock.Setup(v => v.ValidateAsync(office, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            //Act 
            var respond = await _officeService.CreateOfficeAsync(office);
            // Assert
            _unitOfWorkMock.Verify(u => u.officeRepository.Add(It.IsAny<Office>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }
        [Fact]
        public async Task CreateOfficeAsync_ShouldThrowValidationException_WhenDataIsInvalid()
        {
            // Arrange
            var office = new OfficeCreateDTO
            {
                Capacity = 0,
                FloorNumber = 212,
                OfficeNumber = -12,
                Equipment = "Test Equipment"
            };
            var validationFailures = new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Capacity", "Capacity must be greater than 0."),
                new FluentValidation.Results.ValidationFailure("OfficeNumber", "OfficeNumber must be greater than 0.")
            };
            _validatorMock.Setup(v => v.ValidateAsync(office, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult(validationFailures));

            //Act 
            Func<Task> act = async () => await _officeService.CreateOfficeAsync(office);
            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("*Capacity must be greater than 0.*")
                .WithMessage("*OfficeNumber must be greater than 0.*");
        }
        [Fact]
        public async Task GetAllOfficesAsync_ShouldReturnAllOffices()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.officeRepository.GetAll(It.IsAny<Expression<Func<Office, bool>>>()))
                .ReturnsAsync(new List<Office>
                {
                    new Office { Id = 1, OfficeNumber = 999, Capacity = 10, FloorNumber = 212, Equipment = "Test Equipment" },
                    new Office { Id = 2, OfficeNumber = 998, Capacity = 8, FloorNumber = 211, Equipment = "Test Equipment" }
                });

            // Act
            var result = await _officeService.GetAllOfficesAsync();

            // Assert
            result.Should().HaveCount(2);
        }
        [Fact]
        public async Task DeleteOfficeAsync_ShouldSucced() { 
         //Arrange
        var fakeOffice = new Office
        {
            Id = 1,
            OfficeNumber = 999,
            Capacity = 10,
            FloorNumber = 212,
            Equipment = "Test Equipment"
        };
            _unitOfWorkMock.Setup(u=>u.officeRepository.Get(It.IsAny<Expression<Func<Office, bool>>>())).ReturnsAsync(fakeOffice);
        //Act
         await _officeService.DeleteOfficeAsync(1);
        //Assert
        _unitOfWorkMock.Verify(u => u.officeRepository.Remove(It.IsAny<Office>()), Times.Once);

        }
        [Fact]
        public async Task DeleteOfficeAsync_ShouldThrowKeyNotFoundException()
        {
            //Arrange
          var id = 1;
            //Act
            Func<Task> act = async () => await _officeService.DeleteOfficeAsync(id);
            //Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"*Office with ID {id} not found.*");

        }
    }
}