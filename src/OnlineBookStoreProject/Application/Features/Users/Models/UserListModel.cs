using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Users.Models
{
    public class UserListModel  : BasePageableModel
    {
        public List<UserListDto> Items { get; set;}
    }
}
