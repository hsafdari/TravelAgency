using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class HotelRoomDynamicControl
    {
        public int HotelRoomId { get; set; }
        public string HotelRoomTitle { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public int Value { get; set; }
    }


    public class AdditionalServiceControl
    {
        public int TourScheduleAdditionalServiceId { get; set; }
        public int AdditionalServiceId { get; set; }
        public string AdditionalServiceTitle { get; set; }
        public string AdditionalServicePrice { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }
    }
}
