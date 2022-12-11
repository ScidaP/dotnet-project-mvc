using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class ClienteViewModel {
        private int id;
        private string? nombre;
        private string? direccion;
        private long? telefono;
        private string? referenciasDireccion;

        public ClienteViewModel(){}

        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Direccion { get => direccion; set => direccion = value; }
        public long? Telefono { get => telefono; set => telefono = value; }
        public string? ReferenciasDireccion { get => referenciasDireccion; set => referenciasDireccion = value; }
        public int Id { get => id; set => id = value; }
    }
}