using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tp4MvcNuevo.Models {

    public class Persona {
        private int id;
        private string? nombre;
        private string? direccion;
        private long? telefono;

        static private int incremental = 0;

        public Persona(){}

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

        [Required]
        public int Id { get => id; set => id = value; }
        [Required]
        public string? Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string? Direccion { get => direccion; set => direccion = value; }
        [Required][Phone]
        public long? Telefono { get => telefono; set => telefono = value; }
    }

    public class Cliente : Persona {
        private string? referenciasDireccion;

        public Cliente():base(){}

        public Cliente(int id, string nombre, string direccion, long telefono, string referenciasDireccion) : base(nombre, direccion, telefono) {
            ReferenciasDireccion = referenciasDireccion;
        }

        public Cliente(string nombre, string apellido) : base(nombre, apellido) {
            ReferenciasDireccion = null;
        }

        public string? ReferenciasDireccion { get => referenciasDireccion; set => referenciasDireccion = value; }
    }

    public class Cadete : Persona {

        private int cadeteria;
        private double? TotalACobrar;

        public Cadete() {

        }

        public Cadete(string nombre, string direccion, long telefono, int cad, double totalACobrar1) : base(nombre, direccion, telefono) {
            Cadeteria = cad;
            TotalACobrar1 = totalACobrar1;
        }

        public Cadete(int id, string nombre, string direccion, long telefono, double totalACobrar1, int cad) : base(nombre, direccion, telefono) {
            Id = id;
            Cadeteria = cad;
            TotalACobrar1 = totalACobrar1;
        }

        public override void MostrarDatos() {
            base.MostrarDatos(); 
            Console.WriteLine("Total a Cobrar: " + TotalACobrar1);
        }
        public double? TotalACobrar1 { get => TotalACobrar; set => TotalACobrar = value; }
        public int Cadeteria {get => cadeteria; set => cadeteria = value; }
    }

    class Cadeteria {
        private string? nombre;
        private long? telefono;

        public Cadeteria(string nombre, long telefono) {
            Nombre = nombre;
            Telefono = telefono;
        }

        public string? Nombre { get => nombre; set => nombre = value; }
        public long? Telefono { get => telefono; set => telefono = value; }
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