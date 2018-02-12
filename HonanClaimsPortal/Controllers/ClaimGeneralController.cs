using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Claim;
using HonanClaimsWebApi.Services;
using HonanClaimsWebApiAccess1.LoginServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    public class ClaimGeneralController : Controller
    {
        public ActionResult AjaxDoNotShowBanner(string claimId)
        {
            ClaimServices service = new ClaimServices();
            return Json(service.ShowClaimWarningBanner(claimId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxHideBanner(string claimId)
        {
            ClaimTeamLoginModel client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            ClaimServices service = new ClaimServices();
            return Json(service.HideClaimWarningBanner(claimId, client.UserId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxGetPaymentAmount(string claimId,decimal liability_Res_Source,decimal defence_Res_Source,decimal excess)
        {
            PaymentServices paymentServices = new PaymentServices();
            decimal val, liabilityReserveGross = 0, defenceReserveGross = 0;
            ClaimFinObject model = new ClaimFinObject();

            model.Excess = excess;
            model.Liability_Reserve = liability_Res_Source;
            model.Defence_Reserve = defence_Res_Source;

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(claimId, "Liability Reserve", false), out val))
                model.Net_Paid_Liability = val;

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(claimId, "Defence Reserve", false), out val))
                model.Net_Paid_Defence = val;

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(claimId, "Liability Reserve", true), out val))
            {
                liabilityReserveGross = val;
                model.Liability_Reserve = model.Liability_Reserve - model.Net_Paid_Liability;
            }

            if (decimal.TryParse(paymentServices.GetClaimReservePaymentAmount(claimId, "Defence Reserve", true), out val))
            {
                defenceReserveGross = val;
                model.Defence_Reserve = model.Defence_Reserve - model.Net_Paid_Defence;
            }

            model.Total_Reserve = model.Liability_Reserve + model.Defence_Reserve;


            model.Gross_Paid_To_Date = liabilityReserveGross + defenceReserveGross;

            model.Total_Incurred = model.Total_Reserve + model.Net_Paid_Liability + model.Net_Paid_Defence;

            if (model.Total_Reserve < model.Excess)
                model.Current_Exposure = model.Total_Reserve;
            else
                model.Current_Exposure = model.Excess - model.Net_Paid_Liability - model.Net_Paid_Defence;

            decimal totalNet = model.Net_Paid_Liability + model.Net_Paid_Defence;
            if (totalNet > model.Excess)
                model.Over_Excess_Paid = totalNet - model.Excess;
            else
                model.Over_Excess_Paid = 0;

            //Update Claim DB
            ClaimTeamLoginModel client = Session[SessionHelper.claimTeamLogin] as ClaimTeamLoginModel;
            paymentServices.TeamUpdateFinancials(liability_Res_Source, defence_Res_Source, claimId, client.UserId,model.Net_Paid_Liability,model.Net_Paid_Defence);

            return Json(model, JsonRequestBehavior.AllowGet);
        }



    }
}