using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWithAngular.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        public List<Customer> Get()
        {
            return new List<Customer>
            { 
             new Customer{Id=1,FirstName="Nazir",Name="Çevik"},
                new Customer{Id=2,FirstName="ahmet",Name="hamdi"},
                   new Customer{Id=3,FirstName="Mustafa",Name="Çevik"}
            };
        }
    }
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
    }
}
