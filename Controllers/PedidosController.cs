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

namespace Tp4MvcNuevo.Controllers;

public class PedidosController : Controller
{
    private readonly IRepositorioPedidos repoPedidos;
    private readonly IRepositorioCadetes repoCadetes;
    private readonly IRepositorioClientes repoClientes;

    private readonly IMapper mapper;

    public PedidosController(IRepositorioPedidos repoPed, IRepositorioCadetes repoCad, IRepositorioClientes repoCli,IMapper mapp) {
        repoPedidos = repoPed;
        repoCadetes = repoCad;
        repoClientes = repoCli;
        mapper = mapp;
    }
    public IActionResult HacerPedido() {
        List<Cadete> ListaCadetes = repoCadetes.getTodosCadetes();
        List<Cliente> ListaClientes = repoClientes.getTodosClientes();
        HacerPedidoViewModel HacerPedidoVM = new HacerPedidoViewModel(ListaCadetes, ListaClientes);
        return View(HacerPedidoVM);
    }

    [HttpPost]
    public IActionResult PedidoAgregado(HacerPedidoViewModel HacerPedidoVM) {
        var nuevoPedido = mapper.Map<Pedido>(HacerPedidoVM);
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

    [HttpGet]
    public IActionResult ActualizarPedido(int id) {
        var pedidoAActualizar = repoPedidos.getPedido(id);
        var HacerPedidoVM = mapper.Map<HacerPedidoViewModel>(pedidoAActualizar);
        HacerPedidoVM.ListaCadetes1 = repoCadetes.getTodosCadetes();
        HacerPedidoVM.ListaClientes1 = repoClientes.getTodosClientes();
        return View(HacerPedidoVM);
    }

    [HttpPost]
    public IActionResult PedidoActualizado(HacerPedidoViewModel VM) {
        var nuevoPedido = mapper.Map<Pedido>(VM);
        repoPedidos.actualizarPedido(nuevoPedido);
        TempData["Info"] = "Pedido N° " + nuevoPedido.Numero + " actualizado con éxito.";
        return RedirectToAction("Info");
    }
    
    public IActionResult Info() {
        return View();
    }
}