using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ParvazPardaz.ViewModel
{
    public class TourPackageFilterViewModel : BaseEntity
    {
        public TourPackageFilterViewModel()
        {
            this.days = new List<int>();
            this.hotelrates = new List<int>();
            this.airlines = new List<int>();
            this.fromcities = new List<int>();
            this.tocities = new List<int>();
            this.hdntocities = new List<int>();
        }


        #region Properties
        public string enTitle { get; set; }
        public List<int> days {get;set;}
        public List<int>airlines{get;set;}
        public List<int> hotelrates{get;set;}
        public List<int> fromcities{get;set;}
        public List<int> tocities { get; set; }
        public List<int> hdntocities { get; set; }
        public string FilterType { get; set; }
        #endregion
    }
}
