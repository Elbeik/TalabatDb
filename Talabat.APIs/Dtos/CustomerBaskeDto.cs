using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Talabat.Domine.Entites;

namespace Talabat.APIs.Dtos
{
    public class CustomerBaskeDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
