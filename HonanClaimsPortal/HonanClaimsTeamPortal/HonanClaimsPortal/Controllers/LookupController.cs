using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models;
using HonanClaimsWebApi.Models.Common;
using HonanClaimsWebApi.Models.LookupModel;
using HonanClaimsWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class LookupController : Controller
    {
        LookupServices lookupServices;
        PicklistServicecs picklistServicecs;
        // GET: Lookup
        public ActionResult AccountAjaxHandler(jQueryDataTableParamModel param)
        {
            lookupServices = new LookupServices();
            List<AccountSimpleModel> objectList = new List<AccountSimpleModel>();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                objectList =  lookupServices.GetAccounts(param.sSearch,"Customer");
            }

            IEnumerable<AccountSimpleModel> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<AccountSimpleModel, string> orderingFunction = (c => sortColumnIndex == 1 ? c.AccountId :
                                                                sortColumnIndex == 2 ? c.AccountName :
                                                                sortColumnIndex == 3 ? c.AccountType :
                                                                sortColumnIndex == 4 ? c.AccountManager :                                                                
                                                                c.AccountName);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.AccountName.ToUpper().Contains(param.sSearch.ToUpper()));
                //           ||
                //           c.Town.Contains(param.sSearch));
            }



            List<string[]> aData = new List<string[]>();

            foreach (AccountSimpleModel item in filteredRecords)
            {
                string[] arry = new string[] { item.AccountId, item.AccountName, item.AccountType, item.AccountManager,item.BillingMethod };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }


        public ActionResult AccountCustomAjaxHandler(jQueryDataTableParamModel param,string accountType)
        {
            lookupServices = new LookupServices();
            List<AccountSimpleModel> objectList = new List<AccountSimpleModel>();

            if (!string.IsNullOrEmpty(accountType))
            {
                objectList = lookupServices.GetAccounts(param.sSearch, accountType);
            }

            IEnumerable<AccountSimpleModel> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<AccountSimpleModel, string> orderingFunction = (c => sortColumnIndex == 1 ? c.AccountId :
                                                                sortColumnIndex == 2 ? c.AccountName :
                                                                sortColumnIndex == 3 ? c.AccountType :
                                                                sortColumnIndex == 4 ? c.AccountManager :
                                                                c.AccountName);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.AccountName.ToUpper().Contains(param.sSearch.ToUpper()));
                //           ||
                //           c.Town.Contains(param.sSearch));
            }



            List<string[]> aData = new List<string[]>();

            foreach (AccountSimpleModel item in filteredRecords)
            {
                string[] arry = new string[] { item.AccountId, item.AccountName, item.AccountType, item.AccountManager, item.BillingMethod };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }

        public ActionResult AccountOcAjaxHandler(jQueryDataTableParamModel param)
        {
            lookupServices = new LookupServices();
            List<AccountSimpleModel> objectList = new List<AccountSimpleModel>();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                objectList = lookupServices.GetAccounts(param.sSearch, "");
            }

            IEnumerable<AccountSimpleModel> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<AccountSimpleModel, string> orderingFunction = (c => sortColumnIndex == 1 ? c.AccountId :
                                                                sortColumnIndex == 2 ? c.AccountName :
                                                                sortColumnIndex == 3 ? c.AccountType :
                                                                sortColumnIndex == 4 ? c.AccountManager :
                                                                c.AccountName);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.AccountName.ToUpper().Contains(param.sSearch.ToUpper()));
                //           ||
                //           c.Town.Contains(param.sSearch));
            }



            List<string[]> aData = new List<string[]>();

            foreach (AccountSimpleModel item in filteredRecords)
            {
                string[] arry = new string[] { item.AccountId, item.AccountName, item.AccountType, item.AccountManager, item.BillingMethod };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }


        public ActionResult OcAjaxHandler(jQueryDataTableParamModel param)
        {
            lookupServices = new LookupServices();
            List<CRMOCNumSimple> objectList = new List<CRMOCNumSimple>();
            
            //if (!string.IsNullOrEmpty(param.sSearch))
            //{
                objectList =
                    lookupServices.GetOCNumLookup(param.sSearch, "");
           // }

            IEnumerable<CRMOCNumSimple> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<CRMOCNumSimple, string> orderingFunction = (c => sortColumnIndex == 1 ? c.OCNum :
                                                                sortColumnIndex == 2 ? c.Address1 :
                                                                sortColumnIndex == 3 ? c.Address2 :                                                               
                                                                sortColumnIndex == 4 ? c.State :
                                                                sortColumnIndex == 4 ? c.Suburb :
                                                                 sortColumnIndex == 4 ? c.Postcode :
                                                                c.OCNum);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.OCNum.ToUpper().Contains(param.sSearch.ToUpper()));
                //           ||
                //           c.Town.Contains(param.sSearch));
            }



            List<string[]> aData = new List<string[]>();

            foreach (CRMOCNumSimple item in filteredRecords)
            {
                string[] arry = new string[] { item.OCNum, item.Address };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }

        public ActionResult PolicyAjaxHandler(jQueryDataTableParamModel param, string accountId)
        {
            lookupServices = new LookupServices();
            List<PolicySimple> objectList = new List<PolicySimple>();
            
            //if (Session["PolicyobjectList"] == null)
            //{
            objectList =
                lookupServices.GetPolicies(param.sSearch, accountId);
            //}
            //else
            //    objectList = Session["PolicyobjectList"] as List<PolicySimple>;


            IEnumerable<PolicySimple> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<PolicySimple, string> orderingFunction = (c => sortColumnIndex == 1 ? c.PolicyNo :
                                                                sortColumnIndex == 2 ? c.PolicyClass :
                                                                sortColumnIndex == 3 ? c.PolicyStatus :
                                                                c.PolicyExpiry);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.PolicyNo.ToUpper().Contains(param.sSearch.ToUpper())
                                    ||
                          c.PolicyClass.ToUpper().Contains(param.sSearch.ToUpper())
                          ||
                          c.PolicyExpiry.ToUpper().Contains(param.sSearch.ToUpper()));
                //           ||
                //           c.Town.Contains(param.sSearch));
            }


            List<string[]> aData = new List<string[]>();

            foreach (PolicySimple item in filteredRecords)
            {
                string[] arry = new string[] { item.PolicyId, item.PolicyNo, item.PolicyClass, item.PolicyStatus, item.Address1, item.Address2, item.Suburb, item.State, item.Postcode, item.PolicyExpiry, item.AccountManager,
                    item.PeriodFrom != null ? DateTime.Parse(item.PeriodFrom.ToString()).ToShortDateString() : "" 
                    , item.PeriodTo != null ? DateTime.Parse(item.PeriodTo.ToString()).ToShortDateString() : "", item.Excess.ToString(),item.UnderwriterId,item.UnderwriterName,item.Insured_Name};

                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPolicyByAccountId(string accountId)
        {
            lookupServices = new LookupServices();
            List<PolicySimple> objectList = new List<PolicySimple>();

            objectList =
                    lookupServices.GetPolicies("", accountId);

            if (objectList.Count() == 1)
            {
                string[] arry = new string[] { objectList.FirstOrDefault().PolicyId, objectList.FirstOrDefault().PolicyNo, objectList.FirstOrDefault().PolicyClass, objectList.FirstOrDefault().PolicyExpiry, objectList.FirstOrDefault().Address1, objectList.FirstOrDefault().Address2, objectList.FirstOrDefault().Suburb, objectList.FirstOrDefault().State, objectList.FirstOrDefault().Postcode, objectList.FirstOrDefault().AccountManager };


                return Json(new
                {
                    arry

                }, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        public ActionResult AssingToAjaxHandler(jQueryDataTableParamModel param, string teamName)
        {
            picklistServicecs = new PicklistServicecs();
            List<CRMPicklistItem> objectList = new List<CRMPicklistItem>();
            teamName = string.IsNullOrEmpty(teamName) ? "-" : teamName;
            //if (Session["PolicyobjectList"] == null)
            //{
            objectList =
               picklistServicecs.GetTeamGetUserOfTeam(teamName);
            //}
            //else
            //    objectList = Session["PolicyobjectList"] as List<PolicySimple>;


            IEnumerable<CRMPicklistItem> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<CRMPicklistItem, string> orderingFunction = (c => sortColumnIndex == 1 ? c.Code :
                                                                c.Text);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.Text.ToUpper().Contains(param.sSearch.ToUpper()));
                //           c.Town.Contains(param.sSearch));
            }


            List<string[]> aData = new List<string[]>();

            foreach (CRMPicklistItem item in filteredRecords)
            {
                string[] arry = new string[] { item.Text, item.Code };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult StoreAjaxHandler(jQueryDataTableParamModel param, string AccountId)
        {
            lookupServices = new LookupServices();
            List<StoreSimple> objectList = new List<StoreSimple>();

            objectList =
                lookupServices.GetStores("", AccountId);
            Session[SessionHelper.StoreobjectList] = objectList;

            IEnumerable<StoreSimple> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<StoreSimple, string> orderingFunction = (c => sortColumnIndex == 1 ? c.StoreName :
                                                                sortColumnIndex == 2 ? c.Address :
                                                                c.StoreName);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.StoreName.ToUpper().Contains(param.sSearch.ToUpper())
                                    ||
                          c.Address.ToUpper().Contains(param.sSearch.ToUpper()));
                //           ||
                //           c.Town.Contains(param.sSearch));
            }


            List<string[]> aData = new List<string[]>();

            foreach (StoreSimple item in filteredRecords)
            {
                string[] arry = new string[] { item.StoreName, item.Address,item.Region };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }

        public ActionResult ContactAjaxHandler(jQueryDataTableParamModel param, string accountId)
        {
            lookupServices = new LookupServices();
            List<CRMContactSimple> objectList = new List<CRMContactSimple>();

            objectList =
                lookupServices.GetContactLookup(param.sSearch == null ? "" : param.sSearch, accountId);


            IEnumerable<CRMContactSimple> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<CRMContactSimple, string> orderingFunction = (c => sortColumnIndex == 1 ? c.ContactName :
                                                                sortColumnIndex == 2 ? c.FirstName :
                                                                sortColumnIndex == 3 ? c.LastName :
                                                                c.ContactName);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.LastName.ToUpper().Contains(param.sSearch.ToUpper())
                                    ||
                          c.ContactName.ToUpper().Contains(param.sSearch.ToUpper()));
                //           ||
                //           c.Town.Contains(param.sSearch));
            }


            List<string[]> aData = new List<string[]>();

            foreach (CRMContactSimple item in filteredRecords)
            {
                string[] arry = new string[] { item.LastName, item.FirstName, item.ContactName, item.AccountId,item.AccountName,item.ContactId,item.Email, item.Phone  };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<ActionResult> GetPoliciesAjaxHandler(string filter,string accountId)
        {
            lookupServices = new LookupServices();
            List<PolicySimple> list = new List<PolicySimple>();
            list = lookupServices.GetPolicies(filter, accountId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersAjaxHandler(string filter, string teamName)
        {
            PicklistServicecs picklistService = new PicklistServicecs();
            List<CRMPicklistItem> list = new List<CRMPicklistItem>();
            list = picklistService.GetTeamGetUserOfTeamAutoComplete(teamName, filter);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConvertToClaimAjax(string claimId, string policyNo, string assignedUserId, string teamName)
        {
            HonanClaimsWebApiAccess1.LoginServices.ClaimTeamLoginModel login = Session[SessionHelper.claimTeamLogin] as HonanClaimsWebApiAccess1.LoginServices.ClaimTeamLoginModel;
            ClaimServices claimServices = new ClaimServices();
            return Json(claimServices.ConvertNotificationToClaim(login.UserId, claimId, policyNo, assignedUserId, teamName), JsonRequestBehavior.AllowGet);
        }


        public ActionResult BrokerAjaxHandler(jQueryDataTableParamModel param)
        {
            lookupServices = new LookupServices();
            List<AccountSimpleModel> objectList = new List<AccountSimpleModel>();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                objectList = lookupServices.GetAccounts(param.sSearch, "");
            }

            IEnumerable<AccountSimpleModel> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<AccountSimpleModel, string> orderingFunction = (c => sortColumnIndex == 1 ? c.AccountId :
                                                                sortColumnIndex == 2 ? c.AccountName :
                                                                sortColumnIndex == 3 ? c.AccountType :
                                                                sortColumnIndex == 4 ? c.AccountManager :
                                                                c.AccountName);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.AccountName.ToUpper().Contains(param.sSearch.ToUpper()));
                //           ||
                //           c.Town.Contains(param.sSearch));
            }



            List<string[]> aData = new List<string[]>();

            foreach (AccountSimpleModel item in filteredRecords)
            {
                string[] arry = new string[] { item.AccountId, item.AccountName, item.AccountType, item.AccountManager, item.BillingMethod };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }

        public ActionResult AllAccountAjaxHandler(jQueryDataTableParamModel param)
        {
            lookupServices = new LookupServices();
            List<AccountSimpleModel> objectList = new List<AccountSimpleModel>();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                objectList = lookupServices.GetAccounts(param.sSearch, "");
            }

            IEnumerable<AccountSimpleModel> filteredRecords = objectList;

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<AccountSimpleModel, string> orderingFunction = (c => sortColumnIndex == 1 ? c.AccountId :
                                                                sortColumnIndex == 2 ? c.AccountName :
                                                                sortColumnIndex == 3 ? c.AccountType :
                                                                sortColumnIndex == 4 ? c.AccountManager :
                                                                c.AccountName);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredRecords = filteredRecords.OrderBy(orderingFunction);
            else
                filteredRecords = filteredRecords.OrderByDescending(orderingFunction);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredRecords = filteredRecords
                            .Where(c => c.AccountName.ToUpper().Contains(param.sSearch.ToUpper()));
                //           ||
                //           c.Town.Contains(param.sSearch));
            }



            List<string[]> aData = new List<string[]>();

            foreach (AccountSimpleModel item in filteredRecords)
            {
                string[] arry = new string[] { item.AccountId, item.AccountName, item.AccountType, item.AccountManager, item.BillingMethod };
                aData.Add(arry);
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = aData

            },

            JsonRequestBehavior.AllowGet);

        }


    }
}