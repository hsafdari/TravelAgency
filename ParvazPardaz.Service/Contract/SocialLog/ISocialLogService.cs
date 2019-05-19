using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel.SocialLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.SocialLog
{
    public interface ISocialLogService : IBaseService<ParvazPardaz.Model.Entity.SocialLog.SocialLog>
    {
        #region LogVisitPage

        bool LogVisitPage(int id, LinkType linkType);
        #endregion
    }
}
