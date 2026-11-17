namespace StudyMates.ViewModels
{
    public class StudySessionViewModel
    {
        public int Id { get; set; }

        public string Topic { get; set; } = null!;

        public DateTime StartsOn { get; set; }

        public string Location { get; set; } = null!;
    }
}