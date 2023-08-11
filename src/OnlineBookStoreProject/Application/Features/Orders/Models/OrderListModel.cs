using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Dtos;
using Core.Application.Requests;
using Core.Persistence.Paging;

namespace Application.Features.Orders.Models
{
    public class OrderListModel:BasePageableModel
    {
        public List<OrderListDto> Items { get; set; }


    }
}
