using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class ListaCadetesViewModel {
        List<Cadete> ListaCadetes;

        List<Cadeteria> ListaCadeterias; // Para poder imprimir el nombre de las cadeterias al mostrar todos los cadetes

        public ListaCadetesViewModel(List<Cadete> ListaCad) {
            ListaCadetes = ListaCad;
        }

        public ListaCadetesViewModel(List<Cadete> ListaCad, List<Cadeteria> ListaCade) {
            ListaCadetes = ListaCad;
            ListaCadeterias1 = ListaCade;
        }

        public List<Cadete> ListaCadetes1 { get => ListaCadetes; set => ListaCadetes = value; }
        public List<Cadeteria> ListaCadeterias1 { get => ListaCadeterias; set => ListaCadeterias = value; }
    }
}