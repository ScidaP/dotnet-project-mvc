using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class LayoutViewModel {
        int? rol;

        public LayoutViewModel() {}

        public LayoutViewModel(int? rol1) {
            rol = rol1;
        }

        public int? Rol { get => rol; set => rol = value; }
    }
}