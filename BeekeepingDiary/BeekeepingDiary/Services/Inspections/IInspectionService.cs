using System;
using System.Collections.Generic;

namespace BeekeepingDiary.Services.Inspections
{
    public interface IInspectionService
    {
        public InspectionQueryServiceModel All(
            string userId,
            int beehiveId);
        int Create(
            DateTime date,
            int beehiveId,
            string description);

        public InspectionDetailsServiceModel Details(int id);
        public bool Edit(int inspectionId, DateTime date, string description);
        public IEnumerable<InspectionBeehiveServiceModel> AllBeehives(string userId);
        public IEnumerable<InspectionServiceModel> GetInspectionsByBeehiveId(int beehiveId);
        
    }
}
