using PedidosSimple.Data;
using PedidosSimple.Data.Services.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PedidosSimple
{
    public partial class ProductList : Form
    {
        private ProductRepo productRepo;
        private OrderRepo orderRepo;
        public ProductList()
        {
            InitializeComponent();
            productRepo = new ProductRepo();
            orderRepo = new OrderRepo();
        }

        private async void ProductList_Load(object sender, EventArgs e)
        {
            LoadList(await productRepo.Fill());
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            UpsertProduct client = new UpsertProduct(0);
            client.ShowDialog();
            client.Dispose();
            productRepo = new ProductRepo();
            LoadList(await productRepo.Fill());
        }

        private void LoadList(IEnumerable<Product> ClientList)
        {
            dataGridView1.Rows.Clear();
            foreach (var data in ClientList)
            {
                dataGridView1.Rows.Add(data.Id, data.Name , data.Price);
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                try
                {
                    UpsertProduct product = new UpsertProduct(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    product.ShowDialog();
                    product.Dispose();
                    productRepo = new ProductRepo();
                    orderRepo = new OrderRepo();
                    LoadList(await productRepo.Fill());
                }
                catch
                {

                }
            }
            else if (e.ColumnIndex == 4)
            {
                if (MessageBox.Show(this, "Esta seguro que desea eliminar este producto?", "Eliminar", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    if (!await orderRepo.IsProductInOrder(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())))
                    {
                        var result = await productRepo.Delete(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                        if (result)
                        {
                            LoadList(await productRepo.Fill());
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
}
