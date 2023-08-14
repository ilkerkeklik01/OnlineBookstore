using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.BasketItems.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.BasketItems.Models
{
    public class BasketItemListModel : BasePageableModel
    {


        public List<BasketItemListDto> Items{ get; set; }


    }
}
