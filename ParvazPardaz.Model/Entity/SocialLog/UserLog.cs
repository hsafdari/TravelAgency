using ParvazPardaz.Model.Entity.Common;
//using ParvazPardaz.Model.ParvazPardazResource.logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParvazPardaz.Model.Entity.SocialLog
{
    //[Table("Core_UserLogs", Schema = "log")]
    public class UserLog 
    {
        //[Key]
        public long Id { get; set; }

        //[Display(Name = "UserId", ResourceType = typeof(ParvazPardazResource.logs.logResource))]
        public Nullable<int> UserId { get; set; }

        //[Display(Name = "LogDateTime", ResourceType = typeof(ParvazPardazResource.logs.logResource))]
        public Nullable<System.DateTime> LogDateTime { get; set; }

        //[Display(Name = "LastIPAddress", ResourceType = typeof(ParvazPardazResource.logs.logResource))]
        public string IPAddress { get; set; }

        //[Display(Name = "IsLogined", ResourceType = typeof(ParvazPardazResource.logs.logResource))]
        public Nullable<bool> IsLogined { get; set; }
        public string Browser { get; set; }
    }
}
