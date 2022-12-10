using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;

namespace Tp4MvcNuevo.Controllers;

public class CadetesController : Controller {

    private readonly IRepositorioCadetes repoCadetes;
    private readonly IMapper mapper;

    public CadetesController(IRepositorioCadetes repoCad, IMapper mapp) {
        repoCadetes = repoCad;
        mapper = mapp;
    }
    public IActionResult CargarCadete() {
        return View();
    }

    [HttpGet]
    public IActionResult Mostrarcadete(int id) {
        Cadete cad = repoCadetes.getCadete(id);
        var MostrarCadeteVM = mapper.Map<MostrarCadeteViewModel>(cad);
        return View(MostrarCadeteVM);
    }

    [HttpGet]
    public IActionResult ActualizarCadete(int id) {
        Cadete CadeteAActualizar = repoCadetes.getCadete(id);
        return View(CadeteAActualizar);
    }

    [HttpPost]
    public IActionResult CadeteActualizado(string nombre, string direccion, int telefono, int cadeteria, double sueldo) {
        Cadete actualizarCadete = new Cadete(nombre, direccion, telefono, cadeteria, sueldo);
        repoCadetes.actualizarCadete(actualizarCadete);
        TempData["Info"] = "Cadete N° " + actualizarCadete.Id + " actualizado correctamente.";
        return RedirectToAction("Info");
    }

    [HttpPost]
    public IActionResult CadeteAgregado(string nombre, string direccion, int telefono, int cadeteria, double sueldo) {
        Cadete cad = new Cadete(nombre, direccion, telefono, cadeteria, sueldo);
        repoCadetes.agregarCadete(cad);
        TempData["Info"] = "Cadete " + nombre + " agregado satisfactoriamente.";
        return RedirectToAction("Info");
    }
    
    public IActionResult ListarCadetes() {
        List<Cadete> ListaCadetes = repoCadetes.getTodosCadetes();
        var ListarCadetesViewModel = new ListaCadetesViewModel(ListaCadetes);
        return View(ListarCadetesViewModel);
    }
    [HttpGet]
    public IActionResult EliminarCadete(int id) {
        repoCadetes.eliminarCadete(id);
        TempData["Info"] = "Cadete N° " + id + " eliminado correctamente.";
        return RedirectToAction("Info");
    }

    public IActionResult Info() {
        return View();
    }
}