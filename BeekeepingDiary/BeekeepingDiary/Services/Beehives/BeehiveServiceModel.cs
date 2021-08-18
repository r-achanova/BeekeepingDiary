namespace BeekeepingDiary.Services.Beehives
{
    public class BeehiveServiceModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string ImageUrl { get; init; }
        public int Year { get; init; }
        public string Category { get; init; }
        public string BeeGarden { get; init; }
    }
}
