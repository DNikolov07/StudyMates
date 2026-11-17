using System.ComponentModel.DataAnnotations;
using StudyMates.Common;

namespace StudyMates.ViewModels
{
    public class StudySessionFormModel
    {
        [Required]
        [StringLength(ValidationConstants.StudySession.TopicMaxLength,
            MinimumLength = ValidationConstants.StudySession.TopicMinLength)]
        public string Topic { get; set; } = null!;

        [Display(Name = "Starts on")]
        public DateTime StartsOn { get; set; }

        [Required]
        [StringLength(ValidationConstants.StudySession.LocationMaxLength,
            MinimumLength = ValidationConstants.StudySession.LocationMinLength)]
        public string Location { get; set; } = null!;
    }
}