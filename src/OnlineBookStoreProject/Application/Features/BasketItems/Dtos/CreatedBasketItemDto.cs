using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BasketItems.Dtos
{
    public class CreatedBasketItemDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OrderItemId { get; set; }



    }
}
