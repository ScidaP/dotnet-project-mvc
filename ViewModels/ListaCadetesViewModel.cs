using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    class ListaCadetesViewModel {
        List<Cadete> ListaCadetes;

        public ListaCadetesViewModel(List<Cadete> ListaCad) {
            ListaCadetes = ListaCad;
        }

        public List<Cadete> ListaCadetes1 { get => ListaCadetes; set => ListaCadetes = value; }
    }
}