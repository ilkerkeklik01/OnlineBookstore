using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bookshelves.Dtos
{
    public class CreatedBookshelfDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }


    }
}
