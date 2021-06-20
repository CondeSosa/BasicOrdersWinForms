using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PedidosSimple.Data
{
   public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        public DateTime OrderDate { get; set; }


        public virtual Client Client { get; set; }
        public virtual ICollection<ProductOrder> Products { get; set; }


    }
}
