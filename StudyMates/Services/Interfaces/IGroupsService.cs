using StudyMates.ViewModels;

namespace StudyMates.Services.Interfaces
{
    public interface IGroupsService
    {
        Task<IEnumerable<StudyGroupViewModel>> GetAllAsync(string? userId);

        Task<StudyGroupViewModel?> GetByIdAsync(int id, string? userId);

        Task<IEnumerable<SubjectViewModel>> GetSubjectsAsync();

        Task CreateAsync(StudyGroupFormModel model, string creatorId);
    }
}
