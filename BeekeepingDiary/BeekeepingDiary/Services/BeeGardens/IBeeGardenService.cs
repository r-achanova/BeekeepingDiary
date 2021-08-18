﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.BeeGardens
{
    public interface IBeeGardenService
    {
        BeeGardenQueryServiceModel All();

        BeeGardenQueryServiceModel AllForUser(
            int currentPage,
            int beeGardensPerPage,
            string userId);
        BeeGardenServiceModel Details(int beeGardenId);
        int Create(
            string name,
            string location,
            string imageUrl,
            int year,
            string userId);

        bool Edit(
            int beeGardenId,
            string name,
            string location,
            string imageUrl,
            int year);

       bool IsByCurrentUser(int beeGardenId, string userId);

        public IEnumerable<BeeGardenServiceModel> Index();
    }
}
