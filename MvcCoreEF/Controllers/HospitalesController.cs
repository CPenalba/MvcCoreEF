using Microsoft.AspNetCore.Mvc;
using MvcCoreEF.Models;
using MvcCoreEF.Repositories;

namespace MvcCoreEF.Controllers
{
    public class HospitalesController : Controller
    {
        private RepositoryHospital repo;

        public HospitalesController(RepositoryHospital repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Hospital> hospitales = await this.repo.GetHospitalesAsync();
            return View(hospitales);
        }

        public async Task<IActionResult> Details(int idhospital)
        {
            Hospital h = await this.repo.FindHospitalAsync(idhospital);
            return View(h);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hospital h)
        {
            await this.repo.InsertHospitalAsync(h.IdHospital, h.Nombre, h.Direccion, h.Telefono, h.Camas);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int idhospital)
        {
            await this.repo.DeleteHospitalAsync(idhospital);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int idhospital)
        {
            Hospital h = await this.repo.FindHospitalAsync(idhospital);
            return View(h);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Hospital h)
        {
            await this.repo.UpdateHospitalAsync(h.IdHospital, h.Nombre, h.Direccion, h.Telefono, h.Camas);
            return RedirectToAction("Index");
        }
    }
}
