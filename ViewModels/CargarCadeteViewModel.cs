using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class CargarCadeteViewModel {

        private int id;
        private string nombre;
        private string direccion;
        private long telefono;
        private int cadeteria;
        private double TotalACobrar;
        private List<Cadeteria> ListaCadeterias;
        public CargarCadeteViewModel(List<Cadeteria> listaC) {
            ListaCadeterias = listaC;
        }

        public CargarCadeteViewModel(Cadete cad, List<Cadeteria> listaC) {
        }

        public CargarCadeteViewModel(){}
        public List<Cadeteria> ListaCadeterias1 { get => ListaCadeterias; set => ListaCadeterias = value; }
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public long Telefono { get => telefono; set => telefono = value; }
        public int Cadeteria { get => cadeteria; set => cadeteria = value; }
        public double TotalACobrar1 { get => TotalACobrar; set => TotalACobrar = value; }
    }
}