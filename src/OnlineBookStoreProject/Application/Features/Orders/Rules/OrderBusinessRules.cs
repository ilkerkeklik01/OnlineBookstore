using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Orders.Rules
{
    public class OrderBusinessRules
    {

        IOrderRepository _orderRepository;
        IOrderItemRepository _orderItemRepository;
        IUserRepository _userRepository;
        public OrderBusinessRules(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _userRepository = userRepository;
        }


        public async Task UserNullCheck(int userId)
        {
            IPaginate<User> uPaginate = await _userRepository.GetListAsync(x => x.Id== userId);

            bool check = uPaginate.Items.Any();

            if (!check)
            {
                throw new BusinessException("User is not exist!");
            }


        }
        public async Task UserNullCheckAndCheckTheBasketOfUserIsEmpty(int userId)
        {
            await UserNullCheck(userId);

            IPaginate<OrderItem> oPaginate =await _orderItemRepository.GetListAsync(x => x.UserId == userId && x.IsInTheBasket == true);

            bool check = oPaginate.Items.Any();

            if (!check)
            {
                throw new BusinessException("Basket of the user is empty!");
            }

        }




    }
}
