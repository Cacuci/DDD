using Microsoft.Extensions.DependencyInjection;
using Store.Catalogo.Application.Services;
using Store.Catalogo.Domain.Repository;
using Store.Catalogo.Domain.Services;
using Store.Catalogo.Data;
using Store.Catalogo.Data.Repository;
using Store.Core.MediatR;

namespace Store.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();
        }
    }
}
