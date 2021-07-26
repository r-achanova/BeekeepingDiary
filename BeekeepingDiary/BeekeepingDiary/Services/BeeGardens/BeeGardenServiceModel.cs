﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.BeeGardens
{
    public class BeeGardenServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Location { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }
    }
}
