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
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Tp4MvcNuevo.Controllers;

public class PedidosController : Controller
{
    private readonly IRepositorioPedidos repoPedidos;
    private readonly IRepositorioCadetes repoCadetes;
    private readonly IRepositorioClientes repoClientes;
    private readonly IMapper mapper;
    private readonly ILogger<PedidosController> logger;

    public PedidosController(IRepositorioPedidos repoPed, IRepositorioCadetes repoCad, IRepositorioClientes repoCli, IMapper mapp, ILogger<PedidosController> log) {
        repoPedidos = repoPed;
        repoCadetes = repoCad;
        repoClientes = repoCli;
        mapper = mapp;
        logger = log;
    }
    public IActionResult HacerPedido() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 1) { // Admin
            try {
                List<Cadete> ListaCadetes = repoCadetes.getTodosCadetes();
                List<Cliente> ListaClientes = repoClientes.getTodosClientes();
                return View(new HacerPedidoViewModel(ListaCadetes, ListaClientes));
            } catch (Exception e) {
                logger.LogError("Error al cargar página. -> " + e.ToString());
                TempData["Info"] = "Error al cargar página. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info");
            }
        } else {
            if (Rol == 2) {
                return RedirectToAction("Index", "Home");
            } else {
                return RedirectToAction("IniciarSesion", "Logueo");
            }
        }
    }

    [HttpGet]

    public IActionResult MostrarPedido(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 1 || Rol == 2) { // Admin y supervisor
            try {
                Pedido ped = repoPedidos.getPedido(id);
                var mostrarPedidoVM = mapper.Map<MostrarPedidoViewModel>(ped);
                return View(mostrarPedidoVM);
            } catch (Exception e) {
                logger.LogError("Error al mostrar pedidos. -> " + e.ToString());
                TempData["Info"] = "Error al mostrar pedidos. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info"); 
            }
        } else {
            return RedirectToAction("IniciarSesion", "Logueo");
        }
    }

    [HttpPost]
    public IActionResult PedidoAgregado(HacerPedidoViewModel HacerPedidoVM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    if (ModelState.IsValid) {
                        var nuevoPedido = mapper.Map<Pedido>(HacerPedidoVM);
                        repoPedidos.agregarPedido(nuevoPedido);
                        TempData["Info"] = "Pedido agregado con éxito";
                        return RedirectToAction("Info");
                    } else {
                        HacerPedidoVM.ListaCadetes1 = repoCadetes.getTodosCadetes();
                        HacerPedidoVM.ListaClientes1 = repoClientes.getTodosClientes();
                        return View("HacerPedido", HacerPedidoVM);
                    }
                } catch (Exception e) {
                    logger.LogError("Error desconocido. -> " + e.ToString());
                    TempData["Info"] = "Error desconocido. Intente nuevamente. -> + " + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpGet]
    public IActionResult EliminarPedido(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 1) { // Admin
            try {
                repoPedidos.eliminarPedido(id);
                TempData["Info"] = "Pedido N°" + id + " eliminado con éxito.";
                return RedirectToAction("Info");
            } catch (Exception e) {
                logger.LogError("Error al eliminar pedido. -> " + e.ToString());
                TempData["Info"] = "Error al eliminar pedido. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info");
            }
        } else {
            if (Rol == 2) {
                return RedirectToAction("ErrorPermiso", "Home");
            } else {
                return RedirectToAction("IniciarSesion", "Logueo");
            }
        }
    }

    public IActionResult ListarPedidos() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == 1 || Rol == 2) { // Admin y cadete
            try {
                List<Pedido> pedidos = repoPedidos.getTodosPedidos();
                var ListarPedidosVM = new ListarPedidosViewModels();
                ListarPedidosVM.pedidos = mapper.Map<List<Pedido>>(pedidos);
                return View(ListarPedidosVM);
            } catch (Exception e) {
                logger.LogError("Error al listar pedidos. -> " + e.ToString());
                TempData["Info"] = "Error al listar pedidos. Intente nuevamente. -> " + e.Message;
                return RedirectToAction("Info");
            }
        } else {
            return RedirectToAction("IniciarSesion", "Logueo");
        }
    }

    [HttpGet]
    public IActionResult ActualizarPedido(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    var pedidoAActualizar = repoPedidos.getPedido(id);
                    var HacerPedidoVM = mapper.Map<HacerPedidoViewModel>(pedidoAActualizar);
                    HacerPedidoVM.ListaCadetes1 = repoCadetes.getTodosCadetes();
                    HacerPedidoVM.ListaClientes1 = repoClientes.getTodosClientes();
                    return View(HacerPedidoVM);
                } catch (Exception e) {
                    logger.LogError("Error al actualizar pedido. -> " + e.ToString());
                    TempData["Info"] = "Error al actualizar pedido. Intente nuevamente. -> " + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpPost]
    public IActionResult PedidoActualizado(HacerPedidoViewModel VM) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                try {
                    if (ModelState.IsValid) {
                        var nuevoPedido = mapper.Map<Pedido>(VM);
                        repoPedidos.actualizarPedido(nuevoPedido);
                        TempData["Info"] = "Pedido N° " + nuevoPedido.Numero + " actualizado con éxito.";
                        return RedirectToAction("Info");
                    } else {
                        VM.ListaCadetes1 = repoCadetes.getTodosCadetes();
                        VM.ListaClientes1 = repoClientes.getTodosClientes();
                        return View("ActualizarPedido", VM);
                    }
                } catch (Exception e) {
                    logger.LogError("Error al actualizar pedido. -> " + e.ToString());
                    TempData["Info"] = "Error al actualizar pedido. Intente nuevamente. -> " + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }
    
    public IActionResult Info() {
        return View();
    }
}