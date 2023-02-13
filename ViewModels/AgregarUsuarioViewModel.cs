using Tp4MvcNuevo.Models;
using System.ComponentModel.DataAnnotations;

namespace Tp4MvcNuevo.ViewModels {
    public class AgregarUsuarioViewModel {
        private string nombre;
        private string usuario;
        private string pass;
        private int rol;

        public AgregarUsuarioViewModel(string nombre, string usuario, string pass, int rol)
        {
            this.nombre = nombre;
            this.usuario = usuario;
            this.pass = pass;
            this.rol = rol;
        }

        public AgregarUsuarioViewModel() {
            this.nombre = "";
            this.usuario = "";
            this.pass = "";
        }

        public string Nombre { get => nombre; set => nombre = value; }

        [Required(ErrorMessage = "El usuario es requerido")]
        public string Usuario1 { get => usuario; set => usuario = value; }

        [Required(ErrorMessage = "Tiene que llenar el campo contraseña")]
        public string Pass { get => pass; set => pass = value; }

        [RegularExpression(@"^[1-2]{1}$", ErrorMessage = "Rol no válido")]
        public int Rol { get => rol; set => rol = value; }
    }
}