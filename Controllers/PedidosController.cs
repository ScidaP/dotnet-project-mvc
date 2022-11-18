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
    
    public IActionResult HacerPedido() {
        LoadCadetes(ListaCadetes);
        return View();
    }

    [HttpPost]
    public IActionResult PedidoAgregado(string obs, string estado, string nombreCliente, string apellidoCliente, string nombreCadete) {
        Cliente nuevoCliente = new Cliente(nombreCliente, apellidoCliente);
        Pedido nuevoPedido = new Pedido(contadorPedidos++, obs, estado, nuevoCliente);
        Cadete? buscarCadete = ListaCadetes.Find(cad => cad.Nombre.Contains(nombreCadete));
        if (buscarCadete != null) { // Si se encontró el cadete, entonces le agrego el pedido
            buscarCadete.ListaPedidos1.Add(nuevoPedido);
        }
        ListaPedidos.Add(nuevoPedido);
        return View();
    }

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

    private static void LoadCadetes(List<Cadete> lista) {
        string nombreArchivoCadetes = "datosCadetes.csv";
        string ruta = "bin\\Debug\\net6.0\\" + nombreArchivoCadetes;
        var datos = System.IO.File.ReadAllLines(ruta, Encoding.Default).ToList();
        foreach (var linea in datos.Skip(1)) {
            var col = linea.Split(';');
            Cadete nuevoCadete = new Cadete(Convert.ToInt32(col[0]), col[1], col[2], Convert.ToInt64(col[3]), Convert.ToDouble(col[4]));
            lista.Add(nuevoCadete);
        }
    }
}