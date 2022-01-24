using AareonTechnicalTest.Exceptions;
using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using AareonTechnicalTest.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AareonTechnicalTest.Tests.Services
{
    public class NoteServiceTests
    {
        private readonly INoteService _sut;
        private readonly Mock<INoteRepository> _mockNoteRepository = new Mock<INoteRepository>();
        private readonly Mock<ITicketService> _mockTicketService = new Mock<ITicketService>();
        private readonly Mock<IPersonService> _mockPersonService = new Mock<IPersonService>();


        public NoteServiceTests()
        {
            _sut = new NoteService(_mockNoteRepository.Object, _mockTicketService.Object, _mockPersonService.Object);
        }

        #region Delete 

        [Fact]
        public async Task DeleteTicket_WhenTicketDoesNoteExist_ShouldThrowResourceNotFoundException()
        {
            _mockPersonService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Person());
            _mockTicketService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(default(Ticket));

            // Act + Assert
            await Assert.ThrowsAsync<ResourceNotFoundException>(async () => await _sut.DeleteAsync(1, 1));
        }

        [Fact]
        public async Task DeleteTicket_WhenPersonDoesNoteExist_ShouldThrowResourceNotFoundException()
        {
            _mockPersonService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(default(Person));

            // Act + Assert
            await Assert.ThrowsAsync<ResourceNotFoundException>(async () => await _sut.DeleteAsync(1, 1));
        }

        [Fact]
        public async Task DeleteTicket_WhenNotOwnerAndNotAdmin_ShouldThrowUserNotFoundException()
        {
            
            Person person = new Person() { Id = 1, IsAdmin = false };
            Note existingNote = new Note() { PersonId = 2 };

            _mockPersonService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(person);
            _mockTicketService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Ticket());
            _mockNoteRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(existingNote);

            // Act + Assert
            await Assert.ThrowsAsync<UserNotAuthorisedException>(async () => await _sut.DeleteAsync(1, 1));
        }

        [Fact]
        public async Task DeleteTicket_WhenPersonIsOwner_ShouldCallRemoveOnRepository()
        {
            Person person = new Person() { Id = 1, IsAdmin = false };
            Note existingNote = new Note() { PersonId = 1 };

            _mockPersonService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(person);
            _mockTicketService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Ticket());
            _mockNoteRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(existingNote);
            _mockNoteRepository.Setup(x => x.RemoveAsync(existingNote, true)).Verifiable();

            // Act
            await _sut.DeleteAsync(1, 1);

            // Assert
            _mockNoteRepository.Verify(x => x.RemoveAsync(existingNote, true), Times.Once);
        }

        [Fact]
        public async Task DeleteTicket_WhenAdmin_ShouldCallRemoveOnRepository()
        {
            Person person = new Person() { Id = 2, IsAdmin = true };
            Note existingNote = new Note() { PersonId = 1 };

            _mockPersonService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(person);
            _mockTicketService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Ticket());
            _mockNoteRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(existingNote);
            _mockNoteRepository.Setup(x => x.RemoveAsync(existingNote, true)).Verifiable();

            // Act
            await _sut.DeleteAsync(1, 1);

            // Assert
            _mockNoteRepository.Verify(x => x.RemoveAsync(existingNote, true), Times.Once);
        }

        #endregion

    }
}
