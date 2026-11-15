using System.ComponentModel.DataAnnotations;
using StudyMates.Common;

namespace StudyMates.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.Subject.NameMaxLength,
            MinimumLength = ValidationConstants.Subject.NameMinLength)]
        public string Name { get; set; } = null!;

        public ICollection<StudyGroup> StudyGroups { get; set; }
            = new List<StudyGroup>();
    }
}