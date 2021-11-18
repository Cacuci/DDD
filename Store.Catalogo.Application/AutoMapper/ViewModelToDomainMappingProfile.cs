using AutoMapper;
using Store.Catalogo.Application.ViewModels;
using Store.Catalogo.Domain.Models;

namespace Store.Catalogo.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProdutoViewModel, Produto>()
                .ConstructUsing(ctor: p => new Produto(p.Nome, p.Descricao, p.Ativo, p.Valor, p.CategoriaID, p.DataCadastro, p.Imagem,
                                           new Dimensoes(p.Largura, p.Largura, p.Profundidade)));

            CreateMap<CategoriaViewModel, Categoria>()
                .ConstructUsing(ctor: c => new Categoria(c.Nome, c.Codigo));
        }
    }
}
