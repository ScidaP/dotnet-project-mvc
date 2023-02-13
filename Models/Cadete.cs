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
        private int activo;

        public Cadete() {

        }

        public Cadete(string nombre, string direccion, long telefono, int cad, double totalACobrar1) : base(nombre, direccion, telefono) {
            Cadeteria = cad;
            TotalACobrar1 = totalACobrar1;
            Activo = 1;
        }

        public Cadete(int id, string nombre, string direccion, long telefono, int cad, double totalACobrar1) : base(nombre, direccion, telefono) {
            Id = id;
            Cadeteria = cad;
            TotalACobrar1 = totalACobrar1;
            Activo = 1;
        }

        public string NombreCadeteria(int id) {
            string nombre = "";
            using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
                conexion.Open();
                var command = conexion.CreateCommand();
                command.CommandText = @"SELECT nombre FROM cadeteria WHERE id = $id";
                command.Parameters.AddWithValue("$id", id);
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        nombre = reader.GetString(0);
                    }
                }
                conexion.Close();
            }
            return nombre;
        }
        public double? TotalACobrar1 { get => TotalACobrar; set => TotalACobrar = value; }
        public int Cadeteria {get => cadeteria; set => cadeteria = value; }
        public int Activo { get => activo; set => activo = value; }
}