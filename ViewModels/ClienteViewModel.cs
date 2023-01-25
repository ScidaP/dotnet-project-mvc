using Tp4MvcNuevo.Models;
using System.ComponentModel.DataAnnotations;

namespace Tp4MvcNuevo.ViewModels {
    public class ClienteViewModel {
        private int id;
        private string? nombre;
        private string? direccion;
        private long? telefono;
        private string? referenciasDireccion;

        public ClienteViewModel(){}
        [Required(ErrorMessage = "Tiene que llenar el campo nombre")]
        [StringLength(30)]
        public string? Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo dirección")]
        public string? Direccion { get => direccion; set => direccion = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo teléfono")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Número de teléfono no válido")]
        public long? Telefono { get => telefono; set => telefono = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo referencias de dirección")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string? ReferenciasDireccion { get => referenciasDireccion; set => referenciasDireccion = value; }
        public int Id { get => id; set => id = value; }
    }
}