using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mvccoreSeguridadProovedor.Controllers
{
    public class UsuariosController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}