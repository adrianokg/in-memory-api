using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore3.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
