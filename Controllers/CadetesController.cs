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
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 2 || Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            return View(new CargarCadeteViewModel(repoCadeterias.GetTodasCadeterias()));
        }
    }

    [HttpGet]
    public IActionResult Mostrarcadete(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 2 || Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            Cadete cad = repoCadetes.getCadete(id);
            var MostrarCadeteVM = mapper.Map<MostrarCadeteViewModel>(cad);
            return View(MostrarCadeteVM);
        }
    }

    [HttpGet]
    public IActionResult ActualizarCadete(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 2 || Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            Cadete CadeteAActualizar = repoCadetes.getCadete(id);
            List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
            return View(new ActualizarCadeteViewModel(CadeteAActualizar, ListaCadeterias));
        }
    }

    [HttpPost]
    public IActionResult CadeteActualizado(int id, string nombre, string direccion, int telefono, int cadeteria, double sueldo) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 2 || Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            repoCadetes.actualizarCadete(new Cadete(id, nombre, direccion, telefono, cadeteria, sueldo));
            TempData["Info"] = "Cadete N° " + id + " actualizado correctamente.";
            return RedirectToAction("Info");
        }
    }

    [HttpPost]
    public IActionResult CadeteAgregado(CargarCadeteViewModel NuevoCadeteVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 2 || Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            Cadete nuevoCadete = mapper.Map<Cadete>(NuevoCadeteVM);
            repoCadetes.agregarCadete(nuevoCadete);
            TempData["Info"] = "Cadete " + nuevoCadete.Nombre + " agregado satisfactoriamente.";
            return RedirectToAction("Info");
        }
    }
    
    public IActionResult ListarCadetes() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 2 || Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            List<Cadete> ListaCadetes = repoCadetes.getTodosCadetes();
            List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
            return View(new ListaCadetesViewModel(ListaCadetes, ListaCadeterias));
        }
    }
    [HttpGet]
    public IActionResult EliminarCadete(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 2 || Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            repoCadetes.eliminarCadete(id);
            TempData["Info"] = "Cadete N° " + id + " eliminado correctamente.";
            return RedirectToAction("Info");
        }
    }

    public IActionResult VerPedidosTodosCadetes() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 2 || Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            List<Cadete> todosCadetes = repoCadetes.getTodosCadetes();
            List<Pedido> todosPedidos = repoPedidos.getTodosPedidos();
            return View(new VerPedidosTodosCadetesViewModel(todosCadetes, todosPedidos));
        }
    }

    public IActionResult Info() {
        return View();
    }
}