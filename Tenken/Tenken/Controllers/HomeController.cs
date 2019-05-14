﻿using System.Web.Mvc;
using Tenken.Models;

namespace Tenken.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ProductList = ProductAPIController.GetTopProduct("New");
            ViewBag.Size = ViewBag.ProductList.Count;
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}