using Microsoft.EntityFrameworkCore;
using StudyMates.Data;
using StudyMates.Services.Interfaces;
using StudyMates.ViewModels;
using StudyMates.Models;

namespace StudyMates.Services
{
    public class StudyGroupService : IGroupsService
    {
        private readonly ApplicationDbContext context;

        public StudyGroupService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StudyGroupViewModel>> GetAllAsync(string? userId)
        {
            return await context.StudyGroups
                .AsNoTracking()
                .OrderByDescending(g => g.CreatedOn)
                .Select(g => new StudyGroupViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    SubjectName = g.Subject.Name,
                    CreatorName = g.Creator.UserName ?? string.Empty,
                    MaxMembers = g.MaxMembers,
                    CreatedOn = g.CreatedOn,
                    IsCreator = g.CreatorId == userId
                })
                .ToListAsync();
        }

        public async Task<StudyGroupViewModel?> GetByIdAsync(int id, string? userId)
        {
            return await context.StudyGroups
                .AsNoTracking()
                .Where(g => g.Id == id)
                .Select(g => new StudyGroupViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    SubjectName = g.Subject.Name,
                    CreatorName = g.Creator.UserName ?? string.Empty,
                    MaxMembers = g.MaxMembers,
                    CreatedOn = g.CreatedOn,
                    IsCreator = g.CreatorId == userId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SubjectViewModel>> GetSubjectsAsync()
        {
            return await context.Subjects
                .AsNoTracking()
                .OrderBy(s => s.Name)
                .Select(s => new SubjectViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();
        }

        public async Task CreateAsync(StudyGroupFormModel model, string creatorId)
        {
            StudyGroup group = new StudyGroup
            {
                Title = model.Title,
                Description = model.Description,
                MaxMembers = model.MaxMembers,
                SubjectId = model.SubjectId,
                CreatorId = creatorId,
                CreatedOn = DateTime.UtcNow
            };

            await context.StudyGroups.AddAsync(group);
            await context.SaveChangesAsync();
        }

        public async Task<StudyGroupFormModel?> GetForEditAsync(int id)
        {
            return await context.StudyGroups
                .AsNoTracking()
                .Where(g => g.Id == id)
                .Select(g => new StudyGroupFormModel
                {
                    Title = g.Title,
                    Description = g.Description,
                    MaxMembers = g.MaxMembers,
                    SubjectId = g.SubjectId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> EditAsync(int id, StudyGroupFormModel model)
        {
            StudyGroup? group = await context.StudyGroups.FindAsync(id);

            if (group == null)
            {
                return false;
            }

            group.Title = model.Title;
            group.Description = model.Description;
            group.MaxMembers = model.MaxMembers;
            group.SubjectId = model.SubjectId;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsCreatorAsync(int id, string userId)
        {
            return await context.StudyGroups
                .AnyAsync(g => g.Id == id && g.CreatorId == userId);
        }
    }
}
