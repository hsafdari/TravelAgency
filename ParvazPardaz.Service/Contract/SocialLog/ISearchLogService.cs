using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.SocialLog
{
    public interface ISearchLogService: IBaseService<ParvazPardaz.Model.Entity.SocialLog.SearchLog>
    {
        IQueryable<GridSearchLogViewModel> GetViewModelForGrid();
    }
}
