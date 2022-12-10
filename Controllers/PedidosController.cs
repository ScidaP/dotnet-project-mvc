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
    private readonly IRepositorioCadetes repoCadetes;
    private readonly IMapper mapper;

    public PedidosController(IRepositorioCadetes repoCad, IMapper mapp) {
        repoCadetes = repoCad;
        mapper = mapp;
    }
    public IActionResult HacerPedido() {
        return View();
    }

    public IActionResult PedidoAgregado(string obs, string estado, string numeroCliente, string nombreCadete) {
        long numeroTelCliente = Convert.ToInt64(numeroCliente);
    }
}