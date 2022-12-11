using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class HacerPedidoViewModel {
        private List<Cadete> ListaCadetes;
        private List<Cliente> ListaClientes;
        private string? obs;
        private int idCliente;
        private int idCadete;
        private string? estado;

        public HacerPedidoViewModel(){ListaCadetes = new List<Cadete>(); ListaClientes = new List<Cliente>();}

        public HacerPedidoViewModel(List<Cadete> listaCad, List<Cliente> listaCli) {
            ListaCadetes = listaCad;
            ListaClientes = listaCli;
        }

        public HacerPedidoViewModel(string? o, int idcli, int idcad, string? est) {
            ListaCadetes = new List<Cadete>(); ListaClientes = new List<Cliente>();
            Obs = o;
            idCliente1 = idcli;
            idCadete1 = idcad;
            Estado = est;
        }
        public List<Cadete> ListaCadetes1 { get => ListaCadetes; set => ListaCadetes = value; }
        public List<Cliente> ListaClientes1 { get => ListaClientes; set => ListaClientes = value; }
        public string? Obs { get => obs; set => obs = value; }
        public int idCliente1 { get => idCliente; set => idCliente = value; }
        public int idCadete1 { get => idCadete; set => idCadete = value; }
        public string? Estado { get => estado; set => estado = value; }
    }
}