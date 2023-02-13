using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public interface IRepositorioClientes {
    public Cliente getCliente(int id);

    public void agregarCliente(Cliente cli);

    public void eliminarCliente(int id);

    public void actualizarCliente(Cliente cli);

    public List<Cliente> getTodosClientes();

    public bool existeCliente(int id);
}

public class RepositorioClientes : IRepositorioClientes {

    public bool existeCliente(int id) {
        bool res = false;
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cliente WHERE activo=1 AND id=$id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                res = reader.HasRows;
            }
            conexion.Close();
        }
        return res;
    }
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
            command.CommandText = @"INSERT INTO cliente(nombre, direccion, telefono, referenciaDireccion, activo) VALUES ($nombre, $direccion, $telefono, $ref, 1)";
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
            // Ahora cambio a inactivos a todos los pedidos que hizo/hace este cliente.
            var command2 = conexion.CreateCommand();
            command2.CommandText = @"UPDATE pedidos SET activo=0 WHERE cliente=$id";
            command2.Parameters.AddWithValue("$id", id);
            command2.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void actualizarCliente(Cliente cli) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"UPDATE cliente SET nombre = $nombre, direccion = $direccion, telefono = $telefono, referenciaDireccion = $ref WHERE id = $id";
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
            command.CommandText = @"SELECT * FROM cliente WHERE activo=1";
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