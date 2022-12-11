using Tp4MvcNuevo.Models;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;

public class PerfilDeMapeo : Profile {
    public PerfilDeMapeo() {
        CreateMap<Cadete, MostrarCadeteViewModel>().ReverseMap();
        CreateMap<List<Pedido>, ListarPedidosViewModels>().ReverseMap();
        CreateMap<Pedido, HacerPedidoViewModel>().ReverseMap();
    }
}