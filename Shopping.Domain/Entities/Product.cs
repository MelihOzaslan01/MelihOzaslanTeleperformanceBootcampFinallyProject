using System.ComponentModel.DataAnnotations;

namespace Shopping.Domain.Entities
{
    public class Product : IBaseEntity
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingListId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Amount { get; set; }

    }
}
