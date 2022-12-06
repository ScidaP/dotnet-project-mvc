using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public interface IRepositorioClientes {

}

public class RepositorioClientes : IRepositorioClientes {
    public Cliente getCliente(int id) {
        Cliente cliente = new Cliente();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cliente WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    cliente = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt64(3), reader.GetString(4));
                }
            }
            conexion.Close();
        }
        return cliente;
    }

    public void agregarCliente(Cliente cli) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"INSERT INTO cliente(nombre, direccion, telefono, referenciaDireccion) VALUES ($nombre, $direccion, $telefono, $ref)";
            command.Parameters.AddWithValue("$nombre", cli.Nombre);
            command.Parameters.AddWithValue("$direccion", cli.Direccion);
            command.Parameters.AddWithValue("$telefono", cli.Telefono);
            command.Parameters.AddWithValue("$ref", cli.ReferenciasDireccion);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void eliminarCliente(int id) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"DELETE FROM cliente WHERE id=$id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void actualizarCliente(Cliente cli) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"UPDATE cadete SET nombre = $nombre, direccion = $direccion, telefono = $telefono, referenciaDireccion = $ref WHERE id = $id";
            command.Parameters.AddWithValue("$nombre", cli.Nombre);
            command.Parameters.AddWithValue("$direccion", cli.Direccion);
            command.Parameters.AddWithValue("$telefono", cli.Telefono);
            command.Parameters.AddWithValue("$ref", cli.ReferenciasDireccion);
            command.Parameters.AddWithValue("$id", cli.Id);
            int resultado = command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public List<Cliente> getTodosClientes() {
        List<Cliente> ListaClientes = new List<Cliente>();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cliente";
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    var cliente = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt64(3), reader.GetString(4));
                    ListaClientes.Add(cliente);
                }
            }
            conexion.Close();
        }
        return ListaClientes;
    }
}