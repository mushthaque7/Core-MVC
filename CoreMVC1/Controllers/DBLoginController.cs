using Microsoft.AspNetCore.Mvc;
using CoreMVC1.Models;
namespace CoreMVC1.Controllers
{
    public class DBLoginController : Controller
    {
        EmployeeDB dbobj=new EmployeeDB();
        public IActionResult Login_Load()
        {
            return View();
        }

        public IActionResult Login_click(Employee objcls) {
            if (ModelState.IsValid) { 
                var res=dbobj.LoginDB(objcls);
                if(res== "Success")
                {
                    return RedirectToAction("Profile_Load", "ProfileDB", new {id=objcls.Id});
                }
                else
                {
                    TempData["msg"] = res;
                }
            }
            return View("Login_Load", objcls);
        }
    }
}
