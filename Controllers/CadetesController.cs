using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;

namespace Tp4MvcNuevo.Controllers;

public class CadetesController : Controller {

    internal static List<Cadete> ListaCadetes = new List<Cadete>();
    public IActionResult CargarCadete() {
        return View();
    }
    [HttpPost]
    public IActionResult CadeteAgregado(string nombre, string direccion, int telefono, double sueldo) {
        Cadete nuevoCadete = new Cadete(GetIndexCadetes(), nombre, direccion, telefono, sueldo);
        ListaCadetes.Add(nuevoCadete);
        ViewData["nombre"] = nombre;
        return View();
    }

    private static int GetIndexCadetes() {
        string nombreArchivoCadetes = "datosCadetes.csv";
        string ruta = "bin\\Debug\\net6.0\\" + nombreArchivoCadetes;
        var datos = System.IO.File.ReadAllLines(ruta, Encoding.Default).ToList();
        int index = 0;
        foreach (var linea in datos.Skip(1)) {
            var col = linea.Split(';');
            int indexActual = Convert.ToInt32(col[0]);
            if (index < indexActual) {
                index = indexActual;
            }
        }
        return index;
    }
}