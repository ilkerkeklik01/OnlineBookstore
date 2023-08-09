using Application.Features.Books.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Models
{
    public class BookListModel : BasePageableModel
    {
        public IList<BookListDto> Items { get; set; }

    }
}
