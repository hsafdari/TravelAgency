using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridOrderInformationViewModel : BigBaseViewModelOfEntity
    {
        #region properties
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string LastName { get; set; }

        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Title { get; set; }

        [Display(Name = "Age", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Tel", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Tel { get; set; }

        [Display(Name = "Cellphone", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Cellphone { get; set; }

        [Display(Name = "Address", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Address { get; set; }

        [Display(Name = "User", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int UserId { get; set; }

        [Display(Name = "CityId", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int? CityId { get; set; }
        public string NationalCode { get; set; }
        public int Nationality { get; set; }
        #endregion
    }
}
