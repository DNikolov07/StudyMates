using StudyMates.ViewModels;

namespace StudyMates.Services.Interfaces
{
    public interface IGroupsService
    {
        Task<IEnumerable<StudyGroupViewModel>> GetAllAsync(string? userId);

        Task<StudyGroupViewModel?> GetByIdAsync(int id, string? userId);

        Task<IEnumerable<SubjectViewModel>> GetSubjectsAsync();

        Task CreateAsync(StudyGroupFormModel model, string creatorId);

        Task<StudyGroupFormModel?> GetForEditAsync(int id);

        Task<bool> EditAsync(int id, StudyGroupFormModel model);

        Task<bool> IsCreatorAsync(int id, string userId);

        Task<bool> DeleteAsync(int id);

        Task<bool> JoinAsync(int groupId, string userId);

        Task<bool> LeaveAsync(int groupId, string userId);
    }
}
