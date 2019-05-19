//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using ParvazPardaz.Common.Extension;
//using ParvazPardaz.ViewModel;
//using ParvazPardaz.Model.Entity.Tour;

//namespace ParvazPardaz.Web.AutoMapperProfile
//{
//    public class TourScheduleHotelRoomProfile : Profile
//    {
//        protected override void Configure()
//        {
//            CreateMap<TourScheduleHotelRoomDynamicControl, TourScheduleHotelRoom>().ForMember(x => x.TourScheduleId, m => m.MapFrom(model => model.TourScheduleId))
//                                                                                   .ForMember(x => x.HotelRoomId, m => m.MapFrom(model => model.HotelRoomId))
//                                                                                   .ForMember(x => x.NonLimit, m => m.MapFrom(model => (model.Capacity == 0) ? true : false))
//                                                                                   .ForMember(x => x.Capacity, m => m.MapFrom(model => model.Capacity))
//                                                                                   .IgnoreAllNonExisting();
//        }

//    }
//}