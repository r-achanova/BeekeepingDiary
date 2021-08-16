using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Inspections
{
  public  interface IInspectionService
    {

        public InspectionQueryServiceModel All(
            string userId, 
            int beehiveId);
        int Create(
            DateTime date, 
            int beehiveId, 
            string description);

        public InspectionDetailsServiceModel Details(int id);
        public bool Edit(int inspectionId, int beehiveId, DateTime date, string description);
       public IEnumerable<InspectionBeehiveServiceModel> AllBeehives(string userId);
        public IEnumerable<InspectionServiceModel> GetInspectionsByBeehiveId(int beehiveId);
        public IEnumerable<InspectionServiceModel> GetInspectionsByUserId(int userId);
        public InspectionQueryServiceModel Mine(int currentPage, int beehivesPerPage, string userId);


    }
}
