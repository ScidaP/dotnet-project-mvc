using Tp4MvcNuevo.Models;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;

public class PerfilDeMapeo : Profile {
    public PerfilDeMapeo() {
        CreateMap<Cadete, MostrarCadeteViewModel>().ReverseMap();
        CreateMap<Pedido, HacerPedidoViewModel>().ReverseMap();
        CreateMap<Pedido, MostrarPedidoViewModel>().ReverseMap();
        CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        CreateMap<Cadete, CargarCadeteViewModel>().ReverseMap();
        CreateMap<Cadete, ActualizarCadeteViewModel>().ReverseMap();
        CreateMap<Cliente, MostrarClienteViewModel>().ReverseMap();
        CreateMap<Cadeteria, MostrarCadeteriaViewModel>().ReverseMap();
        CreateMap<Usuario, IniciarSesionViewModel>().ReverseMap();
        CreateMap<Cadeteria, CadeteriaViewModel>().ReverseMap();
        CreateMap<Usuario, AgregarUsuarioViewModel>().ReverseMap();
        CreateMap<Usuario, ActualizarUsuarioViewModel>().ReverseMap();
    }
}