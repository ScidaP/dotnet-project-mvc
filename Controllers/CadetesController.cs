using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;

namespace Tp4MvcNuevo.Controllers;

public class CadetesController : Controller {

    private readonly IRepositorioCadetes repoCadetes;
    private readonly IRepositorioCadeterias repoCadeterias;
    private readonly IRepositorioPedidos repoPedidos;
    private readonly IMapper mapper;

    public CadetesController(IRepositorioCadetes repoCadetes1, IRepositorioCadeterias repoCadeterias1, IRepositorioPedidos repoPedidos1, IMapper mapp) {
        repoCadetes = repoCadetes1;
        repoCadeterias = repoCadeterias1;
        repoPedidos = repoPedidos1;
        mapper = mapp;
    }
    public IActionResult CargarCadete() {
        CargarCadeteViewModel CargarCadeteVM = new CargarCadeteViewModel(repoCadeterias.GetTodasCadeterias());
        return View(CargarCadeteVM);
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
        List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
        ActualizarCadeteViewModel ActualizarCadeteVM = new ActualizarCadeteViewModel(CadeteAActualizar, ListaCadeterias);
        return View(ActualizarCadeteVM);
    }

    [HttpPost]
    public IActionResult CadeteActualizado(int id, string nombre, string direccion, int telefono, int cadeteria, double sueldo) {
        Cadete actualizarCadete = new Cadete(id, nombre, direccion, telefono, cadeteria, sueldo);
        repoCadetes.actualizarCadete(actualizarCadete);
        TempData["Info"] = "Cadete N° " + actualizarCadete.Id + " actualizado correctamente.";
        return RedirectToAction("Info");
    }

    [HttpPost]
    public IActionResult CadeteAgregado(CargarCadeteViewModel NuevoCadeteVM) {
        Cadete nuevoCadete = mapper.Map<Cadete>(NuevoCadeteVM);
        repoCadetes.agregarCadete(nuevoCadete);
        TempData["Info"] = "Cadete " + nuevoCadete.Nombre + " agregado satisfactoriamente.";
        return RedirectToAction("Info");
    }
    
    public IActionResult ListarCadetes() {
        List<Cadete> ListaCadetes = repoCadetes.getTodosCadetes();
        List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
        var ListarCadetesViewModel = new ListaCadetesViewModel(ListaCadetes, ListaCadeterias);
        return View(ListarCadetesViewModel);
    }
    [HttpGet]
    public IActionResult EliminarCadete(int id) {
        repoCadetes.eliminarCadete(id);
        TempData["Info"] = "Cadete N° " + id + " eliminado correctamente.";
        return RedirectToAction("Info");
    }

    public IActionResult VerPedidosCadete() {
        List<Cadete> todosCadetes = repoCadetes.getTodosCadetes();
        List<Pedido> todosPedidos = repoPedidos.getTodosPedidos();
        var VerPedidosCadeteVM = new VerPedidosCadeteViewModel(todosCadetes, todosPedidos);
        return View(VerPedidosCadeteVM);
    }

    public IActionResult Info() {
        return View();
    }
}