using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.Controllers;

public class PedidosController : Controller
{
    public IActionResult HacerPedido() {
        return View();
    }

    [HttpPost]
    public IActionResult PedidoAgregado(int numero, string obs, string estado) {
        Pedido nuevoPedido = new Pedido(numero, obs, estado, null);
        return View();
    }
}