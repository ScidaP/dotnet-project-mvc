using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public class RepositorioCadetes {
    public Cliente getCliente(int id) {
        using (var conexion = new SQLiteConnection("../bin/Debug/net6.0/DB/basededatos.db")) {
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cliente WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    var Cliente = 
                }
            }
        }
    }
}