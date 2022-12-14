using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class MostrarCadeteriaViewModel {
        private int id;
        private string? nombre;
        private long? telefono;
        public int Id { get => id; set => id = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public long? Telefono { get => telefono; set => telefono = value; }
    }
}