namespace BeekeepingDiary.Services.Beehives
{
    public class BeehiveDetailsServiceModel:BeehiveServiceModel
    {
        public int CategoryId { get; init; }
        public int BeeGardenId { get; init; }
        public string UserId  { get; init; }
    }
}
