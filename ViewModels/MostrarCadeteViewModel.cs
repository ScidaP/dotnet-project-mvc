using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class MostrarCadeteViewModel {
        private string? nombre;
        private string? direccion;
        private long? telefono;

        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Direccion { get => direccion; set => direccion = value; }
        public long? Telefono { get => telefono; set => telefono = value; }
    }
}