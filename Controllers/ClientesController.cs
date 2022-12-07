using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;
using Tp4MvcNuevo.ViewModels;

namespace Tp4MvcNuevo.Controllers;

public class ClientesController : Controller {

    private readonly IRepositorioClientes repoClientes;

    public ClientesController (IRepositorioClientes repoC) {
        repoClientes = repoC;
    }

    public IActionResult cargarCliente() {
        return View();
    }

    public IActionResult clienteAgregado(string nombre, string direccion, long telefono, string ref_direcc) {
        Cliente nuevoCliente = new Cliente(nombre, direccion, telefono, ref_direcc);
        repoClientes.agregarCliente(nuevoCliente);
        TempData["Info"] = "Cliente " + nombre + " agregado satisfactoriamente.";
        return RedirectToAction("Info");
    }

}