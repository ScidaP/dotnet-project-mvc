using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;

public class Cliente : Persona {
        private string? referenciasDireccion;

        public Cliente():base(){}

        public Cliente(int id, string nombre, string direccion, long telefono, string referenciasDireccion) : base(nombre, direccion, telefono) {
            Id = id;
            ReferenciasDireccion = referenciasDireccion;
        }

        public Cliente(string nombre, string direccion, long telefono, string referenciasDireccion) : base(nombre, direccion, telefono) {
            ReferenciasDireccion = referenciasDireccion;
        }

        public Cliente(string nombre, string apellido) : base(nombre, apellido) {
            ReferenciasDireccion = null;
        }

        public string? ReferenciasDireccion { get => referenciasDireccion; set => referenciasDireccion = value; }
    }