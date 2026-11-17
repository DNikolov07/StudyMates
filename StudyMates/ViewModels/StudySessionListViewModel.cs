namespace StudyMates.ViewModels
{
    public class StudySessionListViewModel
    {
        public int StudyGroupId { get; set; }

        public string StudyGroupTitle { get; set; } = null!;

        public bool IsCreator { get; set; }

        public IEnumerable<StudySessionViewModel> Sessions { get; set; }
            = new List<StudySessionViewModel>();
    }
}