namespace StudyMates.ViewModels
{
    public class StudyGroupViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public string CreatorName { get; set; } = null!;
        public int MaxMembers { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsCreator { get; set; }


    }
}
