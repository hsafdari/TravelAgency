using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourUIViewModel
    {
        public int Id { get; set; }
        public string TourTitle { get; set; }
        public string headerImageURL { get; set; }
        public IEnumerable<TourProgramTourDetailViewModel> TourPrograms { get; set; }
        public TourDetailViewModel tourDetail { get; set; }
        public IEnumerable<AllowBansViewModel> inclusive { get; set; }
        public IEnumerable<AllowBansViewModel> exclusive { get; set; }
        public IEnumerable<TourSchedule> DepartureDate { get; set; }
        public IEnumerable<TourSlider> TourSliders { get; set; }
        public string TourDescription { get; set; }
    }
}
