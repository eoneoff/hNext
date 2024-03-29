﻿using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IDistrictsRepository : IRepository<District>
    {
        Task<IEnumerable<City>> GetCities(int id);
    }
}
