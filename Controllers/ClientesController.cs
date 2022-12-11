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

    [HttpPost]
    public IActionResult ClienteAgregado(ClienteViewModel ClienteVM) {
        var nuevoCliente = _mapper.Map<Cliente>(ClienteVM);
        _repo.agregarCliente(nuevoCliente);
        TempData["Info"] = "Cliente " + ClienteVM.Nombre + " agregado satisfactoriamente.";
        return RedirectToAction("Info");
    }

    public IActionResult ListarClientes() {
        List<Cliente> ListaClientes = _repo.getTodosClientes();
        var ListarCadetesViewModel = new ListarClientesViewModel(ListaClientes);
        return View(ListarCadetesViewModel);
    }

    [HttpGet]
    public IActionResult EliminarCliente(int id) {
        _repo.eliminarCliente(id);
        TempData["Info"] = "Cliente N° " + id + " eliminado correctamente.";
        return RedirectToAction("Info");
    }

    [HttpGet]
    public IActionResult ActualizarCliente(int id) {
        var ClienteAActualizar = _repo.getCliente(id);
        var ActualizarClienteVM = _mapper.Map<ClienteViewModel>(ClienteAActualizar);
        return View(ActualizarClienteVM);
    }

    [HttpPost]

    public IActionResult ClienteActualizado(ClienteViewModel ClienteVM) {
        var clienteActualizado = _mapper.Map<Cliente>(ClienteVM);
        _repo.actualizarCliente(clienteActualizado);
        TempData["Info"] = "Cliente " + ClienteVM.Nombre + " actualizado con éxito.";
        return View("Info");
    }

    public IActionResult Info() {
        return View("Info");
    }
}