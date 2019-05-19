using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TourClientProfile:Profile
    {
        protected override void Configure()
        {
            base.Configure();
        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}