using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zoologico.ApiConsumer;
using Zoologico.Modelos;

namespace Zooologico.MVC.Controllers
{
    public class EspeciesController : Controller
    {
        // GET: EspeciesController
        public ActionResult Index()
        {
            var data = Crud<Especie>.ReadAll();
            var totoal = data.Data.Count;
            ViewBag.Total = totoal;
            ViewData["Total"] = totoal;
            ViewBag.Promedio = data.Data.Average(e => e.Codigo);
            ViewBag.Minimo = data.Data.Min(e => e.Codigo);
            ViewBag.Maximo = data.Data.Max(e => e.Codigo);

            return View(data.Data);
        }

        // GET: EspeciesController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Especie>.ReadBy("codigo", id.ToString());
            return View(data.Data);
        }

        // GET: EspeciesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EspeciesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Especie data)
        {
            try
            {
                Crud<Especie>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EspeciesController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Especie>.ReadBy("codigo", id.ToString());
            return View(data.Data);
        }

        // POST: EspeciesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Especie data)
        {
            try
            {
                Crud<Especie>.Update(id.ToString(), data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EspeciesController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Especie>.ReadBy("codigo", id.ToString());
            return View(data.Data);
        }

        // POST: EspeciesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Especie data)
        {
            try
            {
                Crud<Especie>.Delete(id.ToString());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
