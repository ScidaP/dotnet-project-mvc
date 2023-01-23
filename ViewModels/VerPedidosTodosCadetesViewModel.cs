using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class VerPedidosTodosCadetesViewModel {
        private List<Cadete> ListaCadetes;
        private List<Pedido> ListaPedidos;

        public VerPedidosTodosCadetesViewModel(List<Cadete> ListaCadetes2, List<Pedido> ListaPedidos2){
            ListaCadetes = ListaCadetes2;
            ListaPedidos = ListaPedidos2;
        }
        public List<Cadete> ListaCadetes1 { get => ListaCadetes; set => ListaCadetes = value; }
        public List<Pedido> ListaPedidos1 { get => ListaPedidos; set => ListaPedidos = value; }
    }
}