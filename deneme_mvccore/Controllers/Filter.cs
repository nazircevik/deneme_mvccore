using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using deneme_mvccore.Fİlters;
using Microsoft.AspNetCore.Mvc;

namespace deneme_mvccore.Controllers
{
    public class Filter : Controller
    {
        [CustomFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}
