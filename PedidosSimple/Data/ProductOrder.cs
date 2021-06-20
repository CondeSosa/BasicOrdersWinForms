using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PedidosSimple.Data
{
    public class ProductOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public double UnitPrice { get; set; }
        public double Amount { get; set; }
        public virtual double Total => GetTotal();
        private double GetTotal()
        {
            return Amount * UnitPrice;
        }
        }
    }
