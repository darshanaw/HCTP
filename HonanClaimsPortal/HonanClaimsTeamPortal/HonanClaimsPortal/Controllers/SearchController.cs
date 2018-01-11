using HonanClaimsPortal.Helpers;
using HonanClaimsWebApi.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace HonanClaimsPortal.Controllers
{
    [AuthorizeUser]
    public class SearchController : Controller
    {
        public ActionResult _CommonSearch()
        {
            SearchModel searchModel = new SearchModel();
            searchModel.ClaimPropertyList = new List<SelectListItem>() {
                new SelectListItem(){Text = "Search All",Value = "Search All"},
                new SelectListItem(){Text = "Claims",Value = "Claims"},
                new SelectListItem(){Text = "Policies",Value = "Policies"},
                new SelectListItem(){Text = "Accounts",Value = "Accounts"},
                new SelectListItem(){Text = "Contacts",Value = "Contacts"}};

            if (TempData["SearchField"] != null)
                searchModel.CommonSearchProperty = TempData["SearchField"] as string;

            if (TempData["SearchValue"] != null)
                searchModel.CommonSearchText = TempData["SearchValue"] as string;

            return PartialView(searchModel);
        }

        public ActionResult SearchResults()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SearchResults(SearchModel model)
        {
            TempData["SearchField"] = model.CommonSearchProperty;
            TempData["SearchValue"] = model.CommonSearchText;
            return View(model);
        }

    }
}