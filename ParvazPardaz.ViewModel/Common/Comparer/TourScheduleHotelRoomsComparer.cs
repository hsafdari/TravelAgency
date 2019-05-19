using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.Common.Comparer
{
    public class DynamicControlComparer : IEqualityComparer<TourScheduleHotelRoomDynamicControl>
    {
        public bool Equals(TourScheduleHotelRoomDynamicControl x, TourScheduleHotelRoomDynamicControl y)
        {
            return x.HotelRoomId.Equals(y.HotelRoomId);
        }

        public int GetHashCode(TourScheduleHotelRoomDynamicControl obj)
        {
            return obj.HotelRoomId.GetHashCode();
        }
    }
}
