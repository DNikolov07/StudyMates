using Microsoft.EntityFrameworkCore;
using StudyMates.Data;
using StudyMates.Models;
using StudyMates.Services.Interfaces;
using StudyMates.ViewModels;

namespace StudyMates.Services
{
    public class SessionService : ISessionService
    {
        private readonly ApplicationDbContext context;

        public SessionService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<StudySessionListViewModel?> GetByGroupAsync(int groupId, string? userId)
        {
            return await context.StudyGroups
                .AsNoTracking()
                .Where(g => g.Id == groupId)
                .Select(g => new StudySessionListViewModel
                {
                    StudyGroupId = g.Id,
                    StudyGroupTitle = g.Title,
                    IsCreator = g.CreatorId == userId,
                    Sessions = g.Sessions
                        .OrderBy(s => s.StartsOn)
                        .Select(s => new StudySessionViewModel
                        {
                            Id = s.Id,
                            Topic = s.Topic,
                            StartsOn = s.StartsOn,
                            Location = s.Location
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(int groupId, StudySessionFormModel model)
        {
            StudySession session = new StudySession
            {
                Topic = model.Topic,
                StartsOn = model.StartsOn,
                Location = model.Location,
                StudyGroupId = groupId
            };

            await context.StudySessions.AddAsync(session);
            await context.SaveChangesAsync();
        }

        public async Task<int?> GetGroupIdAsync(int sessionId)
        {
            return await context.StudySessions
                .Where(s => s.Id == sessionId)
                .Select(s => (int?)s.StudyGroupId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            StudySession? session = await context.StudySessions.FindAsync(id);

            if (session == null)
            {
                return false;
            }

            context.StudySessions.Remove(session);
            await context.SaveChangesAsync();

            return true;
        }
    }
}