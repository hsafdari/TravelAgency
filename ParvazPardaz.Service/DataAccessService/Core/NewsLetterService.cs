using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ParvazPardaz.Service.DataAccessService.Core
{
    public class NewsLetterService : BaseService<Newsletter>, INewsLetterService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Newsletter> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public NewsLetterService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Newsletter>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridNewsLetterViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridNewsLetterViewModel>(_mappingEngine);
        }
        #endregion
        #region setCookie
        public HttpCookie setCookie()
        {
            HttpCookie newsLetterCookie = new HttpCookie("charterancookie")
            {
                Value = JsonConvert.SerializeObject(true),
                Expires = DateTime.Now.AddMinutes(5)
            };
            return newsLetterCookie;
        }
        #endregion
    }
}
