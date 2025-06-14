using Microsoft.AspNetCore.Mvc;
using CoreMVC1.Models;
namespace CoreMVC1.Controllers
{
    public class DBInsertController : Controller
    {
        EmployeeDB dbobj=new EmployeeDB();
        public IActionResult Insert_Load()
        {
            return View();
        }
        public IActionResult Insert_click(Employee objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = dbobj.InsertDB(objcls);
                    TempData["msg"] = res;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"]=ex.Message;
            }
            return View("Insert_Load",objcls);  
        }
    }
}
