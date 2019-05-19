using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// لیست مسافران در پنل مشتری
    /// </summary>
    public class PassengersViewModel
    {
        #region Constructor
        public PassengersViewModel()
        {
            PassengerList = new List<AddPassengerViewModel>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// مسافران
        /// </summary>
        public List<AddPassengerViewModel> PassengerList { get; set; } 
        #endregion
    }
}
