using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;

namespace Tp4MvcNuevo.Controllers;

public class PedidosController : Controller
{
    internal static List<Pedido> ListaPedidos = new List<Pedido>();
    internal static List<Cliente> ListaClientes = new List<Cliente>();
    internal static List<Cadete> ListaCadetes = new List<Cadete>();
    
    public IActionResult HacerPedido() {
        LoadCadetes(ListaCadetes);
        return View();
    }

    [HttpPost]
    public IActionResult PedidoAgregado(int numero, string obs, string estado, string nombreCliente, string apellidoCliente) {
        Cliente nuevoCliente = new Cliente(nombreCliente, apellidoCliente);
        Pedido nuevoPedido = new Pedido(numero, obs, estado, nuevoCliente);
        ListaPedidos.Add(nuevoPedido);
        return View();
    }

    public IActionResult ListarPedidos() {
        return View(ListaPedidos);
    }

    public IActionResult PedidoAgregado() {
        return View();
    }

    private static void LoadCadetes(List<Cadete> lista) { // no lee bien
        string nombreArchivoCadetes = "datosCadetes.csv";
        string ruta = "bin\\Debug\\net6.0\\" + nombreArchivoCadetes;
    }
}