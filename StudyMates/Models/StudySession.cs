using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudyMates.Common;

namespace StudyMates.Models
{
    public class StudySession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.StudySession.TopicMaxLength,
            MinimumLength = ValidationConstants.StudySession.TopicMinLength)]
        public string Topic { get; set; } = null!;

        public DateTime StartsOn { get; set; }

        [Required]
        [StringLength(ValidationConstants.StudySession.LocationMaxLength,
            MinimumLength = ValidationConstants.StudySession.LocationMinLength)]
        public string Location { get; set; } = null!;

        [ForeignKey(nameof(StudyGroup))]
        public int StudyGroupId { get; set; }

        public StudyGroup StudyGroup { get; set; } = null!;
    }
}