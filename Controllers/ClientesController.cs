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

    public IActionResult cargarCliente() {
        return View();
    }

    public IActionResult clienteAgregado(string nombre, string direccion, long telefono, string ref_direcc) {
        Cliente nuevoCliente = new Cliente(nombre, direccion, telefono, ref_direcc);
        _repo.agregarCliente(nuevoCliente);
        TempData["Info"] = "Cliente " + nombre + " agregado satisfactoriamente.";
        return RedirectToAction("Info");
    }

    public IActionResult ListarClientes() {
        List<Cliente> ListaClientes = _repo.getTodosClientes();
        var ListarCadetesViewModel = new ListarClientesViewModel(ListaClientes);
        return View(ListarCadetesViewModel);
    }

}