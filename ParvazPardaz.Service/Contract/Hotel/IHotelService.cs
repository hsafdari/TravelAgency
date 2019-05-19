using ParvazPardaz.Service.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.ViewModel;
using System.Web.Mvc;
using EntityType = ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.Service.Contract.Hotel
{
    public interface IHotelService : IBaseService<ParvazPardaz.Model.Entity.Hotel.Hotel>
    {
        #region  GetForGridView
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridHotelViewModel> GetViewModelForGrid();

        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <param name="username">اکر خالی بود برای همه کاربران نمایش داده شود در غیر این صورت فقط خودش</param>
        /// <returns></returns>
        IQueryable<GridHotelViewModel> GetViewModelForGrid(string username = "");
        #endregion


        #region  CreateAsync
        Task<AddHotelViewModel> CreateAsync(AddHotelViewModel viewModel);
        #endregion

        #region UpdateAsync
        Task<EditHotelViewModel> UpdateAsync(EditHotelViewModel viewModel);
        #endregion

        #region GetTagsForDDL
        /// <summary>
        /// دریافت سلکت-لیست-آیتم هایی از کلیدواژه های 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetTagsForDDL();
        #endregion

        //#region joinHotelToLinkTbl
        //List<HotelDetailViewModel> JoinToLink(List<EntityType.Hotel> listModel);
        //IList<HotelListDetailViewModel> RelatedHotel(List<string> tags);
        //IList<HotelListDetailViewModel> RelatedHotel(List<string> tags, int currentHotelId);
        ///// <summary>
        ///// get groupId Or TagId
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //IList<HotelListDetailViewModel> HotelLists(int id, LinkType linktype);
        ///// <summary>
        ///// Get lastUpdatedPostby count
        ///// </summary>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //IList<HotelListDetailViewModel> LastUpdated(int count);
        //#endregion

        //#region  UploadHotelGallery
        //Task<HotelGalleryViewModel> UploadHotelGallery(HotelGalleryViewModel viewModel);
        //#endregion

        //#region  RemoveHotelGallery
        //bool RemoveHotelGallery(int id);
        //#endregion

        #region GetAllHotelsOfSelectListItem
        IEnumerable<SelectListItem> GetAllHotelsOfSelectListItem();
        #endregion

        #region GetHotelsByTourProgram
        /// <summary>
        /// لیست تمام هتلهای شهر های یک تور بر اساس برنامه سفر آن  
        /// </summary>
        /// <param name="term"></param>
        /// <param name="tourProgramId"></param>
        /// <returns></returns>
        IEnumerable<FilterList> GetHotelsByTourProgram(int tourId);
        #endregion

        #region GetHotelsByTourProgramForAutoComplete
        /// <summary>
        ///لیست تمام هتلهای شهر های یک تور بر اساس برنامه سفر آن و قیلتر برای اتو کامپلت 
        /// </summary>
        /// <param name="phrase"></param>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IEnumerable<FilterListForAutoComplete> GetHotelsByTourProgramForAutoComplete(string phrase, int tourId);
        #endregion

        #region LatestHotel
        IList<PostListDetailViewModel> LatestHotel();
        #endregion

        #region RelatedHotels
        IList<PostListDetailViewModel> RelatedHotel(List<string> tags);
        IList<PostListDetailViewModel> RelatedHotel(List<string> tags, int currentHotelId);
        #endregion

        #region JoinHotelToLink
        List<HotelDetailsViewModel> JoinHotelToLink(List<ParvazPardaz.Model.Entity.Hotel.Hotel> listModel);
        #endregion
         #region FindHotelByCityId (Cascading DDL)
         IEnumerable<SelectListItem> FindHotelByCityId(int? cityId);
            #endregion
    }
}
