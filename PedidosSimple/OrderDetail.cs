using PedidosSimple.Data;
using PedidosSimple.Data.Services.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PedidosSimple
{
    public partial class OrderDetail : Form
    {
        private int OrderId;
        public OrderDetail(int? OrderId)
        {
            InitializeComponent();
            this.OrderId = OrderId.Value;
        }

        private async void OrderDetail_Load(object sender, EventArgs e)
        {
            OrderRepo orderRepo = new OrderRepo();
            var Order = await orderRepo.GetById(OrderId);
            txtId.Text = Order.Id.ToString();
            txtOrderDate.Text = Order.OrderDate.ToString();
            txtName.Text = $"{Order.Client.Name} {Order.Client.SureName}";
            txtPhone.Text = Order.Client.PhoneNumber;
            txtEmail.Text = Order.Client.Email;
            txtAddress.Text = Order.Client.Address;
            txtTotal.Text = Order.Products.Sum(x => x.Total).ToString("N2");
            loadProducts(Order.Products.ToList());
        }

        private void loadProducts(List<ProductOrder> data)
        {
            foreach (var dt in data)
            {
                dataGridView1.Rows.Add(dt.Product.Name,dt.UnitPrice,dt.Amount,dt.Total);
            }
        }
    }
}
