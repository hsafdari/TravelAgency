using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.SocialLog;
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using ParvazPardaz.Model.Entity.SocialLog;

namespace ParvazPardaz.Service.DataAccessService.SocialLog
{
    
    public class SocialLogService : BaseService<ParvazPardaz.Model.Entity.SocialLog.SocialLog>, ISocialLogService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ParvazPardaz.Model.Entity.SocialLog.SocialLog> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion
        #region Ctor
        public SocialLogService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<ParvazPardaz.Model.Entity.SocialLog.SocialLog>();
            _httpContextBase = httpContextBase;
        }
        #endregion
        public bool LogVisitPage(int id, LinkType linkType)
        {
            ParvazPardaz.Model.Entity.SocialLog.SocialLog sociallog = new ParvazPardaz.Model.Entity.SocialLog.SocialLog();
            sociallog.TypeId = id;
            sociallog.LinkType = linkType;
            _dbSet.Add(sociallog);
            _unitOfWork.SaveAllChanges();
            return true;
        }
    }
}
