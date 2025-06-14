using Microsoft.AspNetCore.Mvc;
using CoreMVC1.Models;
namespace CoreMVC1.Controllers
{
    public class DisplayAllController : Controller
    {
        EmployeeDB dbobj=new EmployeeDB();
        public IActionResult Allprofile_page()
        {
            List<Employee> getlist = dbobj.selectDB();
            return View(getlist);
        }
    }
}
