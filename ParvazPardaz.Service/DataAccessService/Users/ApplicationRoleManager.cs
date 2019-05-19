using Microsoft.AspNet.Identity;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Common.Extension;
using System.Web.Mvc;
using AutoMapper;
using EntityFramework.Extensions;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;

namespace ParvazPardaz.Service.DataAccessService.Users
{
    public class ApplicationRoleManager : RoleManager<Role, int>, IApplicationRoleManager
    {

        #region Fields
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Role> _roles;
        #endregion

        #region Constructor
        public ApplicationRoleManager(IUnitOfWork unitOfWork, IRoleStore<Role, int> roleStore, IMappingEngine mappingEngine)
            : base(roleStore)
        {
            _unitOfWork = unitOfWork;
            _roles = _unitOfWork.Set<Role>();
            _mappingEngine = mappingEngine;
            //AutoCommitEnabled = true;
        }
        #endregion

        #region FindRoleByName
        public Role FindRoleByName(string roleName)
        {
            return this.FindByName(roleName);
        }
        #endregion

        #region CreateRole
        public IdentityResult CreateRole(Role role)
        {
            return this.Create(role); // RoleManagerExtensions
        }
        #endregion

        #region GetUsersOfRole
        public IList<UserRole> GetUsersOfRole(string roleName)
        {
            return Roles.Where(role => role.Name == roleName)
                             .SelectMany(role => role.Users)
                             .ToList();
        }

        #endregion

        #region FindUserRoles
        public IList<string> FindUserRoles(int userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.OrderBy(x => x.Name).Select(a => a.Name).ToList();
        }

        public IEnumerable<int> FindUserRoleIds(int userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.Select(a => a.Id).ToList();
        }

        public async Task<IList<int>> FindUserRoleIdsAsync(int userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return await userRolesQuery.Select(a => a.Id).ToListAsync();
        }


        //public async Task<IList<string>> FindUserPermissions(int userId)
        //{
        //    var userRolesQuery = from role in Roles
        //                         from user in role.Users
        //                         where user.UserId == userId
        //                         select new { role.Name };

        //    var roles = await userRolesQuery.AsNoTracking().ToListAsync();
        //    var roleNames = roles.Select(a => a.Name).ToList();
        //    return roleNames.Union(
        //            _permissionService.GetUserPermissionsAsList(
        //                roles.Select(a => XElement.Parse(a.Permissions)).ToList())).ToList();
        //}
        #endregion

        #region GetRolesForUser
        public string[] GetRolesForUser(int userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new string[] { };
            }

            return roles.ToArray();
        }

        #endregion

        #region IsUserInRole
        public bool IsUserInRole(int userId, string roleName)
        {
            var userRolesQuery = from role in Roles
                                 where role.Name == roleName
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }

        #endregion

        #region GetAllRolesAsync
        public async Task<IList<Role>> GetAllRolesAsync()
        {
            return await Roles.ToListAsync();
        }

        #endregion

        #region DeleteAll
        public async Task RemoteAll()
        {
            await Roles.DeleteAsync();
        }
        #endregion

        #region GetList


        //public async Task<IEnumerable<RoleViewModel>> GetList()
        //{
        //    return await _roles.Project(_mappingEngine).To<RoleViewModel>().ToListAsync();
        //}
        //#endregion

        //#region AddRole
        //public async Task<RoleViewModel> AddRole(AddRoleViewModel viewModel)
        //{
        //    var role = _mappingEngine.Map<Role>(viewModel);
        //    _roles.Add(role);
        //    await _unitOfWork.SaveChangesAsync();
        //    return await GetRole(role.Id);
        //}
        #endregion

        #region SeedDatabase
        /// <summary>
        /// برای مقدار دهی اولیه دیتا بیس و جدول نقش ها
        /// </summary>
        public void SeedDatabase()
        {
            var standardRoles = StandardRoles.GetSysmteRoles();

            foreach (var role in from roleName in standardRoles
                                 let role = this.FindByName(roleName)
                                 where role == null
                                 select new Role
                                 {
                                     Name = roleName,
                                     IsSystemRole = true,
                                     IsBanned = false,
                                     CreatorDateTime = DateTime.Now
                                 })
            {
                _roles.Add(role);
            }

            _unitOfWork.SaveAllChanges();
        }

        #endregion

        #region AddRange
        public void AddRange(IEnumerable<Role> roles)
        {
            _unitOfWork.AddThisRange(roles);
        }
        #endregion

        #region AnyAsync
        public Task<bool> AnyAsync()
        {
            return _roles.AnyAsync();
        }
        public bool Any()
        {
            return _roles.Any();
        }
        #endregion

        #region CheckForExisByName
        public bool CheckForExisByName(string name, int? id)
        {
            var roles = _roles.Select(a => new { Id = a.Id, Name = a.Name }).ToList();
            return id == null ? roles.Any(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName())
                : roles.Any(a => id.Value != a.Id && a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName());
        }
        #endregion

        #region CheckRoleIsSystemRoleAsync
        public async Task<bool> CheckRoleIsSystemRoleAsync(int id)
        {
            return await _roles.AnyAsync(a => a.Id == id && a.IsSystemRole);
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(int id)
        {
            return _roles.AnyAsync(a => a.Id == id);
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridRoleViewModel> GetRolesViewModelForGrid()
        {
            return _roles.AsNoTracking().ProjectTo<GridRoleViewModel>(_mappingEngine);
        }
        #endregion

        #region CreateRoleAsync
        public async Task<int> CreateRoleAsync(AddRoleViewModel viewModel)
        {
            var role = _mappingEngine.Map<Role>(viewModel);
            _roles.Add(role);
            return await _unitOfWork.SaveAllChangesAsync();
        }
        #endregion

        #region GetRoleViewModelAsync
        public virtual async Task<EditRoleViewModel> GetRoleViewModelAsync(int id)
        {
            return await _unitOfWork.Set<Role>().AsNoTracking().ProjectTo<EditRoleViewModel>(_mappingEngine).FirstOrDefaultAsync(u => u.Id == id);
        }
        #endregion

        #region EditRoleAsync
        public async Task<int> EditRoleAsync(EditRoleViewModel viewModel)
        {
            var role = await ((DbSet<Role>)_roles).FindAsync(viewModel.Id);
            _mappingEngine.Map(viewModel, role);
            return await _unitOfWork.SaveAllChangesAsync();
        }
        #endregion

        #region DeleteRoleAsync
        public async Task<int> DeleteRoleAsync(int id)
        {
            return await _roles.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region GetAllRolesOfSelectListItem
        public IEnumerable<SelectListItem> GetRolesOfSelectListItem()
        {
            return _roles.Select(r => new SelectListItem() { Selected = false, Text = r.Name, Value = r.Id.ToString() }).AsEnumerable();
        }
        #endregion
    }
}
