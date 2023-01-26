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
    private readonly ILogger<CadeteriasController> logger;

    public CadeteriasController(IMapper map, IRepositorioCadeterias repo, ILogger<CadeteriasController> log) {
        repoCadeterias = repo;
        mapper = map;
        logger = log;
    }

    public IActionResult CargarCadeteria() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                return View(new CadeteriaViewModel());
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpPost]

    public IActionResult CadeteriaAgregada(CadeteriaViewModel cadVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                if (ModelState.IsValid) {
                    var cad = mapper.Map<Cadeteria>(cadVM);
                    repoCadeterias.AgregarCadeteria(cad);
                    TempData["Info"] = "Cadetería " + cad.Nombre + " agregada con éxito.";
                    return RedirectToAction("Info");
                } else {
                    return View("CargarCadeteria", cadVM);
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    public IActionResult ActualizarCadeteria(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                var CadeteriaAActualiar = repoCadeterias.GetCadeteria(id);
                var CadeteriaVM = mapper.Map<CadeteriaViewModel>(CadeteriaAActualiar);
                return View(CadeteriaVM);
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    public IActionResult CadeteriaActualizada(CadeteriaViewModel CadVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                if (ModelState.IsValid) {
                    var Cadeteria = mapper.Map<Cadeteria>(CadVM);
                    repoCadeterias.ActualizarCadeteria(Cadeteria);
                    TempData["Info"] = "Cadetería N° " + Cadeteria.Id + " actualizada correctamente.";
                    return RedirectToAction("Info");
                } else {
                    return View("Actualizarcadeteria", CadVM);
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    public IActionResult ListarCadeterias() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
            return View(new ListarCadeteriasViewModel(ListaCadeterias));
        }
    }

    public IActionResult EliminarCadeteria(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                repoCadeterias.EliminarCadeteria(id);
                TempData["Info"] = "Cadeteria N° " + id + " eliminada con éxito";
                return RedirectToAction("Info");
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpGet]
    public IActionResult MostrarCadeteria(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            Cadeteria cad = repoCadeterias.GetCadeteria(id);
            MostrarCadeteriaViewModel cadVM = mapper.Map<MostrarCadeteriaViewModel>(cad);
            return View(cadVM);
        }
    }

    public IActionResult Info() {
        return View();
    }
}