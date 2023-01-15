using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;

public class Cadete : Persona {

        private int cadeteria;
        private double? TotalACobrar;

        public Cadete() {

        }

        public Cadete(string nombre, string direccion, long telefono, int cad, double totalACobrar1) : base(nombre, direccion, telefono) {
            Cadeteria = cad;
            TotalACobrar1 = totalACobrar1;
        }

        public Cadete(int id, string nombre, string direccion, long telefono, int cad, double totalACobrar1) : base(nombre, direccion, telefono) {
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