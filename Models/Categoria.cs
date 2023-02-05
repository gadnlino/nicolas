
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula3108.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [Display(Name = "Nome da Categoria")]
        public String CategoriaNome { get; set; }

        public List<Produtos> Produtos { get; set; }

	}
}