using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridOrderTaskViewModel : BaseViewModelOfEntity
    {
        #region Properties
        [Display(Name = "Order", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public long OrderId { get; set; }

        [Display(Name = "Task", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int TaskId { get; set; }

        [Display(Name = "Value", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TaskValue { get; set; }
        #endregion
    }
}
