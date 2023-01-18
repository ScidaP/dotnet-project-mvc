using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;

public class Usuario {
    private int id;
    private string nombre;
    private string usuario;
    private string pass;
    private string rol;

    public Usuario() {
        nombre = "";
        usuario = "";
        pass = "";
        rol = "";
    }

    public Usuario(int id1, string nombre1, string usuario1, string pass1, string rol1) {
        id = id1;
        nombre = nombre1;
        usuario = usuario1;
        pass = pass1;
        rol = rol1;
    }
    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Usuario1 { get => usuario; set => usuario = value; }
    public string Pass { get => pass; set => pass = value; }
    public string Rol { get => rol; set => rol = value; }
}