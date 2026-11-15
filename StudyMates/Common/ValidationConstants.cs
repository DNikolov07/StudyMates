namespace StudyMates.Common
{
    public static class ValidationConstants
    {
        public static class Subject
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        public static class StudyGroup
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 60;

            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 500;

            public const int MaxMembersMin = 2;
            public const int MaxMembersMax = 20;
        }

        public static class StudySession
        {
            public const int TopicMinLength = 3;
            public const int TopicMaxLength = 80;

            public const int LocationMinLength = 3;
            public const int LocationMaxLength = 100;
        }
    }
}