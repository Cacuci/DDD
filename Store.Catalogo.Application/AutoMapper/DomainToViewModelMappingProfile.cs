using AutoMapper;
using Store.Catalogo.Application.ViewModels;
using Store.Catalogo.Domain.Models;

namespace Store.Catalogo.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(destinationMember: d => d.Largura, memberOptions: o => o.MapFrom(mapExpression: s => s.Dimensoes.Largura))
                .ForMember(destinationMember: d => d.Largura, memberOptions: o => o.MapFrom(mapExpression: s => s.Dimensoes.Altura))
                .ForMember(destinationMember: d => d.Largura, memberOptions: o => o.MapFrom(mapExpression: s => s.Dimensoes.Profundidade));
            
            CreateMap<Categoria, CategoriaViewModel>();
        }
    }
}
