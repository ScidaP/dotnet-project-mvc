using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public interface IRepositorioPedidos {
    public Pedido getPedido(int id);
    public void agregarPedido(Pedido ped);
    //public void eliminarPedido(int id);
    //public List<Pedido> getTodosPedidos();
    //public void actualizarPedido(Pedido cad);
}

public class RepositorioPedidos : IRepositorioPedidos {
    public Pedido getPedido(int id) {
        Pedido Pedido = new Pedido();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM pedidos WHERE id = $id";
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
}