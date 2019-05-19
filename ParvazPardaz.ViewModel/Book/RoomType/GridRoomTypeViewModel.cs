using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridRoomTypeViewModel : BaseViewModelOfEntity
    {
        #region properties
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Title { get; set; }
        #endregion
    }
}
