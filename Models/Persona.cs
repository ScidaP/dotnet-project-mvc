using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;

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