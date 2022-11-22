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
        cargarCadeteCSV(crearLinea(nuevoCadete));
        return View();
    }
    
    private static int GetIndexCadetes() {
        string nombreArchivoCadetes = "datosCadetes.csv";
        string ruta = "bin\\Debug\\net6.0\\" + nombreArchivoCadetes;
        var datos = System.IO.File.ReadAllLines(ruta, Encoding.Default).ToList();
        int index = datos.Count + 1;
        return index;
    }

    private static string crearLinea(Cadete cad) { //Recibe datos y devuelve una línea concatenada (es para agregar la línea al CSV)
        string linea = cad.Id + ";" + cad.Nombre + ";" + cad.Direccion + ";" + cad.Telefono + ";" + cad.TotalACobrar1;
        return linea;
    }

    private static void cargarCadeteCSV(string linea) {
        System.IO.File.AppendAllText("bin\\Debug\\net6.0\\datosCadetes.csv", linea + Environment.NewLine);
    }
}