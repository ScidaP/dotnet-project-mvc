using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public interface IRepositorioCadetes {
    public Cadete getCadete(int id);
    public void agregarCadete(Cadete cad);
    public void eliminarCadete(int id);
    public List<Cadete> getTodosCadetes();
    public void actualizarCadete(Cadete cad);
}

public class RepositorioCadetes : IRepositorioCadetes {
    public Cadete getCadete(int id) {
        Cadete cadete = new Cadete();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cadete WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    cadete = new Cadete(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt64(3), reader.GetInt32(4), reader.GetInt32(5));
                }
            }
            conexion.Close();
        }
        return cadete;
    }

    public void agregarCadete(Cadete cad) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"INSERT INTO cadete(nombre, direccion, telefono, totalACobrar, cadeteria) VALUES ($nombre, $direccion, $telefono, $sueldo, $cadeteria)";
            command.Parameters.AddWithValue("$nombre", cad.Nombre);
            command.Parameters.AddWithValue("$direccion", cad.Direccion);
            command.Parameters.AddWithValue("$telefono", cad.Telefono);
            command.Parameters.AddWithValue("$cadeteria", cad.Cadeteria);
            command.Parameters.AddWithValue("$sueldo", cad.TotalACobrar1);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void eliminarCadete(int id) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"DELETE FROM cadete WHERE id=$id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void actualizarCadete(Cadete cad) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"UPDATE cadete SET nombre = $nombre, direccion = $direccion, telefono = $telefono, totalACobrar = $sueldo, cadeteria = $cadeteria WHERE id = $id";
            command.Parameters.AddWithValue("$nombre", cad.Nombre);
            command.Parameters.AddWithValue("$direccion", cad.Direccion);
            command.Parameters.AddWithValue("$telefono", cad.Telefono);
            command.Parameters.AddWithValue("$sueldo", cad.TotalACobrar1);
            command.Parameters.AddWithValue("$cadeteria", cad.Cadeteria);
            command.Parameters.AddWithValue("$id", cad.Id);
            int resultado = command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public List<Cadete> getTodosCadetes() {
        List<Cadete> ListaCadetes = new List<Cadete>();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cadete";
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    var cadete = new Cadete(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt64(3), reader.GetInt32(5), reader.GetInt32(4));
                    ListaCadetes.Add(cadete);
                }
            }
            conexion.Close();
        }
        return ListaCadetes;
    }
}