using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;

namespace Tp4MvcNuevo.Controllers;

public class CadeteriasController : Controller {
    private readonly IRepositorioCadeterias repoCadeterias;
    private readonly IMapper mapper;

    public CadeteriasController(IMapper map, IRepositorioCadeterias repo) {
        repoCadeterias = repo;
        mapper = map;
    }

    public IActionResult CargarCadeteria() {
        return View();
    }

    [HttpPost]

    public IActionResult CadeteriaAgregada(Cadeteria cad) {
        repoCadeterias.AgregarCadeteria(cad);
        TempData["Info"] = "Cadetería " + cad.Nombre + " agregada con éxito.";
        return RedirectToAction("Info");
    }

    public IActionResult ListarCadeterias() {
        List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
        ListarCadeteriasViewModel ListarCadVM = new ListarCadeteriasViewModel(ListaCadeterias);
        return View(ListarCadVM);
    }

    public IActionResult EliminarCadeteria(int id) {
        repoCadeterias.EliminarCadeteria(id);
        TempData["Info"] = "Cadeteria N° " + id + " eliminada con éxito";
        return RedirectToAction("Info");
    }

    public IActionResult Info() {
        return View();
    }
}