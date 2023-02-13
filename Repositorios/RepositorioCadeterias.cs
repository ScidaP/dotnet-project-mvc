using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public interface IRepositorioCadeterias {
    public Cadeteria GetCadeteria(int id);
    public void AgregarCadeteria(Cadeteria cad);
    public void EliminarCadeteria(int id);
    public void ActualizarCadeteria(Cadeteria cad);
    public List<Cadeteria> GetTodasCadeterias();
    public bool ExisteCadeteria(int id);
}

public class RepositorioCadeterias : IRepositorioCadeterias {
    public bool ExisteCadeteria(int id) {
        bool res = false;
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cadeteria WHERE activo=1 AND id=$id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                res = reader.HasRows;
            }
            conexion.Close();
        }
        return res;
    }
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

    public void AgregarCadeteria(Cadeteria cad) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"INSERT INTO cadeteria(nombre, telefono, activo) VALUES ($nombre, $telefono, 1)";
            command.Parameters.AddWithValue("$nombre", cad.Nombre);
            command.Parameters.AddWithValue("$telefono", cad.Telefono);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }
    public void EliminarCadeteria(int id) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            // Primera consulta -> Elimino la cadetería
            var command = conexion.CreateCommand();
            command.CommandText = @"DELETE FROM cadeteria WHERE id=$id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
            // Cambio a inactivo a todos los registros a los cuales les afecte esta eliminación
            // Segunda consulta -> Modifico los registros de la tabla cadetes
            var command2 = conexion.CreateCommand();
            command2.CommandText = @"UPDATE cadetes SET activo=0 WHERE cadeteria=$id";
            command2.Parameters.AddWithValue("$id", id);
            command2.ExecuteNonQuery();
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