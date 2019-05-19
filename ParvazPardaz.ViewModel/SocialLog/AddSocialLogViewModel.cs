using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.SocialLog
{
    public class AddSocialLogViewModel : BaseViewModelId
    {
        #region Properties
        public int TypeId { get; set; }
        public LinkType LinkType { get; set; }

        #endregion
    }
}
