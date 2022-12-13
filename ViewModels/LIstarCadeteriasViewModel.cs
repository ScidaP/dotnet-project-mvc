using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class ListarCadeteriasViewModel {
        List<Cadeteria> ListaCadeterias;

        public ListarCadeteriasViewModel(List<Cadeteria> ListaCad) {
            ListaCadeterias = ListaCad;
        }

        public List<Cadeteria> ListaCadeterias1 { get => ListaCadeterias; set => ListaCadeterias = value; }
    }
}