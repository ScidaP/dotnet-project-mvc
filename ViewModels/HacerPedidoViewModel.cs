using Tp4MvcNuevo.Models;
using System.ComponentModel.DataAnnotations;

namespace Tp4MvcNuevo.ViewModels {
    public class HacerPedidoViewModel {
        private List<Cadete> ListaCadetes;
        private List<Cliente> ListaClientes;
        private int numero;
        private string? obs;
        private int idCliente;
        private int idCadete;
        private string? estado;
        private int activo;

        public HacerPedidoViewModel(){ListaCadetes = new List<Cadete>(); ListaClientes = new List<Cliente>();}

        public HacerPedidoViewModel(List<Cadete> listaCad, List<Cliente> listaCli) {
            ListaCadetes = listaCad;
            ListaClientes = listaCli;
        }
        public List<Cadete> ListaCadetes1 { get => ListaCadetes; set => ListaCadetes = value; }
        public List<Cliente> ListaClientes1 { get => ListaClientes; set => ListaClientes = value; }
        [Required(ErrorMessage = "Tiene que llegar el campo Observaciones")]
        [StringLength(40, ErrorMessage = "Máximo 40 caracteres")]
        public string? Obs { get => obs; set => obs = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo Cliente")]
        public int idCliente1 { get => idCliente; set => idCliente = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo Cadete")]
        public int idCadete1 { get => idCadete; set => idCadete = value; }
        [Required(ErrorMessage = "Tiene que llenar el campo Estado")]
        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        public string? Estado { get => estado; set => estado = value; }
        public int Numero { get => numero; set => numero = value; }
        public int Activo { get => activo; set => activo = value; }
    }
}