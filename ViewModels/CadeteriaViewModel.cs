using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;

namespace Tp4MvcNuevo.ViewModels {
    public class CadeteriaViewModel {
        private int id;
        private string? nombre;
        private long? telefono;

        public CadeteriaViewModel(){}

        public CadeteriaViewModel(string nombre, long telefono) {
            Nombre = nombre;
            Telefono = telefono;
        }

        public CadeteriaViewModel(int id, string nombre, long telefono) {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
        }

        public string? Nombre { get => nombre; set => nombre = value; }
        public long? Telefono { get => telefono; set => telefono = value; }
        public int Id { get => id; set => id = value; }
    }
}