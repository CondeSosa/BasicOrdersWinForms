using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosSimple.Data
{
   public class OrderItems
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public virtual double Total => GetTotal();

        private double GetTotal()
        {
            return  Amount * Price;
        }


    }
}
