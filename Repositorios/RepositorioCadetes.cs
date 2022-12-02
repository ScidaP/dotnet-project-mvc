using System.Data.SQLite;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;

public interface IRepositorioCadetes {
    public Cadete getCadete(int id);
}

public class RepositorioCadetes : IRepositorioCadetes {
    public Cadete getCadete(int id) {
        Cadete cadete = new Cadete();
        using (var conexion = new SQLiteConnection("Data Source=DB/basededatos.db")) {
            conexion.Open();
            var command = conexion.CreateCommand();
            command.CommandText = @"SELECT * FROM cliente WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    int id1 = reader.GetInt32(0);
                    //cadete = new Cadete(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
                }
            }
            conexion.Close();
        }
        return cadete;
    }
}