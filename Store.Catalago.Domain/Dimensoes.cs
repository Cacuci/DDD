using Store.Core.DomainObjects;

namespace Store.Catalago.Domain
{
    public class Dimensoes
    {
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Profundidade { get; set; }

        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            AssertConcern.SmallThatlMin(value: altura, min: 1, message: "O campo Altura não pode ser menor ou igual a 0");
            AssertConcern.SmallThatlMin(value: largura, min: 1, message: "O campo Largura não pode ser menor ou igual 0");
            AssertConcern.SmallThatlMin(value: profundidade, min: 1, message: "O campo Profundidade não pode ser menor ou igual 0");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public string DescriçaoFormatada()
        {
            return $"LxAxP: {Largura} x {Altura} x {Profundidade}";
        }

        public override string ToString()
        {
            return DescriçaoFormatada();
        }
    }
}