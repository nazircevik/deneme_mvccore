using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using deneme_mvccore.Entities;
using deneme_mvccore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace deneme_mvccore.Pages.Studentp
{
    public class IndexModel : PageModel
    {
        public List<Student> Students { get; set; }
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }
        public void OnGet(string search)
        {
            Students = string.IsNullOrEmpty(search)
                ?_context.Students.ToList():
                _context.Students.Where(p => p.FirstName.ToLower().Contains(search.ToLower())).ToList();

        }
        [BindProperty]
        public Student Student { get; set; }
        public IActionResult OnPost()
        {
            _context.Students.Add(Student);
            _context.SaveChanges();
            return RedirectToPage("/Studentp/Index");
        }
    }
}
