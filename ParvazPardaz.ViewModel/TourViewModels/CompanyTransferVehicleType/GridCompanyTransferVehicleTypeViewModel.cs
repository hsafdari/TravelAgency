using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridCompanyTransferVehicleTypeViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// نام مدل وسیله نقلیه
        /// </summary>
        [Display(Name = "ModelName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ModelName { get; set; }
        /// <summary>
        /// نام شرکت حمل و نقل
        /// </summary>
        [Display(Name = "CompanyTransfer", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string CompanyTransfer { get; set; }
        /// <summary>
        /// نوع وسیله نقلیه
        /// </summary>
        [Display(Name = "VehicleType", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string VehicleType { get; set; }
    }
}
