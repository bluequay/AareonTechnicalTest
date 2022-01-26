using AareonTechnicalTest.Exceptions;
using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using AareonTechnicalTest.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AareonTechnicalTest.Tests
{
    public class TicketServiceTests
    {

        private readonly ITicketService _sut;
        private readonly Mock<ITicketRepository> mockTicketRepository = new Mock<ITicketRepository>();
        private readonly Mock<IPersonService> mockPersonService = new Mock<IPersonService>();

        public TicketServiceTests()
        {
            _sut = new TicketService(mockTicketRepository.Object, mockPersonService.Object);
        }

        #region Create Ticket

        [Fact]
        public async Task CreateTicket_IfPersonDoesNotExist_ShouldThrowResourceNotFoundException()
        {
            // Arrange 
            mockPersonService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(default(Person));

            // Act + Assert
            await Assert.ThrowsAsync<ResourceNotFoundException>(async() => await _sut.CreateAsync(new Ticket() { PersonId = 1 }));
        }

        [Fact]
        public async Task CreateTicket_IfPreConditionsPass_ShouldCallRespositorySave()
        {
            // Arrange 
            int personId = 1;
            Ticket ticket = new Ticket() { Content = "Test Content", PersonId = personId };

            mockPersonService.Setup(x => x.GetAsync(1)).ReturnsAsync(new Person());
            mockTicketRepository.Setup(x => x.AddAsync(ticket, true)).Verifiable();

            // Act
            await _sut.CreateAsync(ticket);

            // Assert
            mockTicketRepository.Verify(x => x.AddAsync(ticket, true), Times.Once);
        }

        #endregion

        #region Update Ticket

        [Fact]
        public async Task UpdateTicket_IfPersonDoesNotExist_ShouldThrowResourceNotFoundException()
        {
            // Arrange 
            mockPersonService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(default(Person));

            // Act + Assert
            await Assert.ThrowsAsync<ResourceNotFoundException>(async () => await _sut.UpdateAsync(1, new Ticket() { PersonId = 1 }));
        }

        [Fact]
        public async Task UpdateTicket_IfTicketDoesNotExist_ShouldBubbleResourceNotFoundException()
        {
            // Arrange 

            mockPersonService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Person());
            mockTicketRepository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Ticket>(), It.IsAny<bool>())).ThrowsAsync(new ResourceNotFoundException(""));

            // Act + Assert
            await Assert.ThrowsAsync<ResourceNotFoundException>(async () => await _sut.UpdateAsync(1, new Ticket() { PersonId = 1 }));
        }

        [Fact]
        public async Task UpdateTicket_IfPreConditionsPass_ShouldCallRespositoryUpdate()
        {
            // Arrange 
            int personId = 1;
            int ticketId = 2;
            Ticket ticket = new Ticket() { Content = "Test Content", PersonId = personId };

            mockPersonService.Setup(x => x.GetAsync(personId)).ReturnsAsync(new Person());
            mockTicketRepository.Setup(x => x.GetAsync(ticketId)).ReturnsAsync(new Ticket());
            mockTicketRepository.Setup(x => x.UpdateAsync(ticketId, ticket, true)).Verifiable();

            // Act
            await _sut.UpdateAsync(ticketId, ticket);

            // Assert
            mockTicketRepository.Verify(x => x.UpdateAsync(ticketId, ticket, true), Times.Once);
        }

        #endregion

        #region Delete Ticket

        [Fact]
        public async Task DeleteTicket_IfTicketDoesNotExist_ShouldThrowResourceNotFoundException()
        {
            // Arrange 
            mockTicketRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(default(Ticket));

            // Act + Assert
            await Assert.ThrowsAsync<ResourceNotFoundException>(async () => await _sut.DeleteAsync(1));
        }

        [Fact]
        public async Task DeleteTicket_IfPreConditionsPass_ShouldCallRespositoryRemove()
        {
            // Arrange 
            int ticketId = 1;
            Ticket ticket = new Ticket();

            mockTicketRepository.Setup(x => x.GetAsync(ticketId)).ReturnsAsync(ticket);

            mockTicketRepository.Setup(x => x.RemoveAsync(ticket, true)).Verifiable();

            // Act
            await _sut.DeleteAsync(ticketId);

            // Assert
            mockTicketRepository.Verify(x => x.RemoveAsync(ticket, true), Times.Once);
        }

        #endregion
    }
}
