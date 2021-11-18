using Store.Core.DomainObjects;
using System;

namespace Store.Catalogo.Domain.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public Guid CategoriaID { get; set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }

        public Dimensoes Dimensoes { get; set; }
        
        public Categoria Categoria { get; private set; }

        private Produto()
        {
        }

        public Produto(string nome, string descricao, bool ativo, decimal valor, Guid categoriaID, DateTime dataCadastro, string imagem, Dimensoes dimensoes)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            CategoriaID = categoriaID;
            DataCadastro = dataCadastro;
            Imagem = imagem;
            Dimensoes = dimensoes;

            Validar();
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarCategoria(Categoria categoria)
        {
            Categoria = categoria;
            CategoriaID = categoria.ID;
        }

        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0)
            {
                quantidade *= -1;
            }

            QuantidadeEstoque -= quantidade;
        }

        public void ReporEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public bool PossuiEstoque(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }

        public void Validar()
        {
            AssertConcern.Empty(value: Nome, message: "O campo Nome do produto não pode estar vazio");
            AssertConcern.Empty(value: Descricao, message: "O campo Descricao do produto não pode estar vazio");
            AssertConcern.Equals(object1: CategoriaID, object2: Guid.Empty, "O campo CategoriaID do produto não pode estar vazio");
            AssertConcern.SmallEquallMin(value: Valor, min:  0, "O campo Valor do produto não pode se menor igual a 0");
            AssertConcern.Empty(value: Imagem, message: "O campo Imagem do produto não pode estar vazio");
        }
    }
}
