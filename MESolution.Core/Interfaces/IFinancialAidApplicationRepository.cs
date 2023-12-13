﻿using MESolution.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Interfaces
{
    public interface IFinancialAidApplicationRepository
    {
        Task<FinancialAidApplication> GetByIdWithRequestItemsAsync(int id);

    }
}
