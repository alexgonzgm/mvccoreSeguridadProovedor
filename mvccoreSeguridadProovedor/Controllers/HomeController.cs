﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MvcCoreNuevo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(String username)
        {
            HttpContext.Session.SetString("USERNAME", username);
            return RedirectToAction("Index");
        }

        public IActionResult CloseSession()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
