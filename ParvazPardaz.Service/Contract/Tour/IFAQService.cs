﻿using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface IFAQService : IBaseService<FAQ>
    {
        #region  GetForGridView
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridFAQViewModel> GetViewModelForGrid();
        #endregion
    }
}