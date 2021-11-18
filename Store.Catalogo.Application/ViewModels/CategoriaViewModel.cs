using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Catalogo.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Codigo { get; set; }
    }
}
