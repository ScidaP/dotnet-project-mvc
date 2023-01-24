using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;

public class Pedido {
        private int numero;
        private string? obs;
        private int idCliente;
        private int idCadete;
        private string? estado;

        public Pedido() {}

        public Pedido(string obs, string estado, int idCli, int idCad) {
            Obs = obs;
            Estado = estado;
            idCliente = idCli;
            idCadete = idCad;
        }

        public Pedido(int numero, string obs, int idCli, string estado, int idCad) {
            Numero = numero;
            Obs = obs;
            Estado = estado;
            idCliente = idCli;
            idCadete = idCad;
        }

        public void CambiarEstado(string EstadoNuevo) {
            Estado = EstadoNuevo;
        }

        public string getNombreCliente(int id) {
            string nombre = "";
            using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
                conexion.Open();
                var command = conexion.CreateCommand();
                command.CommandText = @"SELECT nombre FROM cliente WHERE id = $id";
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

        public string getNombreCadete(int id) {
            string nombre = "";
            using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
                conexion.Open();
                var command = conexion.CreateCommand();
                command.CommandText = @"SELECT nombre FROM cadetes WHERE id = $id";
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
        public int Numero { get => numero; set => numero = value; }
        public string? Obs { get => obs; set => obs = value; }
        public string? Estado { get => estado; set => estado = value; }
        public int idCliente1 { get => idCliente; set => idCliente = value; }
        public int idCadete1 { get => idCadete; set => idCadete = value; }
    }