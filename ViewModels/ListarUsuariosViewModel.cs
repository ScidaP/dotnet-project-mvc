using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp4MvcNuevo.Models;
using System.Data.SQLite;

namespace Tp4MvcNuevo.ViewModels {
    public class ListarUsuariosViewModel {
        private List<Usuario> listaUsuarios;

        public ListarUsuariosViewModel(List<Usuario> listaUsuarios)
        {
            this.listaUsuarios = listaUsuarios;
        }

        public ListarUsuariosViewModel() {
            this.listaUsuarios = new List<Usuario>();
        }

        public List<Usuario> ListaUsuarios { get => listaUsuarios; set => listaUsuarios = value; }
    }
}