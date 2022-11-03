using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp4MvcNuevo.Models {

    class Persona {
        private int id;
        private string? nombre;
        private string? direccion;
        private long? telefono;

        static private int incremental = 0;

        public Persona(string nombre, string direccion, long telefono) {
            Id = incremental;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            incremental++;
        }

        public Persona(string nombre, string apellido) {
            Id = incremental;
            Nombre = nombre + " " + apellido;
            Direccion = null;
            Telefono = null;
            incremental++;
        }

        public virtual void MostrarDatos() {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Nombre: " + Nombre);
            Console.WriteLine("Direccion: " + Direccion);
            Console.WriteLine("Telefono: " + Telefono);
        }

        public int Id { get => id; set => id = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Direccion { get => direccion; set => direccion = value; }
        public long? Telefono { get => telefono; set => telefono = value; }
    }

    class Cliente : Persona {
        private string? referenciasDireccion;

        public Cliente(int id, string nombre, string direccion, long telefono, string referenciasDireccion) : base(nombre, direccion, telefono) {
            ReferenciasDireccion = referenciasDireccion;
        }

        public Cliente(string nombre, string apellido) : base(nombre, apellido) {
            ReferenciasDireccion = null;
        }

        public string? ReferenciasDireccion { get => referenciasDireccion; set => referenciasDireccion = value; }
    }

    class Cadete : Persona {
        private List<Pedido>? ListaPedidos;
        private double? TotalACobrar;

        public Cadete(int id, string nombre, string direccion, long telefono, double totalACobrar1, List<Pedido> listaPedidos1) : base(nombre, direccion, telefono) {
            TotalACobrar1 = totalACobrar1;
            ListaPedidos1 = listaPedidos1;
        }

        public override void MostrarDatos() {
            base.MostrarDatos(); 
            Console.WriteLine("Total a Cobrar: " + TotalACobrar1);
        }
        public double? TotalACobrar1 { get => TotalACobrar; set => TotalACobrar = value; }
        internal List<Pedido>? ListaPedidos1 { get => ListaPedidos; set => ListaPedidos = value; }
    }

    class Cadeteria {
        private string? nombre;
        private long? telefono;
        private List<Cadete>? ListaCadetes;

        public Cadeteria(string nombre, long telefono, List<Cadete> listaCadetes1) {
            Nombre = nombre;
            Telefono = telefono;
            ListaCadetes1 = listaCadetes1;
        }

        public string? Nombre { get => nombre; set => nombre = value; }
        public long? Telefono { get => telefono; set => telefono = value; }
        internal List<Cadete>? ListaCadetes1 { get => ListaCadetes; set => ListaCadetes = value; }
    }
    class Pedido {
        private int numero;
        private string? obs;
        private Cliente? datosCliente;
        private string? estado;

        public Pedido(int numero, string obs, string estado, Cliente datosCliente) {
            Numero = numero;
            Obs = obs;
            Estado = estado;
            DatosCliente = datosCliente;
        }

        public void CambiarEstado(string EstadoNuevo) {
            Estado = EstadoNuevo;
        }
        public int Numero { get => numero; set => numero = value; }
        public string? Obs { get => obs; set => obs = value; }
        public string? Estado { get => estado; set => estado = value; }
        internal Cliente? DatosCliente { get => datosCliente; set => datosCliente = value; }
    }
}