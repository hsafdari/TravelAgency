using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class FlightSearchParamViewModel
    {
        public FlightSearchParamViewModel()
        {           
        }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
        public int? companytranferId { get; set; }
        public string fromairport { get; set; }
        public string toairport { get; set; }

        public string reporttype { get; set; }
    }
}
