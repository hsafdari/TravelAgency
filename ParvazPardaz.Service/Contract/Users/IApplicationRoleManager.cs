using Microsoft.AspNet.Identity;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Users
{
    public interface IApplicationRoleManager : IDisposable
    {
        #region Microsoft.AspNet.Identity.RoleManager - متدهای مربوط به کلاس
        /// <summary>
        /// Used to validate roles before persisting changes
        /// </summary>
        IIdentityValidator<Role> RoleValidator { get; set; }

        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> CreateAsync(Role role);

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> UpdateAsync(Role role);

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> DeleteAsync(Role role);
        /// <summary>
        /// Returns true if the role exists
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<bool> RoleExistsAsync(string roleName);

        /// <summary>
        /// Find a role by id
        /// </summary>
        /// <param name="roleId"/>
        /// <returns/>
        Task<Role> FindByIdAsync(int roleId);

        /// <summary>
        /// Find a role by name
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<Role> FindByNameAsync(string roleName);
        #endregion

        /// <summary>
        /// پیدا کردن نقش بر اساس نام نقش
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Role FindRoleByName(string roleName);
        /// <summary>
        /// افزودن نقش
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IdentityResult CreateRole(Role role);
        /// <summary>
        /// لیست کاربران این نقش
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        IList<UserRole> GetUsersOfRole(string roleName);
        /// <summary>
        /// لیست نام نقشهای این کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<string> FindUserRoles(int userId);
        /// <summary>
        /// لیست نام نقشهای این کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string[] GetRolesForUser(int userId);
        /// <summary>
        /// آیا این کاربر در این گروه کاربری است؟
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool IsUserInRole(int userId, string roleName);
        /// <summary>
        /// لیست نقش ها
        /// </summary>
        /// <returns></returns>
        Task<IList<Role>> GetAllRolesAsync();
        /// <summary>
        /// مقداردهی اولیه جدول نقش
        /// </summary>
        void SeedDatabase();
        /// <summary>
        /// حذف تمام نقش ها
        /// </summary>
        /// <returns></returns>
        Task RemoteAll();
        /// <summary>
        /// افزودن لیستی از نقش ها
        /// </summary>
        /// <param name="roles"></param>
        void AddRange(IEnumerable<Role> roles);
        /// <summary>
        /// نقشی وجود دارد ؟
        /// </summary>
        /// <returns></returns>
        Task<bool> AnyAsync();
        /// <summary>
        /// نقشی وجود دارد ؟
        /// </summary>
        /// <returns></returns>
        bool Any();
        /// <summary>
        /// آیا نقش وجود دارد
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckForExisByName(string name, int? id);
        /// <summary>
        /// آیا نقش یک نقش سیسمتی است ؟
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> CheckRoleIsSystemRoleAsync(int id);
        /// <summary>
        /// پیدا کردن آیدی نقشهای این کاربر
        /// </summary>
        /// <returns></returns>
        IEnumerable<int> FindUserRoleIds(int userId);
        /// <summary>
        /// پیدا کردن آیدی نقشهای این کاربر
        /// </summary>
        /// <returns></returns>
        Task<IList<int>> FindUserRoleIdsAsync(int userId);
        /// <summary>
        /// چک کردن برای موجود بودن در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(int id);
        /// <summary>
        /// واکش نقشها به ویومدل برای نمایش در گرید
        /// </summary>
        /// <returns></returns>
        IQueryable<GridRoleViewModel> GetRolesViewModelForGrid();
        /// <summary>
        /// پیدا کردن این نقش و تبدیل آن به ویومدل
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditRoleViewModel> GetRoleViewModelAsync(int id);
        /// <summary>
        /// ویرایش نقش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditRoleAsync(EditRoleViewModel viewModel);
        /// <summary>
        /// حذف نقش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteRoleAsync(int id);
        /// <summary>
        /// لیست نقشهایی از لیست آیتم 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetRolesOfSelectListItem();
        /// <summary>
        /// ایجاد نقش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> CreateRoleAsync(AddRoleViewModel viewModel);
    }
}
