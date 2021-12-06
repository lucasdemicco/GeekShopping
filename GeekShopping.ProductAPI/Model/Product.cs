using GeekShopping.ProductAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductAPI.Model
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [Column("Name")]
        [Required]
        [StringLength(150, ErrorMessage = "Nome pode ter no máximo 150 caracteres")]
        public string Name { get; set; }

        [Column("Price")]
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }   
        
        [Column("Description")]
        [StringLength(500, ErrorMessage = "Descrição de no máximo 500 caracteres")]
        public string Description { get; set; }

        [Column("Category_Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Nome da categoria pode conter no máximo 50 caracteres")]
        public string CategoryName { get; set;}

        [Column("Image_Url")]
        [StringLength(300, ErrorMessage = "String URL de no máximo 300 caracteres")]
        public string ImageUrl { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var primeiraLetra = this.Name[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new
                    ValidationResult("A primeira letra deve ser maiúscula",
                new[]
                { nameof(this.Name) }
                );
            }

            if(this.Price < 0)
            {
                yield return new
                    ValidationResult("O preço deve ser maior ou igual a zero",
                    new[]
                    { nameof(this.Price) }
                    );
            }
        }
    }
}