using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.Controllers;

public class PedidosController : Controller
{
    internal static List<Pedido> ListaPedidos = new List<Pedido>();
    public IActionResult HacerPedido() {
        return View();
    }

    [HttpPost]
    public IActionResult PedidoAgregado(int numero, string obs, string estado) {
        Pedido nuevoPedido = new Pedido(numero, obs, estado, null);
        ListaPedidos.Add(nuevoPedido);
        return View();
    }

    public IActionResult ListarPedidos() {
        return View(ListaPedidos);
    }

    public IActionResult PedidoAgregado() {
        return View();
    }
}