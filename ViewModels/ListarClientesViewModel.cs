using Tp4MvcNuevo.Models;

namespace Tp4MvcNuevo.ViewModels {
    public class ListarClientesViewModel {
        List<Cliente> ListaClientes;

        public ListarClientesViewModel(List<Cliente> ListaCli) {
            ListaClientes = ListaCli;
        }

        public List<Cliente> ListaClientes1 { get => ListaClientes; set => ListaClientes = value; }
    }
}