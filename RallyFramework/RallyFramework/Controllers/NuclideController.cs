using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Framework.Facade;

namespace RallyFramework.Controllers
{
    public class NuclideController : Controller
    {
        private INuclideManager nuclideManager = Facade.CreateNuclideManager();

        // GET: NuclideController
        public ActionResult Index()
        {
            return View(this.nuclideManager.GetNuclides());
        }

        // GET: NuclideController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NuclideController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NuclideController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NuclideController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NuclideController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NuclideController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NuclideController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
