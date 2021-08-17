namespace BeekeepingDiary.Data
{
    public class DataConstants
    {
        public class BeeGarden
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
            public const int LocationMinLength = 2;
            public const int LocationMaxLength = 40;
            public const int DescriptionMinLength = 10;
        }

        public class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 20;
        }

        public class Beehive
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
        }

        public class Produce
        {
            public const int DescriptionMinLength = 10;
            public const int HoneyTypeMinLength = 2;
            public const int HoneyTypeMaxLength = 20;
            public const int HoneyTypeMinKg = 0;
            public const int HoneyTypeMaxKg = 200;

        }

    }
}
