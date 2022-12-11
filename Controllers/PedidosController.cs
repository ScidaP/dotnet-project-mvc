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
using AutoMapper;
using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.Controllers;

public class PedidosController : Controller
{
    private readonly IRepositorioPedidos repoPedidos;
    private readonly IMapper mapper;

    public PedidosController(IRepositorioPedidos repoPed, IMapper mapp) {
        repoPedidos = repoPed;
        mapper = mapp;
    }
    public IActionResult HacerPedido() {
        return View();
    }

    [HttpPost]
    public IActionResult PedidoAgregado(string obs, string estado, string numeroCliente, string nombreCadete) {
        long numeroTelCliente = Convert.ToInt64(numeroCliente);
        var cliente = repoPedidos.getIdCliente(numeroTelCliente);
        var cadete = repoPedidos.getIdCadete(nombreCadete);
        var nuevoPedido = new Pedido(obs, estado, cliente, cadete);
        repoPedidos.agregarPedido(nuevoPedido);
        TempData["Info"] = "Pedido agregado con éxito";
        return RedirectToAction("Info");
    }

    [HttpGet]
    public IActionResult EliminarPedido(int id) {
        repoPedidos.eliminarPedido(id);
        TempData["Info"] = "Pedido N°" + id + " eliminado con éxito.";
        return RedirectToAction("Info");
    }

    public IActionResult ListarPedidos() {
        List<Pedido> pedidos = repoPedidos.getTodosPedidos();
        var ListarPedidosVM = new ListarPedidosViewModels();
        ListarPedidosVM.pedidos = mapper.Map<List<Pedido>>(pedidos);
        return View(ListarPedidosVM);
    }
    
    public IActionResult Info() {
        return View();
    }
}