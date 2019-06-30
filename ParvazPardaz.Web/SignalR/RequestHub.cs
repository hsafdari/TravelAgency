using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.SignalR
{
    public class RequestHub : Hub
    {
        [HubMethodName("broadcastData")]
        public void BroadcastData()
        {
            //var id = Context.ConnectionId;
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
            context.Clients.All.updatedData();

        }
        public string InvokeHubMethod()
        {
            return "ConnectionID";  //ConnectionID will the Id as string that you want outside the hub
        }

        static List<UserInfo> UsersList = new List<UserInfo>();
        //static List<messageinfo> MessageList = new List<messageinfo>();

        //-->>>>> ***** Receive Request From Client [  Connect  ] *****
        public void Connect(string tour, string phonenumber, string infantCount, string childCount, string adultCount, string roomtype)
        {
            var id = Context.ConnectionId;

            //Manage Hub Class
            //if freeflag==0 ==> Busy
            //if freeflag==1 ==> Free

            //if tpflag==0 ==> User
            //if tpflag==1 ==> Admin
            UserInfo userInfo = new UserInfo()
            {
                ConnectionId = id,
                AdultCount = adultCount,
                ChildCount = childCount,
                CreateDateTime = DateTime.Now.ToPersianString(PersianDateTimeFormat.LongDateFullTime),
                InfantCount = infantCount,
                PhoneNumber = phonenumber,
                RoomType = roomtype,
                TourPackage = tour,
                UserName = "",
                freeflag = "1",
                tpflag = "0",
            };
            UsersList.Add(userInfo);
            Clients.Caller.onConnected(userInfo);
        }

        public void GetUsers()
        {
            var list = UsersList.ToList();
            Clients.All.userLoaded(list);
        }

        public void AcceptUser()
        {
            var item = UsersList.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                var list = UsersList.ToList();
                Clients.Caller.userDisconnected(list);
            }
            else
            {
                Clients.Caller.userAccepeted(Context.ConnectionId);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = UsersList.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                UsersList.Remove(item);
            }
            return base.OnDisconnected(stopCalled);
        }
    }

    
}

/// <summary>
/// اطلاعات درخواست ها
/// </summary>
public class UserInfo
{
    public string ConnectionId { get; set; }
    public string CreateDateTime { get; set; }
    public string PhoneNumber { get; set; }
    public string InfantCount { get; set; }
    public string ChildCount { get; set; }
    public string AdultCount { get; set; }
    public string RoomType { get; set; }
    public string TourPackage { get; set; }
    public string UserName { get; set; }

    //if freeflag==0 ==> Busy
    //if freeflag==1 ==> Free
    public string freeflag { get; set; }

    //if tpflag==2 ==> User Admin
    //if tpflag==0 ==> User Member
    //if tpflag==1 ==> Admin
    public string tpflag { get; set; }
}

