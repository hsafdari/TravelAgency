using AutoMapper;
using DataTables.Mvc;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Book;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.ViewModel;
using ParvazPardaz.ViewModel.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.Web.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mappingEngine;
        private readonly IApplicationUserManager _userManager;
        private readonly IOrderService _orderService;
        #endregion

        #region Ctor
        public OrderController(IApplicationUserManager userManager, IOrderService orderService, IMappingEngine mappingEngine, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _orderService = orderService;
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            var loggeInUserId = _userManager.GetCurrentUser().Id;
            var viewmodel = _orderService.GetViewModelForGridByUserId(loggeInUserId);
            return View(viewmodel);
        }

        [HttpPost]
        public JsonResult GetOrderTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, DateTime? fromdate, DateTime? todate, string reporttype = "")
        {
            try
            {
                string _username = _userManager.GetCurrentUser().UserName;
                var query = _orderService.GetViewModelForGrid(UserProfileType.Personal, fromdate, todate, reporttype).Where(x => x.CreatorUserName == _username);
                var totalCount = query.Count();

                #region Filtering
                // Apply filters for searching
                if (requestModel.Search.Value != string.Empty)
                {
                    var value = requestModel.Search.Value.Trim();
                    int n;
                    bool isNumeric = int.TryParse(value, out n);
                    if (isNumeric)
                    {
                        long _orderId = long.Parse(value);
                        query = query.Where((x => (x.OrderId != null && x.OrderId.Equals(_orderId))));
                    }
                    query = query.Where((x => (x.TourTitle != null || x.TourTitle.Contains(value)) &&
                                              (x.TourCode != null || x.TourCode.Contains(value)) &&
                                              (x.TourPackageCode != null || x.TourPackageCode.Contains(value)) &&
                                              (x.TrackingCode != null || x.TrackingCode.Contains(value))
                                              ));
                }
                var filteredCount = query.Count();
                #endregion

                #region Sorting
                // Sorting
                var sortedColumns = requestModel.Columns.GetSortedColumns();
                var sortColumn = String.Empty;
                foreach (var column in sortedColumns)
                {
                    sortColumn += sortColumn != String.Empty ? "," : "";
                    sortColumn += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
                }
                query = System.Linq.Dynamic.DynamicQueryable.OrderBy(query, sortColumn == string.Empty ? "Id asc" : sortColumn);//sortColumn + " " + sortColumnDirection);    
                #endregion

                // Paging
                var data = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();
                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region GetHotelPackages
        public ActionResult GetHotelPackages(int Id)
        {
            GridOrderViewModel OrderViewModel = new GridOrderViewModel();

            var Order = _orderService.Filter(x => x.Id == Id).FirstOrDefault();

            OrderViewModel.TourReserve = _mappingEngine.DynamicMap<TourReserveViewModel>(Order);

            ViewBag.TrackingCode = Order.TrackingCode;
            return PartialView("_PrvListOfHotelPackage", OrderViewModel);
        }
        #endregion

        #region Voucher
        public ActionResult Voucher(string trackingCode)
        {
            //شخصی که الان داخل سیستم است
            var loggedInUserId = _unitOfWork.Set<User>().FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
            //اطلاعات رسید؛ در صورتی که این رسید برای وی باشد، در غیر این صورت هیچ اطلاعاتی نمی آورد
            VoucherViewModel voucherInfo = new VoucherViewModel();
            voucherInfo = _orderService.VoucherInfo(trackingCode, loggedInUserId);
            var Footer = _unitOfWork.Set<ParvazPardaz.Model.Entity.Core.Footer>().Where(x => x.FooterType == EnumFooterType.Ticket).ToList();
            if (Footer != null)
            {
                _mappingEngine.DynamicMap<List<ParvazPardaz.Model.Entity.Core.Footer>, List<FooterUIViewModel>>(Footer, voucherInfo.Footer);
            }
            List<VoucherBookRoomList> selectedRooms = voucherInfo.HotelRoomInfos
                .GroupBy(x => new { Title = x.Title, TitleEn = x.TitleEn })
                .Select(s => new VoucherBookRoomList { Title = s.Key.Title, TitleEn = s.Key.TitleEn, Count = s.Count() })
                .ToList();
            ViewBag.SelectedRooms = selectedRooms;
            return View(voucherInfo);
        }
        #endregion
    }
}