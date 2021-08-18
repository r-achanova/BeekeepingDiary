namespace BeekeepingDiary.Services.BeeGardens
{
    public class BeeGardenIndexServiceModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }
    }
}
