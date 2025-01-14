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
    static ILogger<CadeteriasController> logger;

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
                    try {
                        var cad = mapper.Map<Cadeteria>(cadVM);
                        repoCadeterias.AgregarCadeteria(cad);
                        var mensaje = "Cadetería " + cad.Nombre + " agregada con éxito.";
                        TempData["Info"] = mensaje;
                        logger.LogInformation(mensaje);
                        return RedirectToAction("Info");
                    } catch (Exception e) {
                        logger.LogError("No se pudo agregar la cadetería a la DB -> " + e.ToString());
                        TempData["Info"] = "Error cargando la cadetería. Intente nuevamente -> " + e.Message;
                        return RedirectToAction("Info");
                    }
                } else {
                    return View("CargarCadeteria", cadVM);
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpGet]
    public IActionResult ActualizarCadeteria(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    if (repoCadeterias.ExisteCadeteria(id)) {
                        var CadeteriaAActualiar = repoCadeterias.GetCadeteria(id);
                        var CadeteriaVM = mapper.Map<CadeteriaViewModel>(CadeteriaAActualiar);
                        return View(CadeteriaVM);
                    } else {
                        TempData["Info"] = "No existe la cadetería solicitada.";
                        return RedirectToAction("Info");
                    }
                } catch (Exception e) {
                    logger.LogError("Error al obtener todas las cadeterías. -> " + e.ToString());
                    TempData["Info"] = "Error al cargar página. Intente nuevamente. -> " + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpPost]
    public IActionResult CadeteriaActualizada(CadeteriaViewModel CadVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                if (ModelState.IsValid) {
                    try {
                        var Cadeteria = mapper.Map<Cadeteria>(CadVM);
                        repoCadeterias.ActualizarCadeteria(Cadeteria);
                        var mensaje = "Cadetería " + Cadeteria.Id + " actualizada con éxito.";
                        logger.LogInformation(mensaje);
                        TempData["Info"] = mensaje;
                        return RedirectToAction("Info");
                    } catch (Exception e) {
                        logger.LogError("Error al actualizar la cadetería. --> " + e.ToString());
                        TempData["Info"] = "Error al actualizar la cadetería. Intente nuevamente. -> " + e.Message;
                        return RedirectToAction("Info");
                    }
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
            try {
                List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
                return View(new ListarCadeteriasViewModel(ListaCadeterias));
            } catch (Exception e) {
                logger.LogError("Error al listar cadeterías. -> " + e.ToString());
                TempData["Info"] = "Error al listar las cadeterías. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info");
            }
        }
    }

    [HttpGet]
    public IActionResult EliminarCadeteria(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    if (repoCadeterias.ExisteCadeteria(id)) {
                        repoCadeterias.EliminarCadeteria(id);
                        var mensaje = "Cadetería " + id + " eliminada con éxito.";
                        TempData["Info"] = mensaje;
                        logger.LogInformation(mensaje);
                        return RedirectToAction("Info");
                    } else {
                        TempData["Info"] = "No existe la cadetería solicitada.";
                        return RedirectToAction("Info");
                    }
                } catch (Exception e) {
                    logger.LogError("Error al eliminar cadetería. ->" + e.ToString());
                    TempData["Info"] = "Error al eliminar la cadetería. Intente nuevamente. ->" + e.Message;
                    return RedirectToAction("Info");
                }
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
            try {
                if (repoCadeterias.ExisteCadeteria(id)) {
                    Cadeteria cad = repoCadeterias.GetCadeteria(id);
                    MostrarCadeteriaViewModel cadVM = mapper.Map<MostrarCadeteriaViewModel>(cad);
                    return View(cadVM);
                } else {
                    TempData["Info"] = "No existe la cadetería solicitada.";
                    return RedirectToAction("Info");
                }
            } catch (Exception e) {
                logger.LogError("Error al mostrar cadetería. -> " + e.ToString());
                TempData["Info"] = "Error el mostrar la cadetería. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info");
            }
        }
    }

    public IActionResult Info() {
        return View();
    }
}