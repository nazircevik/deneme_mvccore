using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using deneme_mvccore.ExtensionMethods;
using deneme_mvccore.Entities;

namespace deneme_mvccore.Controllers
{
    public class SessionDemoController : Controller
    {
        public string Index()
        {
            HttpContext.Session.SetInt32("age", 32);
            HttpContext.Session.SetString("name", "NAzir");
            HttpContext.Session.SetObject("student",new Student { Email="nazir@",FirstName="Nazir",Id=1,LastName="çevik"});
            return "session create";
        }
        public string GetSessions()
        {
            return String.Format("Hello {0},you are {1} Students is{2}",HttpContext.Session.GetInt32("age"),HttpContext.Session.GetString("name"),HttpContext.Session.GetObject<Student>("student").FirstName); 
        }
    }
}
