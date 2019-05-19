﻿using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Core
{
    public interface ISliderGroupService : IBaseService<SliderGroup>
    {
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridSliderGroupViewModel> GetViewModelForGrid();
    }
}
