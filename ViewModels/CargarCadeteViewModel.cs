using Tp4MvcNuevo.Models;
using System.ComponentModel.DataAnnotations;

namespace Tp4MvcNuevo.ViewModels {
    public class CargarCadeteViewModel {
        private int id;
        private string nombre;
        private string direccion;
        private long? telefono;
        private int cadeteria;
        private double? TotalACobrar;
        private int activo;
        private List<Cadeteria>? ListaCadeterias;
        public CargarCadeteViewModel(List<Cadeteria> listaC) {
            ListaCadeterias = listaC;
        }

        public CargarCadeteViewModel(Cadete cad, List<Cadeteria> listaC) {
        }

        public CargarCadeteViewModel(){}
        public List<Cadeteria>? ListaCadeterias1 { get => ListaCadeterias; set => ListaCadeterias = value; }
        public int Id { get => id; set => id = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo nombre")]
        [StringLength(30)]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo dirección")]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo teléfono")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Número de teléfono no válido")]
        public long? Telefono { get => telefono; set => telefono = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo cadetería. Si no hay ninguna cargada, cargue una antes.")]
        public int Cadeteria { get => cadeteria; set => cadeteria = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo sueldo")]
        public double? TotalACobrar1 { get => TotalACobrar; set => TotalACobrar = value; }
        [Required(ErrorMessage = "No se proporcionó el campo activo correctamente")]
        public int Activo { get => activo; set => activo = value; }
    }
}