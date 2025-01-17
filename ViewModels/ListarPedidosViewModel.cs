using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp4MvcNuevo.Models;
using System.Data.SQLite;

namespace Tp4MvcNuevo.ViewModels {
    public class ListarPedidosViewModels {
        public List<Pedido> pedidos {get;set;}
        public List<Cadete> cadetes {get;set;}

        public ListarPedidosViewModels (List<Pedido> ped, List<Cadete> cad) {
            this.pedidos = ped;
            this.cadetes = cad;
        }

        public ListarPedidosViewModels(){
            pedidos = new List<Pedido>();
            cadetes = new List<Cadete>();
        }
    }
}