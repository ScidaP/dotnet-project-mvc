using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;

namespace Tp4MvcNuevo.Controllers;

public class CadeteriasController : Controller {
    private readonly IMapper mapper;

    public CadeteriasController(IMapper map) {
        mapper = map;
    }

    public IActionResult CargarCadeteria() {
        return View();
    }
}