using StudyMates.ViewModels;

namespace StudyMates.Services.Interfaces
{
    public interface ISessionService
    {
        Task<StudySessionListViewModel?> GetByGroupAsync(int groupId, string? userId);

        Task CreateAsync(int groupId, StudySessionFormModel model);

        Task<int?> GetGroupIdAsync(int sessionId);

        Task<bool> DeleteAsync(int id);
    }
}