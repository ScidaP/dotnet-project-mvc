using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class IniciarSesionViewModel {
        string? usuario;
        string? pass;

        public IniciarSesionViewModel() {}

        public string? Usuario1 { get => usuario; set => usuario = value; }
        public string? Pass { get => pass; set => pass = value; }
    }
}