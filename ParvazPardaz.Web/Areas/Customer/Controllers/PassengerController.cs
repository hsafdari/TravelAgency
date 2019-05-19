using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.Web.Areas.Customer.Controllers
{
    public class PassengerController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mappingEngine;
        private readonly IApplicationUserManager _userManager;
        private readonly ICountryService _countryService;
        #endregion

        #region Constructor
        public PassengerController(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, IApplicationUserManager userManager, ICountryService countryService)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _userManager = userManager;
            _countryService = countryService;
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            #region مسافران
            var loggeInUserId = _userManager.GetCurrentUser().Id;
            var passengerList = _unitOfWork.Set<Passenger>().Where(x => !x.IsDeleted && x.SelectedRoom.SelectedHotelPackage.Order.CreatorUserId == loggeInUserId).Distinct().Select(x => new AddPassengerViewModel()
            {
                Id = x.Id,
                Gender = x.Gender,
                AgeRange = x.AgeRange,
                BirthCountryId = x.BirthCountryId,
                BirthCountryTitle = x.BirthCountry.Title,
                Birthdate = x.Birthdate,
                EnFirstName = x.EnFirstName,
                EnLastName = x.EnLastName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                NationalCode = x.NationalCode,
                Nationality = x.NationalityTitle,
                PassportExpirationDate = x.PassportExpirationDate,
                PassportExporterCountryId = x.PassportExporterCountryId,
                PassportNo = x.PassportNo

            }).ToList();
            #endregion

            //دراپ داون کشورها
            ViewBag.CountryDDL = _countryService.GetAllCountryOfSelectListItem();
            //سن بزرگسال باید حداقل 12 سال باشد
            ViewBag.AdultStartBirthDate = DateTime.Now.AddYears(-120).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            var AdultStartBirthDate = DateTime.Now.AddYears(-120).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ViewBag.AdultEndBirthDate = DateTime.Now.AddYears(-12).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            //سن کودک بین 2 تا 12 سال باشد 
            ViewBag.ChildEndBirthDate = DateTime.Now.AddYears(-2).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ViewBag.ChildStartBirthDate = DateTime.Now.AddYears(-11).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            //سن نوزاد باید بین 10 روز تا 2 سال باشد
            ViewBag.InfantEndBirthDate = DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ViewBag.InfantStartBirthDate = DateTime.Now.AddYears(-2).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            return View(passengerList);
        }
        #endregion

        #region Edit
        [HttpPost]
        public ActionResult Edit(AddPassengerViewModel viewmodel)
        {
            var passengerInDb = _unitOfWork.Set<Passenger>().FirstOrDefault(x => x.Id == viewmodel.Id);

            #region تغییر تاریخ به شمسی به خاطر اینکه در دیتابیس به درستی ثبت بشه
            PersianCalendar pc = new PersianCalendar();
            var passportExpirationDate = viewmodel.PassportExpirationDate.Value;
            viewmodel.PassportExpirationDate = new DateTime(pc.GetYear(passportExpirationDate), pc.GetMonth(passportExpirationDate), pc.GetDayOfMonth(passportExpirationDate));
            var Birthdate = viewmodel.Birthdate.Value;
            viewmodel.Birthdate = new DateTime(pc.GetYear(Birthdate), pc.GetMonth(Birthdate), pc.GetDayOfMonth(Birthdate));
            #endregion

            _mappingEngine.DynamicMap<AddPassengerViewModel, Passenger>(viewmodel, passengerInDb);
            var countOfUpdate = _unitOfWork.SaveAllChanges();

            #region ViewBag......
            //دراپ داون کشورها
            ViewBag.CountryDDL = _countryService.GetAllCountryOfSelectListItem();
            //سن بزرگسال باید حداقل 12 سال باشد
            ViewBag.AdultStartBirthDate = DateTime.Now.AddYears(-120).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            var AdultStartBirthDate = DateTime.Now.AddYears(-120).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ViewBag.AdultEndBirthDate = DateTime.Now.AddYears(-12).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            //سن کودک بین 2 تا 12 سال باشد 
            ViewBag.ChildEndBirthDate = DateTime.Now.AddYears(-2).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ViewBag.ChildStartBirthDate = DateTime.Now.AddYears(-11).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            //سن نوزاد باید بین 10 روز تا 2 سال باشد
            ViewBag.InfantEndBirthDate = DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ViewBag.InfantStartBirthDate = DateTime.Now.AddYears(-2).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            #endregion

            #region مسافران
            var loggeInUserId = _userManager.GetCurrentUser().Id;
            var passengerList = _unitOfWork.Set<Passenger>().Where(x => !x.IsDeleted && x.SelectedRoom.SelectedHotelPackage.Order.CreatorUserId == loggeInUserId).Distinct().Select(x => new AddPassengerViewModel()
            {
                Id = x.Id,
                Gender = x.Gender,
                AgeRange = x.AgeRange,
                BirthCountryId = x.BirthCountryId,
                BirthCountryTitle = x.BirthCountry.Title,
                Birthdate = x.Birthdate,
                EnFirstName = x.EnFirstName,
                EnLastName = x.EnLastName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                NationalCode = x.NationalCode,
                Nationality = x.NationalityTitle,
                PassportExpirationDate = x.PassportExpirationDate,
                PassportExporterCountryId = x.PassportExporterCountryId,
                PassportNo = x.PassportNo

            }).ToList();

            #endregion

            return PartialView("_PrvPassengerList", passengerList);
        }
        #endregion
    }
}