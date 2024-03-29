﻿using hNext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.IRepository
{
    public interface IPoster<T>:IGetter<T>
    {
        Task<T> Post(T item);
    }
}
