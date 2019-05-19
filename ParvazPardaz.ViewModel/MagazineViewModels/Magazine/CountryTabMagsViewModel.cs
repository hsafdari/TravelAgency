using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel.Magazine;
using ParvazPardaz.Model.Entity.Magazine;

namespace ParvazPardaz.ViewModel
{
    public class CountryTabMagsViewModel
    {
        public CountryTabMagsViewModel()
        {
            TabList = new List<TabMagazineContent>();
        }
        public long LinkTblId { get; set; }
        public string CountryTitle { get; set; }
        public string SeoText { get; set; }
        public int PostRateAvg { get; set; }
        public int PostRateCount { get; set; }

        //public List<TabMagazine> TabMagazineList { get; set; }
        public List<TabMagazineContent> TabList { get; set; }
    }

    public class TabMagazineContent
    {
        public TabMagazineContent()
        {

        }

        public TabMagazine TabMagazine { get; set; }
        public List<TabMagPost> TabMagPostList { get; set; }
        public TabMagNestedItemViewModel ChildTabMag { get; set; }
    }
}
