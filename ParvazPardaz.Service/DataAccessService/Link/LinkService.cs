using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Service.Contract.Link;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Service.DataAccessService.Link
{
    public class LinkService : BaseService<LinkTable>, ILinkService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<LinkTable> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion
        #region Ctor
        public LinkService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<LinkTable>();
            _httpContextBase = httpContextBase;
        }
        #endregion
        #region GetForGrid
        public IQueryable<GridLinkViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridLinkViewModel>(_mappingEngine);


        }
        #endregion
        #region Edit
        public string UniqEdit(EditLinkViewModel viewModel)
        {
            var models = _dbSet.Where(x => x.Id != viewModel.Id && !x.IsDeleted).ToList();
            foreach (var item in models)
            {
                if (viewModel.URL == item.URL)
                {
                    return "duplicate";
                }
            }
            var model = _dbSet.Find(viewModel.Id);// base.GetByIdAsync(c => c.Id == viewModel.Id);

            //Mapper.CreateMap<EditLinkViewModel, LinkTable>().IgnoreAllNonExisting();
            //Mapper.Map(viewModel, model);
            _mappingEngine.Map(viewModel, model);
            _unitOfWork.SaveAllChanges();
            return "update";
        }
        #endregion
    }
}
