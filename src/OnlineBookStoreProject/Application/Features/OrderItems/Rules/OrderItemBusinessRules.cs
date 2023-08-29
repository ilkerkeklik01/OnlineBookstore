using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Dtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.OrderItems.Rules
{
    public class OrderItemBusinessRules 
    {

        private readonly IOrderItemRepository _orderItemRepository;


        public OrderItemBusinessRules(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;

        }

        public async Task<OrderItem> OrderItemMustExistsWhenRequested(int id)
        {
            OrderItem orderItem = await _orderItemRepository.GetAsync(x=>x.Id==id);

            if (orderItem == null)
            {
                throw new BusinessException("Requested Order Item does not exist!");
            }
            return orderItem;
        }

        public async Task<OrderItem> QuantityOfOrderItemCannotBeMoreThanTenAndNullCheckFirst(int id)
        {
            OrderItem orderItem =  await OrderItemMustExistsWhenRequested(id);


            if (orderItem.Quantity==10 )
            {
                throw new BusinessException("Quantity of Order Item cannot exceed 10!");
            }
            return orderItem;

        }
        public async Task<OrderItem> QuantityOfOrderItemCannotBeLessThanZeroAndNullCheckFirst(int id)
        {
            OrderItem orderItem = await OrderItemMustExistsWhenRequested(id);

            if (orderItem.Quantity == 0)
            {
                throw new BusinessException("Quantity of Order Item cannot be less than 0!");
            }

            return orderItem;
        }

        public async Task CannotIncreseTheQuantityOfOrderItemNotInTheBasket(bool isInTheBasket)
        {
            if (!isInTheBasket)
            {
                throw new BusinessException("Cannot increase the quantity of order item that is not in the basket!");
            }

        }

        public async Task CannotCreateOrderItemWithExistingBookIfItIsInTheBasketOfTheSameUser(int bookId,int userId)
        {
            IPaginate<OrderItem> orderItems = await _orderItemRepository.GetListAsync(x=>x.BookId==bookId && x.IsInTheBasket&& x.UserId==userId);
            bool isAny=orderItems.Items.Any();

            if (isAny)
            {
                throw new BusinessException("You cannot create an order item with existing book if it is in the basket of the same user! Try to increase quantity.");
            }

        }

        public async Task CannotDecreaseTheQuantityOfOrderItemNotInTheBasket(bool isInTheBasket)
        {
            if (!isInTheBasket)
            {
                throw new BusinessException("Cannot decrease the quantity of order item that is not in the basket!");
            }

        }



    }
}
