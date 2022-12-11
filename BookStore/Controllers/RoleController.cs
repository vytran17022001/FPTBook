using System;
using System.Linq;
using System.Web.Mvc;
using BookStore.CustomFilters;
using BookStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        [AuthLog(Roles = "Admin")]
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        [AuthLog(Roles = "Admin")]
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            using (var Role = new ApplicationDbContext())
            {
                var Roles = context.Roles.Find(id);

                if (Role == null)
                {
                    return RedirectToAction("Index");
                }

                context.Roles.Remove(Roles);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
