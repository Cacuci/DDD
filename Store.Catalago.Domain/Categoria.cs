using Store.Core.DomainObjects;

namespace Store.Catalago.Domain
{
    public class Categoria : Entity
    {
        public string Nome { get; private set; }
        public string Codigo { get; private set; }

        public Categoria(string nome, string codigo)
        {
            Nome = nome;
            Codigo = codigo;
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