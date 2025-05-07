using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Extension;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class OfferController : BaseController
    {
        // GET: Offer
        public ActionResult Index()
        {
            SessionStatus();
            var u = new UserMinimal(System.Web.HttpContext.Current.GetMySessionObject());
            return View(u);
        }
    }
}