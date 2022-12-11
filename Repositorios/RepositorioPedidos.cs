using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public interface IRepositorioPedidos {
    public Pedido getPedido(int id);
    public void agregarPedido(Pedido ped);
    public void eliminarPedido(int id);
    public List<Pedido> getTodosPedidos();
    public void actualizarPedido(Pedido ped);
    public int getIdCliente(long telefono); //Te devuelve el id (o null) de un cliente al buscarlo por su numero de telefono
    public int getIdCadete(string nombre);
}

public class RepositorioPedidos : IRepositorioPedidos {
    public Pedido getPedido(int id) {
        Pedido Pedido = new Pedido();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM pedidos WHERE nro = $id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    Pedido = new Pedido(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4));
                }
            }
            conexion.Close();
        }
        return Pedido;
    }

    public void agregarPedido(Pedido ped) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"INSERT INTO pedidos(obs, cliente, estado, cadete) VALUES ($obs, $cliente, $estado, $cadete)";
            command.Parameters.AddWithValue("$obs", ped.Obs);
            command.Parameters.AddWithValue("$cliente", ped.idCliente1);
            command.Parameters.AddWithValue("$estado", ped.Estado);
            command.Parameters.AddWithValue("$cadete", ped.idCadete1);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void eliminarPedido(int id) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"DELETE FROM pedidos WHERE nro=$id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public List<Pedido> getTodosPedidos() {
        List<Pedido> ListaPedidos = new List<Pedido>();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM pedidos";
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    var pedido = new Pedido(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4));
                    ListaPedidos.Add(pedido);
                }
            }
            conexion.Close();
        }
        return ListaPedidos;
    }

    public void actualizarPedido(Pedido ped) {
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"UPDATE pedidos SET obs = $obs, cliente = $cliente, estado = $estado, cadete = $cadete WHERE nro = $id";
            command.Parameters.AddWithValue("$obs", ped.Obs);
            command.Parameters.AddWithValue("$cliente", ped.idCliente1);
            command.Parameters.AddWithValue("$estado", ped.Estado);
            command.Parameters.AddWithValue("$cadete", ped.idCadete1);
            command.Parameters.AddWithValue("$id",  ped.Numero);
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public int getIdCliente(long telefono) {
        int idCliente = 0;
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT id FROM cliente WHERE telefono like $telefono";
            command.Parameters.AddWithValue("$telefono", telefono);
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    if (reader.GetInt32(0) >= 0) {
                        idCliente = reader.GetInt32(0);
                    }
                }
            }
        }
        return idCliente;
    }

    public int getIdCadete(string nombre) {
        int idCadete = 0;
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT id FROM cadete WHERE nombre like $nombre";
            command.Parameters.AddWithValue("$nombre", nombre);
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    if (reader.GetInt32(0) >= 0) {
                        idCadete = reader.GetInt32(0);
                    }
                }
            }
        }
        return idCadete;
    }
}