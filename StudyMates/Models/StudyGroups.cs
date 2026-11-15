using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using StudyMates.Common;

namespace StudyMates.Models
{
    public class StudyGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.StudyGroup.TitleMaxLength,
            MinimumLength = ValidationConstants.StudyGroup.TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.StudyGroup.DescriptionMaxLength,
            MinimumLength = ValidationConstants.StudyGroup.DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Range(ValidationConstants.StudyGroup.MaxMembersMin,
            ValidationConstants.StudyGroup.MaxMembersMax)]
        public int MaxMembers { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }

        public Subject Subject { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Creator))]
        public string CreatorId { get; set; } = null!;

        public IdentityUser Creator { get; set; } = null!;

        public ICollection<IdentityUser> Members { get; set; }
            = new List<IdentityUser>();

        public ICollection<StudySession> Sessions { get; set; }
            = new List<StudySession>();
    }
}