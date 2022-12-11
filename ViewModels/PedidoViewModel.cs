using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class PedidoViewModel {
        private string? obs;
        private int idCliente;
        private int idCadete;
        private string? estado;

        public PedidoViewModel() {}

        public string? Obs { get => obs; set => obs = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public int IdCadete { get => idCadete; set => idCadete = value; }
        public string? Estado { get => estado; set => estado = value; }
    }
}