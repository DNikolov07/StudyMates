using System.ComponentModel.DataAnnotations;
using StudyMates.Common;

namespace StudyMates.ViewModels
{
    public class StudyGroupFormModel
    {
        [Required]
        [StringLength(ValidationConstants.StudyGroup.TitleMaxLength, MinimumLength = ValidationConstants.StudyGroup.TitleMinLength)]

        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.StudyGroup.DescriptionMaxLength, MinimumLength = ValidationConstants.StudyGroup.DescriptionMinLength)]

        public string Description { get; set; } = null!;

        [Display(Name = "Maximum members")]
        [Range(ValidationConstants.StudyGroup.MaxMembersMin, ValidationConstants.StudyGroup.MaxMembersMax)]

        public int MaxMembers { get; set; }

        [Display(Name = "Subject")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a subject.")]

        public int SubjectId { get; set; }

        public IEnumerable<SubjectViewModel> Subjects { get; set; } = new List<SubjectViewModel>();
    }
}
