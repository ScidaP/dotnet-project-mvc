using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Text;
using Tp4MvcNuevo.ViewModels;

namespace Tp4MvcNuevo.Controllers;

public class PedidosController : Controller
{
    internal static List<Pedido> ListaPedidos = new List<Pedido>();
    internal static List<Cliente> ListaClientes = new List<Cliente>();
    internal static List<Cadete> ListaCadetes = new List<Cadete>();

    internal static int contadorPedidos = 0;

    public IActionResult ListarPedidos() {
        ListarPedidosViewModels ViewModel = new ListarPedidosViewModels(ListaPedidos, ListaCadetes);
        return View(ViewModel);
    }

    public IActionResult PedidoAgregado() {
        return View();
    }
    [HttpGet]
    public IActionResult EliminarPedido(int id) {
        var pedidoBuscado = ListaPedidos.Find(pedido => pedido.Numero.Equals(id));
        if (pedidoBuscado == null) {
            // Mostrar mensaje de error
        } else {
            ListaPedidos.Remove(pedidoBuscado);
        }
        ViewData["idPedidoEliminado"] = id;
        return View();
    }
}