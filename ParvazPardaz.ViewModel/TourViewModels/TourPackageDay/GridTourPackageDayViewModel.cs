using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridTourPackageDayViewModel : BaseViewModelOfEntity
    {
        #region Properties
        public string Title { get; set; }
        public string Name { get; set; }
        #endregion
    }
}
