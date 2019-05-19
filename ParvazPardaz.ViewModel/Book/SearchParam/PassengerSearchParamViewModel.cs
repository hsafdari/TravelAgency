using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class PassengerSearchParamViewModel
    {
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }     
        public string reporttype { get; set; }
        public int? cityId { get; set; }
        public int? hotelId { get; set; }
    }
}
