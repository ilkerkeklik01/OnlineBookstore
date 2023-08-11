using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Paging;

namespace Application.Features.OrderItems.Models
{
    public class OrderItemListModel:BasePageableModel
    {

        public List<OrderItemListDto> Items { get; set; }


    }
}
