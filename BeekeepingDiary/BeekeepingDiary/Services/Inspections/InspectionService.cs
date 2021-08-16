using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Inspections
{
    public class InspectionService : IInspectionService
    {
        private readonly BeekeepingDbContext data;
        public InspectionService(BeekeepingDbContext data)
        {
            this.data=data;
        }

        public InspectionQueryServiceModel All(string userId, int beehiveId)
        {
            var inspections = GetInspectionsByBeehiveId(beehiveId);
            inspections = inspections.OrderByDescending(b => b.Date);

            return new InspectionQueryServiceModel
            {
                Inspections = inspections
            };
        }
        public IEnumerable<InspectionBeehiveServiceModel> AllBeehives(string userId)
        {
            var beehives = this.data
                  .Beehives
                  .Where(b => b.BeeGarden.UserId == userId)
                  .Select(b => new InspectionBeehiveServiceModel
                  {
                      Id = b.Id,
                      Name = b.Name,
                      UserId = b.BeeGarden.UserId
                  })
                   .ToList();
            return beehives;
        }

        public int Create(DateTime date, int beehiveId, string description)
        {
            var inspectionData = new Inspection
            {
                Date = date,
                Description=description,
                BeehiveId=beehiveId
            };
            this.data.Inspections.Add(inspectionData);
            this.data.SaveChanges();

            return inspectionData.Id;
        }

        public InspectionDetailsServiceModel Details(int id)
            => this.data
                .Inspections
                .Where(i => i.Id == id)
                .Select(i => new InspectionDetailsServiceModel
                {
                    Id = i.Id,
                    Date = i.Date,
                    Description = i.Description,
                    BeehiveId=i.BeehiveId,
                    Beehive=i.Beehive.Name,
                    UserId = i.Beehive.BeeGarden.UserId
                })
                .FirstOrDefault();

        public bool Edit(int inspectionId, int beehiveId, DateTime date, string description)
        {
            var inspectionData = this.data.Inspections.Find(inspectionId);
            if (inspectionData == null)
            {
                return false;
            }

            inspectionData.Date = date;
            inspectionData.Description = description;
            inspectionData.BeehiveId = beehiveId;
           
            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<InspectionServiceModel> GetInspectionsByBeehiveId(int beehiveId)
        {
            var query = this.data.Inspections
                .Where(x => x.BeehiveId == beehiveId)
                .Select(x => new InspectionServiceModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Beehive = x.Beehive.Name,
                    Description=x.Description
                })
                .ToList();
            return query;
        }

        public IEnumerable<InspectionServiceModel> GetInspectionsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public InspectionQueryServiceModel Mine(int currentPage, int beehivesPerPage, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
