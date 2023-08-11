using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Bookshelves.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Bookshelves.Models
{
    public class BookshelfListModel : BasePageableModel
    {

        public List<BookshelfListDto> Items { get; set; }

    }
}
