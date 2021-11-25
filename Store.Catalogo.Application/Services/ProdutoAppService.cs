using AutoMapper;
using Store.Catalogo.Application.ViewModels;
using Store.Catalogo.Domain.Models;
using Store.Catalogo.Domain.Repository;
using Store.Catalogo.Domain.Services;
using Store.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMapper _mapper;

        public ProdutoAppService(IProdutoRepository produtoRepository, 
                                 IEstoqueService estoqueService,
                                 IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
            _mapper = mapper;
        }

        public async Task AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);

            _produtoRepository.Adicionar(produto);

            await _produtoRepository.UnityOfWork.Commit();
        }

        public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);

            _produtoRepository.Atualizar(produto);

            await _produtoRepository.UnityOfWork.Commit();
        }

        public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
            {
                throw new DomainException(message: "Falha ao debitar estoque");
            }

            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorID(id));
        }

        public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.ReporEstoque(id, quantidade).Result)
            {
                throw new DomainException(message: "Falha ao repor estoque");
            }

            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorID(id));
        }        

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _produtoRepository.ObterCategorias());
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterPorCategoria(codigo));
        }

        public async Task<ProdutoViewModel> ObterPorID(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorID(id));
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
            _estoqueService?.Dispose();
        }
    }
}
