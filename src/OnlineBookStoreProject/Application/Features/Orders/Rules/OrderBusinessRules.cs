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

        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUserRepository _userRepository;
        public OrderBusinessRules(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _userRepository = userRepository;
        }


        public async Task UserNullCheck(int userId)
        {
            User? user = await _userRepository.GetAsync(x => x.Id == userId);
            bool userCheck = user!=null ;

            if (!userCheck)
            {
                throw new BusinessException("User is not exist!");
            }


        }
        public async Task UserNullCheckAndCheckTheBasketOfUserIsEmpty(int userId)
        {
            await UserNullCheck(userId);

            IPaginate<OrderItem> oPaginate =await _orderItemRepository.GetListAsync(x => x.UserId == userId && x.IsInTheBasket == true);

            bool checkEmpty = oPaginate.Items.Any();

            if (!checkEmpty)
            {
                throw new BusinessException("Basket of the user is empty!");
            }

        }




    }
}
