using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public interface IRepositorioCadeterias {
    public Cadeteria GetCadeteria(int id);
    public void Agregarcadeteria(Cadeteria cad);
    public void EliminarCadeteria(int id);
    public void ActualizarCadeteria(Cadeteria cad);
    public List<Cadeteria> GetTodasCadeterias();
}

public class RepositorioCadeterias : IRepositorioCadeterias {
    public Cadeteria GetCadeteria(int id) {
        Cadeteria cad = new Cadeteria();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cadeteria WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    cad = new Cadeteria(reader.GetInt32(0), reader.GetString(1), reader.GetInt64(2));
                }
            }
            conexion.Close();
        }
        return cad;
    }

    public void Agregarcadeteria(Cadeteria cad) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"INSERT INTO cadete(nombre, telefono) VALUES ($nombre, $telefono)";
            command.Parameters.AddWithValue("$nombre", cad.Nombre);
            command.Parameters.AddWithValue("$telefono", cad.Telefono);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }
    public void EliminarCadeteria(int id) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"DELETE FROM cadeteria WHERE id=$id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void ActualizarCadeteria(Cadeteria cad) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"UPDATE cadeteria SET nombre = $nombre, telefono = $telefono WHERE id = $id";
            command.Parameters.AddWithValue("$nombre", cad.Nombre);
            command.Parameters.AddWithValue("$telefono", cad.Telefono);
            command.Parameters.AddWithValue("$id", cad.Id);
            int resultado = command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public List<Cadeteria> GetTodasCadeterias() {
        List<Cadeteria> ListaCadeterias = new List<Cadeteria>();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cadeteria";
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    var cadeteria = new Cadeteria(reader.GetInt32(0), reader.GetString(1), reader.GetInt64(2));
                    ListaCadeterias.Add(cadeteria);
                }
            }
            conexion.Close();
        }
        return ListaCadeterias;
    }
}