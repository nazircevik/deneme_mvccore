using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deneme_mvccore.Fİlters
{
    public class CustomFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           // throw new NotImplementedException();
           //method başladığında
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
            //method bittğinde çalışacak kısım
        }
    }
}
