using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class ActualizarCadeteViewModel {
        private Cadete Cadete;
        private List<Cadeteria> ListaCadeterias;

        public ActualizarCadeteViewModel(Cadete cad, List<Cadeteria> ListaC) {
            Cadete = cad;
            ListaCadeterias = ListaC;
        }

        public Cadete Cadete1 { get => Cadete; set => Cadete = value; }
        public List<Cadeteria> ListaCadeterias1 { get => ListaCadeterias; set => ListaCadeterias = value; }
    }
}