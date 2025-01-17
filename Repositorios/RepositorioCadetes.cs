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
    public bool existeCadete(int id);
}

public class RepositorioCadetes : IRepositorioCadetes {
    public bool existeCadete(int id) {
        bool res = false;
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cadetes WHERE activo=1 AND id=$id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                res = reader.HasRows;
            }
            conexion.Close();
        }
        return res;
    }
    public Cadete getCadete(int id) {
        Cadete cadete = new Cadete();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cadetes WHERE id = $id";
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
            command.CommandText = @"INSERT INTO cadetes(nombre, direccion, telefono, totalACobrar, cadeteria, activo) VALUES ($nombre, $direccion, $telefono, $sueldo, $cadeteria, 1)";
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
            command.CommandText = @"DELETE FROM cadetes WHERE id=$id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
            // Ahora cambio a inactivos a todos los pedidos que hizo/hace este cadete.
            var command2 = conexion.CreateCommand();
            command2.CommandText = @"UPDATE pedidos SET activo=0 WHERE cadete=$id";
            command2.Parameters.AddWithValue("$id", id);
            command2.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void actualizarCadete(Cadete cad) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"UPDATE cadetes SET nombre = $nombre, direccion = $direccion, telefono = $telefono, cadeteria = $cadeteria, totalACobrar = $sueldo WHERE id = $id";
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
            command.CommandText = @"SELECT * FROM cadetes WHERE activo=1"; // Se agrega la nueva claúsula WHERE para traer aquellos cadetes activos.
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    var cadete = new Cadete(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt64(3), reader.GetInt32(4), reader.GetInt32(5));
                    ListaCadetes.Add(cadete);
                }
            }
            conexion.Close();
        }
        return ListaCadetes;
    }
}