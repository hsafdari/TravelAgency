using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ExportExcelSelectedHotelViewModel
    {
        public int Id { get; set; }
        public string fromdate{ get; set; }
        public string todate { get; set; }
        public string Gfromdate { get; set; }
        public string Gtodate { get; set; }
        public string Hotel{ get; set; }
        public string City { get; set; }
        public int? numberroom { get; set; }
        public string BedType { get; set; }
        public long OrderId { get; set; }
        public decimal TotalPay { get; set; }
        public string Buyer { get; set; }
    }
}
