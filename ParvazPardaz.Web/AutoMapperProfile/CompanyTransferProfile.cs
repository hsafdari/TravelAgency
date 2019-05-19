using AutoMapper;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class CompanyTransferProfile : Profile
    {
        protected override void Configure()
        {
            //مپ مدل به ویو مدل جهت نمایش در ویو اگر فیلدی در ویو مدل همنام نبود با مدل بدین صورت مپ میکنیم
            CreateMap<CompanyTransfer, EditCompanyTransferViewModel>().ForMember(x => x.ImageType, m => m.MapFrom(model => model.ImageExtension))
                                                                      .IgnoreAllNonExisting();
            //مپ ویومدل به مدل برای ثبت داده ها ویرایش شده به همین جهت ار فیلدهایی که در ویو نیامده و در ویو مدل آمده و نخواهیم در مدل تغییری کند صرف نظر میشود
            CreateMap<EditCompanyTransferViewModel, CompanyTransfer>().ForMember(x => x.ImageFileName, m => m.Condition(o=>o.PropertyMap.SourceMember !=null && !o.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageSize, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageExtension, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageUrl, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .IgnoreAllNonExisting();

            CreateMap<AddCompanyTransferViewModel, CompanyTransfer>().IgnoreAllNonExisting();
            CreateMap<CompanyTransfer, AddCompanyTransferViewModel>().IgnoreAllNonExisting();

            CreateMap<CompanyTransfer, GridCompanyTransferViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                    .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                    .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                    .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                                    .ForMember(x => x.Image, m => m.MapFrom(model=>model.ImageUrl)).IgnoreAllNonExisting();
        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}