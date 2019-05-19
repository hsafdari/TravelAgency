//using ParvazPardaz.Model.ParvazPardazResource.logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParvazPardaz.Model.Entity.SocialLog
{
    //[Table("Core_UserUnSuccessLogs", Schema = "log")]
    public class UserUnSuccessLog
    {
        public UserUnSuccessLog()
        {

        }
        //[Key]
        public long Id { get; set; }
        //[MaxLength(50)]
        //[DisplayName(logResource.UserName)]
        //[Display(Name = "UserName", ResourceType = typeof(ParvazPardazResource.logs.logResource))]
        public string UserName { get; set; }

        //[MaxLength(50)]
        //[DisplayName(logResource.Password)]
        //[Display(Name = "Password", ResourceType = typeof(ParvazPardazResource.logs.logResource))]
        public string Password { get; set; }

        //[MaxLength(50)]
        //[DisplayName(logResource.IPAddress)]
        //[Display(Name = "IPAddress", ResourceType = typeof(ParvazPardazResource.logs.logResource))]
        public string IPAddress { get; set; }

        //DisplayName(logResource.RequestTime)]
        //[Display(Name = "RequestTime", ResourceType = typeof(ParvazPardazResource.logs.logResource))]
        public System.DateTime RequestTime { get; set; }
        public string Browser { get; set; }
    }
}
