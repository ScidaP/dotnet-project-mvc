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
    private readonly ILogger<CadetesController> logger;

    public CadetesController(IRepositorioCadetes repoCadetes1, IRepositorioCadeterias repoCadeterias1, IRepositorioPedidos repoPedidos1, IMapper mapp, ILogger<CadetesController> log) {
        repoCadetes = repoCadetes1;
        repoCadeterias = repoCadeterias1;
        repoPedidos = repoPedidos1;
        mapper = mapp;
        logger = log;
    }
    public IActionResult CargarCadete() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    return View(new CargarCadeteViewModel(repoCadeterias.GetTodasCadeterias()));    
                } catch (Exception e) {
                    logger.LogError("Error desconocido. -> " + e.ToString());
                    TempData["Info"] = "Error desconocido. Cargue la página nuevamente. -> " + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpGet]
    public IActionResult MostrarCadete(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            try {
                if (repoCadetes.existeCadete(id)) {
                    Cadete cad = repoCadetes.getCadete(id);
                    var MostrarCadeteVM = mapper.Map<MostrarCadeteViewModel>(cad);
                    return View(MostrarCadeteVM);
                } else {
                    TempData["Info"] = "No existe el cadete solicitado.";
                    return RedirectToAction("Info");
                }
            } catch (Exception e) {
                logger.LogError("Error al mostrar cadete. -> " + e.ToString());
                TempData["Info"] = "Error al mostrar cadete. Intente nuevamente. -> " + e.Message;
                return View("Info");
            }
        }
    }

    [HttpGet]
    public IActionResult ActualizarCadete(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    if (repoCadetes.existeCadete(id)) {
                        Cadete CadeteAActualizar = repoCadetes.getCadete(id);
                        List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
                        return View(new ActualizarCadeteViewModel(CadeteAActualizar, ListaCadeterias));
                    } else {
                        TempData["Info"] = "No existe el cadete solicitado.";
                        return RedirectToAction("Info");
                    }
                } catch (Exception e) {
                    logger.LogError("Error al actualizar cadete. -> " + e.ToString());
                    TempData["Info"] = "Error al actualizar cadete. Intente nuevamente. -> " + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpPost]
    public IActionResult CadeteActualizado(ActualizarCadeteViewModel CadeteVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try { // usé un solo trycatch ya que teóricamente los trycatch son muy costosos y hay que evitarlos lo más que se pueda..
                    if (ModelState.IsValid) {
                        var cadete = mapper.Map<Cadete>(CadeteVM);
                        repoCadetes.actualizarCadete(cadete);
                        var mensaje = "Cadete " + cadete.Id + " actualizado con éxito.";
                        TempData["Info"] = mensaje;
                        logger.LogInformation(mensaje);
                        return RedirectToAction("Info");
                    } else {
                        CadeteVM.ListaCadeterias1 = repoCadeterias.GetTodasCadeterias();
                        return View("ActualizarCadete", CadeteVM);
                    }
                } catch (Exception e) {
                    logger.LogError("Error desconocido: " + e.ToString());
                    TempData["Info"] = "Error desconocido. Intente nuevamente" + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpPost]
    public IActionResult CadeteAgregado(CargarCadeteViewModel NuevoCadeteVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    if (ModelState.IsValid) {
                        Cadete nuevoCadete = mapper.Map<Cadete>(NuevoCadeteVM);
                        repoCadetes.agregarCadete(nuevoCadete);
                        var mensaje = "Cadete " + nuevoCadete.Nombre + " agregado con éxito.";
                        TempData["Info"] = mensaje;
                        logger.LogInformation(mensaje);
                        return RedirectToAction("Info");
                    } else {
                        NuevoCadeteVM.ListaCadeterias1 = repoCadeterias.GetTodasCadeterias();
                        return View("CargarCadete", NuevoCadeteVM);
                    }
                } catch (Exception e) {
                    logger.LogError("Error desconocido: " + e.ToString());
                    TempData["Info"] = "Error desconocido. Intente nuevamente" + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }
    
    public IActionResult ListarCadetes() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            try {
                List<Cadete> ListaCadetes = repoCadetes.getTodosCadetes();
                List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
                return View(new ListaCadetesViewModel(ListaCadetes, ListaCadeterias));
            } catch (Exception e) {
                logger.LogError("Error al listar cadetes. -> " + e.ToString());
                TempData["Info"] = "Error al listar cadetes. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info");
            }
        }
    }
    [HttpGet]
    public IActionResult EliminarCadete(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    if (repoCadetes.existeCadete(id)) {
                        repoCadetes.eliminarCadete(id);
                        var mensaje = "Cadete N°" + id + " eliminado con éxito.";
                        TempData["Info"] = mensaje;
                        logger.LogInformation(mensaje);
                        return RedirectToAction("Info");
                    } else {
                        TempData["Info"] = "No existe el cadete solicitado.";
                        return RedirectToAction("Info");
                    }
                } catch (Exception e) {
                    logger.LogError("Error al eliminar cadete. -> " + e.ToString());
                    TempData["Info"] = "Error al eliminar cadete. Intente nuevamente. -> " + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    public IActionResult VerPedidosTodosCadetes() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            try {
                List<Cadete> todosCadetes = repoCadetes.getTodosCadetes();
                List<Pedido> todosPedidos = repoPedidos.getTodosPedidos();
                return View(new VerPedidosTodosCadetesViewModel(todosCadetes, todosPedidos));
            } catch (Exception e) {
                logger.LogError("Error al ver pedidos. -> " + e.ToString());
                TempData["Info"] = "Error al ver pedidos. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info"); 
            }
        }
    }

    public IActionResult Info() {
        return View();
    }
}