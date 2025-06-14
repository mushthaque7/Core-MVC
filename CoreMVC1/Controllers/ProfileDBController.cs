using Microsoft.AspNetCore.Mvc;
using CoreMVC1.Models;
namespace CoreMVC1.Controllers
{
    public class ProfileDBController : Controller
    {
        EmployeeDB dbobj = new EmployeeDB();
        Employee objcls=new Employee();
        public IActionResult Profile_Load(int id)
        {
            Employee emp = dbobj.ProfileDB(id);
            TempData["uid"] = id;
            return View(emp);
        }
        public IActionResult Update_click(Employee objcls)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int i = dbobj.UpdateProfile(Convert.ToInt32(TempData["uid"]) ,objcls);
                    if (i == 1)
                    {
                        TempData["msg1"] = "Updated Successfully";
                    }
                }
                catch (Exception ex) {
                    TempData["msg1"] = ex.Message.ToString();
                }
                
            }
            return View("Profile_Load");
        }
    }
}
