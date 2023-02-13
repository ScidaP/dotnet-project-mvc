using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public interface IRepositorioUsuarios {
    public Usuario GetUsuario(int id);
    public int DatosCorrectos(string? usuario, string? pass);
    public void AgregarUsuario(Usuario usuario);
    public List<Usuario> GetTodosUsuarios();
    public void EliminarUsuario(int id);
}

public class RepositorioUsuarios : IRepositorioUsuarios {

    public void EliminarUsuario(int id) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"DELETE FROM usuarios WHERE id=$id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void AgregarUsuario(Usuario usuario) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"INSERT INTO usuarios(nombre, usuario, pass, rol) VALUES ($nombre, $usuario, $pass, $rol)";
            command.Parameters.AddWithValue("$nombre", usuario.Nombre);
            command.Parameters.AddWithValue("$usuario", usuario.Usuario1);
            command.Parameters.AddWithValue("$pass", usuario.Pass);
            command.Parameters.AddWithValue("$rol", usuario.Rol);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public List<Usuario> GetTodosUsuarios() {
        var ListaUsuarios = new List<Usuario>();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM usuarios";
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    var usuario = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                    ListaUsuarios.Add(usuario);
                }
            }
            conexion.Close();
        }
        return ListaUsuarios;
    }
    public Usuario GetUsuario(int id) {
        Usuario usuario = new Usuario();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM usuarios WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    usuario = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                }
            }
            conexion.Close();
        }
        return usuario;
    }

    public int DatosCorrectos(string? usuario, string? pass) { // Devuelve -1 si NO encontró el usuario, o el id del usuario que SI haya encontrado.
        int res = -1;
        if (String.IsNullOrEmpty(usuario) || String.IsNullOrEmpty(pass)) {
            return -1;
        } else {
            using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
                conexion.Open();
                var command = conexion.CreateCommand();
                command.CommandText = @"SELECT * FROM usuarios WHERE usuario = $usuario AND pass = $pass";
                command.Parameters.AddWithValue("$usuario", usuario);
                command.Parameters.AddWithValue("$pass", pass);
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            if (reader.HasRows) { 
                                res = reader.GetInt32(0); // Devuelvo el id
                            } else {
                                res = -1;
                            }
                        }
                    }
                conexion.Close();
                return res;
            }
        }
    }
}