using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class MostrarPedidoViewModel {
        private int numero;
        private string? obs;
        private int idCliente;
        private int idCadete;
        private string? estado;

        public int Numero { get => numero; set => numero = value; }
        public string? Obs { get => obs; set => obs = value; }
        public string? Estado { get => estado; set => estado = value; }
        public int idCliente1 { get => idCliente; set => idCliente = value; }
        public int idCadete1 { get => idCadete; set => idCadete = value; }
    }
}