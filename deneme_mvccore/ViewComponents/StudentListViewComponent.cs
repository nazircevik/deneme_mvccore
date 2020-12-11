using deneme_mvccore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deneme_mvccore.ViewComponents
{
    public class StudentListViewComponent:ViewComponent
    {
        SchoolContext _context;
        public StudentListViewComponent(SchoolContext context)
        {
            _context = context;
        }
        public ViewViewComponentResult Invoke(string filter)
        {
            return View(new StudentListViewModel
            {
                Students = _context.Students.Where(s=>s.FirstName.ToLower().Contains(filter.ToLower())).ToList(),
            }); ;
        }
    }
}
