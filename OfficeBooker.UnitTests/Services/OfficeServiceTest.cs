using FluentAssertions;
using Moq;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
using OfficeBooker.Services;
using System.Linq.Expressions;

namespace OfficeBooker.UnitTests.Services
{
    public class OfficeServiceTest
    {
        [Fact]
        public async Task GetById_WhenOfficeExists_ShouldReturnOffice()
        {
            // Arrange
            var officeId = 1;
            var fakeOffice = new Office { Id = officeId, OfficeNumber=999};

            var repoMock = new Mock<IOfficeRepository>();
            repoMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<Office, bool>>>()))
                    .ReturnsAsync(fakeOffice);

            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(u => u.officeRepository).Returns(repoMock.Object);

            var service = new OfficeService(uowMock.Object);

            // Act
            var result = await service.GetOfficeByIdAsync(officeId);

            // Assert
            result.Should().NotBeNull();
        }
    }
}