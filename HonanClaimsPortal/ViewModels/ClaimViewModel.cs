using HonanClaimsWebApi.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonanClaimsPortal.ViewModels
{
    public class ClaimViewModel
    {
        public List<ClaimNotificationSimple> ClaimList { get; set; }
    }
}