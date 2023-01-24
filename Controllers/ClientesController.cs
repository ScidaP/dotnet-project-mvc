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

    public ClientesController (IRepositorioClientes repoC, IMapper mapper) {
        _repo = repoC;
        _mapper = mapper;
    }

    public IActionResult CargarClientes() {
        return View();
    }

    [HttpGet]
    public IActionResult MostrarCliente(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { // Los únicos uqe no pueden acceder a esta página son los no logueados
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            Cliente cliente = _repo.getCliente(id);
            MostrarClienteViewModel clienteVM = _mapper.Map<MostrarClienteViewModel>(cliente);
            return View(clienteVM);
        }
    }

    [HttpPost]
    public IActionResult ClienteAgregado(ClienteViewModel ClienteVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            var nuevoCliente = _mapper.Map<Cliente>(ClienteVM);
            _repo.agregarCliente(nuevoCliente);
            TempData["Info"] = "Cliente " + ClienteVM.Nombre + " agregado satisfactoriamente.";
            return RedirectToAction("Info");
        }
    }

    public IActionResult ListarClientes() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            List<Cliente> ListaClientes = _repo.getTodosClientes();
            return View(new ListarClientesViewModel(ListaClientes));
        }
    }

    [HttpGet]
    public IActionResult EliminarCliente(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            _repo.eliminarCliente(id);
            TempData["Info"] = "Cliente N° " + id + " eliminado correctamente.";
            return RedirectToAction("Info");
        }
    }

    [HttpGet]
    public IActionResult ActualizarCliente(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            var ClienteAActualizar = _repo.getCliente(id);
            var ActualizarClienteVM = _mapper.Map<ClienteViewModel>(ClienteAActualizar);
            return View(ActualizarClienteVM);
        }
    }

    [HttpPost]

    public IActionResult ClienteActualizado(ClienteViewModel ClienteVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) { 
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            var clienteActualizado = _mapper.Map<Cliente>(ClienteVM);
            _repo.actualizarCliente(clienteActualizado);
            TempData["Info"] = "Cliente " + ClienteVM.Nombre + " actualizado con éxito.";
            return View("Info");
        }
    }

    public IActionResult Info() {
        return View("Info");
    }
}