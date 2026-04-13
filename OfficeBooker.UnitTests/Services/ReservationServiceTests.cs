using Moq;
using Xunit;
using FluentAssertions;
using OfficeBooker.Services;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;
using OfficeBooker.Models.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace OfficeBooker.UnitTests
{
    public class ReservationServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<ReservationCreateDTO>> _validatorMock;
        private readonly Mock<UserManager<Worker>> _userManagerMock;
        private readonly ReservationService _reservationService;

        public ReservationServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<ReservationCreateDTO>>();
            var store = new Mock<IUserStore<Worker>>();
            _userManagerMock = new Mock<UserManager<Worker>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            _reservationService = new ReservationService(
                _unitOfWorkMock.Object,
                _userManagerMock.Object,
                _validatorMock.Object
            );
        }

        [Fact]
        public async Task Create_ShouldSucceed_WhenDataIsValid()
        {
            // Arrange
            var userId = "user-1";
            var dto = new ReservationCreateDTO
            {
                OfficeId = 1,
                ReservationStartTime = DateTime.Now.AddDays(20),
                ReservationEndTime = DateTime.Now.AddDays(20).AddHours(2)
            };

            _validatorMock.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new Worker { Id = userId });

            _unitOfWorkMock.Setup(u => u.officeRepository.Get(It.IsAny<Expression<Func<Office, bool>>>()))
                .ReturnsAsync(new Office { Id = 1, Capacity = 10 });

            _unitOfWorkMock.Setup(u => u.reservationRepository.IsReservationAvailable(It.IsAny<Reservation>()))
                .ReturnsAsync(true);

            // Act
            var result = await _reservationService.CreateReservationAsync(dto, userId);

            // Assert
            result.Should().NotBeNull();
            _unitOfWorkMock.Verify(u => u.reservationRepository.Add(It.IsAny<Reservation>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task GetById_ShouldThrowReservationNotFoundException_WhenNotExists()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.reservationRepository.Get(It.IsAny<Expression<Func<Reservation, bool>>>()))
                .ReturnsAsync((Reservation)null!);

            // Act 
            Func<Task> act = async () => await _reservationService.GetByIdAsync(999);

            // Assert
            await act.Should().ThrowAsync<ReservationNotFoundException>();
        }

        [Fact]
        public async Task Create_ShouldThrowOfficeAlreadyReservedException_WhenDatesOverlap()
        {
            // Arrange
            var dto = new ReservationCreateDTO
            {
                OfficeId = 1,
                ReservationStartTime = DateTime.Now.AddDays(1),
                ReservationEndTime = DateTime.Now.AddDays(1).AddHours(2)
            };

            _validatorMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());
            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new Worker());
            _unitOfWorkMock.Setup(u => u.officeRepository.Get(It.IsAny<Expression<Func<Office, bool>>>())).ReturnsAsync(new Office { Id = 1 });

            _unitOfWorkMock.Setup(u => u.reservationRepository.IsReservationAvailable(It.IsAny<Reservation>()))
                .ReturnsAsync(false);

            // Act
            Func<Task> act = async () => await _reservationService.CreateReservationAsync(dto, "user-id");

            // Assert
            await act.Should().ThrowAsync<OfficeAlreadyReservedException>();
        }

        [Fact]
        public async Task Delete_ShouldSucceed_WhenUserIsOwner()
        {
            // Arrange
            var userId = "user-1";
            var res = new Reservation { Id = 1, WorkerId = userId };
            _unitOfWorkMock.Setup(u => u.reservationRepository.Get(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(res);

            // Act
            await _reservationService.DeleteReservationAsync(1, userId);

            // Assert
            _unitOfWorkMock.Verify(u => u.reservationRepository.Remove(res), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }
    }
}