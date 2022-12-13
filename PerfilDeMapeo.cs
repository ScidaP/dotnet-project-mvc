using Tp4MvcNuevo.Models;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;

public class PerfilDeMapeo : Profile {
    public PerfilDeMapeo() {
        CreateMap<Cadete, MostrarCadeteViewModel>().ReverseMap();
        CreateMap<Pedido, HacerPedidoViewModel>().ReverseMap();
        CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        CreateMap<Cadete, CargarCadeteViewModel>().ReverseMap();
    }
}