using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;

namespace Tp4MvcNuevo.Controllers;

public class ClientesController : Controller {

    private readonly IRepositorioClientes _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<ClientesController> logger;

    public ClientesController (IRepositorioClientes repoC, IMapper mapper, ILogger<ClientesController> log) {
        _repo = repoC;
        _mapper = mapper;
        logger = log;
    }

    public IActionResult CargarClientes() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                return View(new ClienteViewModel());
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpGet]
    public IActionResult MostrarCliente(int id) {
        logger.LogInformation("Mostrando Clientes");
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { // Los únicos uqe no pueden acceder a esta página son los no logueados
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            try {
                Cliente cliente = _repo.getCliente(id);
                MostrarClienteViewModel clienteVM = _mapper.Map<MostrarClienteViewModel>(cliente);
                return View(clienteVM);
            } catch (Exception e) {
                logger.LogError("Error al mostrar cliente. -> " + e.ToString());
                TempData["Info"] = "Error al mostrar cliente. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info");
            }
        }
    }

    [HttpPost]
    public IActionResult ClienteAgregado(ClienteViewModel ClienteVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                if (ModelState.IsValid) {
                    try {
                        var nuevoCliente = _mapper.Map<Cliente>(ClienteVM);
                        _repo.agregarCliente(nuevoCliente);
                        TempData["Info"] = "Cliente " + ClienteVM.Nombre + " agregado satisfactoriamente.";
                        return RedirectToAction("Info");
                    } catch (Exception e) {
                        logger.LogError("Error al agregar cliente. -> " + e.ToString());
                        TempData["Info"] = "Error al agregar cliente. Intente nuevamente. " + e.Message;
                        return RedirectToAction("Info");
                    }
                } else {
                    return View("CargarClientes", ClienteVM);
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    public IActionResult ListarClientes() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            try {
                List<Cliente> ListaClientes = _repo.getTodosClientes();
                return View(new ListarClientesViewModel(ListaClientes));
            } catch (Exception e) {
                logger.LogError("Error al listar clientes. -> " + e.ToString());
                TempData["Info"] = "Error al listar clientes. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info");
            }
        }
    }

    [HttpGet]
    public IActionResult EliminarCliente(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    _repo.eliminarCliente(id);
                    TempData["Info"] = "Cliente N° " + id + " eliminado correctamente.";
                    return RedirectToAction("Info");
                } catch (Exception e) {
                    logger.LogError("Error al eliminar cliente. -> " + e.ToString());
                    TempData["Info"] = "Error al eliminar cliente. Intente nuevamente. -> " + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpGet]
    public IActionResult ActualizarCliente(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    var ClienteAActualizar = _repo.getCliente(id);
                    var ActualizarClienteVM = _mapper.Map<ClienteViewModel>(ClienteAActualizar);
                    return View(ActualizarClienteVM);
                } catch (Exception e) {
                    logger.LogError("Error al actualizar cliente. -> "+ e.ToString());
                    TempData["Info"] = "Error al actualizar cliente. Intente nuevamente. ->" + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpPost]

    public IActionResult ClienteActualizado(ClienteViewModel ClienteVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                if (ModelState.IsValid) {
                    try {
                        var clienteActualizado = _mapper.Map<Cliente>(ClienteVM);
                        _repo.actualizarCliente(clienteActualizado);
                        TempData["Info"] = "Cliente " + ClienteVM.Nombre + " actualizado con éxito.";
                        return View("Info");
                    } catch (Exception e) {
                        logger.LogError("Error al actualizar cliente. -> "+ e.ToString());
                        TempData["Info"] = "Error al actualizar cliente. Intente nuevamente. ->" + e.Message;
                        return RedirectToAction("Info");
                    }
                } else {
                    return View("ActualizarCliente", ClienteVM);
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    public IActionResult Info() {
        return View("Info");
    }
}