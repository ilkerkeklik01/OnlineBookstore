using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Dtos
{
    public class CreatedOrderDto : IComparable<CreatedOrderDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        
        public List<OrderItemForOrderDto> OrderItems { get; set; }
        //OrderItem ı orderItemForOrderDto ya maplemeliyim


        public int CompareTo(CreatedOrderDto? other)
        {
            if (other == null)
            {
                return 1; 
            }

            int idComparison = this.Id.CompareTo(other.Id);
            if (idComparison != 0)
            {
                return idComparison;
            }

            int userIdComparison = this.UserId.CompareTo(other.UserId);
            if (userIdComparison != 0)
            {
                return userIdComparison;
            }

            int userNameComparison = string.Compare(this.UserName, other.UserName, StringComparison.OrdinalIgnoreCase);
            if (userNameComparison != 0)
            {
                return userNameComparison;
            }

            int orderDateComparison = this.OrderDate.CompareTo(other.OrderDate);
            if (orderDateComparison != 0)
            {
                return orderDateComparison;
            }

            int totalPriceComparison = this.TotalPrice.CompareTo(other.TotalPrice);
            if (totalPriceComparison != 0)
            {
                return totalPriceComparison;
            }

            int orderItemsComparison = this.OrderItems.Count.CompareTo(other.OrderItems.Count);
            if (orderItemsComparison != 0)
            {
                return orderItemsComparison;
            }

            return 0;
        }
    }
}
