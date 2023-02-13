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
        public string Usuario { get => usuario; set => usuario = value; }
        public string Pass { get => pass; set => pass = value; }
        public int Rol { get => rol; set => rol = value; }
    }
}