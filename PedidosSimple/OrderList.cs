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
    public partial class OrderList : Form
    {
        OrderRepo orderRepo;
        ProductOrderRepo productOrder;
        public OrderList()
        {
            InitializeComponent();
            orderRepo = new OrderRepo();
            productOrder = new ProductOrderRepo();
        }

        private async void OrderList_Load(object sender, EventArgs e)
        {
            LoadList(await orderRepo.Fill());
        }

        private void LoadList(IEnumerable<Order> dataList)
        {
            dataGridView1.Rows.Clear();
            foreach(var data in dataList.OrderByDescending(x =>x.Id))
            {
                dataGridView1.Rows.Add(data.Id, data.OrderDate, data.Client.Name + " " + data.Client.SureName, data.Products.Sum(x => x.Total));
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                try
                {
                    OrderDetail product = new OrderDetail(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    product.ShowDialog();
                    product.Dispose();
                }
                catch
                {

                }
            }
            else if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show(this, "Esta seguro que desea eliminar este pedido?", "Eliminar", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
               
                        await productOrder.DeleteOrder(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                        orderRepo = new OrderRepo();
                        var result = await orderRepo.Delete(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                       
                        if (result)
                        {
                            LoadList(await orderRepo.Fill());
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error al intentar eliminar");
                        }
                    
                }
            }
        }
    }
}
