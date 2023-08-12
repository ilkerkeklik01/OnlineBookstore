using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Reviews.Models
{
    public class ReviewListModel : BasePageableModel
    {

        public List<ReviewListDto> Items { get; set; }


    }
}
