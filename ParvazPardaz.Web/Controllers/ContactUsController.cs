using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Model;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Core;
using Infrastructure;

namespace ParvazPardaz.Web.Controllers
{
    public class ContactUsController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactUsService _contactusService;
        #endregion

        #region	Ctor
        public ContactUsController(IUnitOfWork unitOfWork, IContactUsService contactusService)
        {
            _unitOfWork = unitOfWork;
            _contactusService = contactusService;
        }
        #endregion

        #region Create
        [Route("ContactUs")]
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            ViewBag.DepartmentDDL = new SelectList(_unitOfWork.Set<Department>().Where(x => !x.IsDeleted && x.IsActive), "Id", "DepartmentName");
            return View();
        }


        [HttpPost]
        [Route("ContactUs")]
        public async Task<ActionResult> Create(AddContactUsViewModel addContactUsViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = Request["g-recaptcha-response"];
                if (response != null && ReCaptcha.IsValid(response))
                {
                    var newContactUs = await _contactusService.CreateAsync<AddContactUsViewModel>(addContactUsViewModel);
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                return Json("InvalidCaptcha", JsonRequestBehavior.AllowGet);
            }

            ViewBag.DepartmentDDL = new SelectList(_unitOfWork.Set<Department>().Where(x => !x.IsDeleted && x.IsActive), "Id", "DepartmentName");
            return View(addContactUsViewModel);
        }
        #endregion
    }
}
