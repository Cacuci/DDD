using Store.Core.DomainObjects;
using System.Collections.Generic;

namespace Store.Catalago.Domain.Models
{
    public class Categoria : Entity
    {
        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        public ICollection<Produto> Produtos { get; set; }

        //EF Relation
        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }

        //EF Relation
        protected Categoria()
        {
        }

        public override string ToString()
        {
            return $"{Codigo} - {Nome}";
        }

        public void Validar()
        {
            AssertConcern.Empty(value: Nome, message: "O campo Nome da categoria não pode estar vazio");
            AssertConcern.Equals(object1: Codigo, object2: 0, message: "O campo Codigo não pode ser 0");
        }
    }
}