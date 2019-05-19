using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Post
{
    public interface IPostService : IBaseService<ParvazPardaz.Model.Entity.Post.Post>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <param name="username">اکر خالی بود برای همه کاربران نمایش داده شود در غیر این صورت فقط خودش</param>
        /// <returns></returns>
        IQueryable<GridPostViewModel> GetViewModelForGrid(string username = "");
        #endregion

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddPostViewModel> CreateAsync(AddPostViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditPostViewModel viewModel);
        #endregion

        #region GetTagsForDDL
        /// <summary>
        /// دریافت سلکت-لیست-آیتم هایی از کلیدواژه های 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetTagsForDDL();
        #endregion

        #region joinPostToLinkTbl
        List<PostDetailViewModel> JoinToLink(List<ParvazPardaz.Model.Entity.Post.Post> listModel);
        List<PostDetailViewModel> JoinHotelToLink(List<ParvazPardaz.Model.Entity.Post.Post> listModel);
        IList<PostListDetailViewModel> LatestPost();
        IList<PostListDetailViewModel> RelatedPost(List<string> tags);
        IList<PostListDetailViewModel> RelatedPost(List<string> tags, int currentPostId);
        /// <summary>
        /// get groupId Or TagId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<PostListDetailViewModel> PostLists(int id, LinkType linktype, out bool hasHotel);
        /// <summary>
        /// دریافت پستها بر اساس مرتب سازی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="linktype"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        IList<PostListDetailViewModel> PostLists(int id, LinkType linktype, string orderby, out bool hasHotel);

        /// <summary>
        /// جستجو در میان پست ، هتل ، تور ، تگ و گروه پست
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        IList<PostListDetailViewModel> PostSearchLists(out bool isBeHiddenBtnMore, int pageIndex, int pageSize, string q);

        /// <summary>
        /// جستجو در میان پست ، هتل ، تگ و گروه پست
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        List<TabMagPost> MagSearchLists(out bool isBeHiddenBtnMore, int pageIndex, int pageSize, string q);


        /// <summary>
        /// Get lastUpdatedPostby count
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IList<PostListDetailViewModel> LastUpdated(int count);
        #endregion

        #region like
        int like(int? id, string type, HttpRequestBase req);
        #endregion

        #region CheckURL
        bool CheckURL(string Name);
        #endregion
    }
}
