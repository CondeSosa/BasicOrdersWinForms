using PedidosSimple.Data;
using PedidosSimple.Data.Services.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PedidosSimple
{
    public partial class Pedidos : Form
    {
        private ProductRepo productRepo;
        private ClientRepo clientRepo;
        private OrderRepo orderRepo;
        private ProductOrderRepo productOrderRepo;
        private List<OrderItems> OrderItems;
        private IEnumerable<Client> Clients;
        public Pedidos()
        {
            InitializeComponent();
            orderRepo = new OrderRepo();
            productOrderRepo = new ProductOrderRepo();
            clientRepo = new ClientRepo();
            productRepo = new ProductRepo();
            OrderItems = new List<OrderItems>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientsList clientsList = new ClientsList();
            clientsList.ShowDialog();
            clientsList.Dispose();
            clientRepo = new ClientRepo();
            LoadClientComboBox();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            ProductList productList = new ProductList();
            productList.ShowDialog();
            productList.Dispose();
            productRepo = new ProductRepo();
            textBox1.Text = "";
            LoadProductList(await productRepo.Fill());
        }

        private async void Pedidos_Load(object sender, EventArgs e)
        {
            LoadProductList(await productRepo.Fill());
            LoadClientComboBox();
        }

        private void LoadProductList(IEnumerable<Product> products)
        {
            flowLayoutPanel1.Controls.Clear();

            foreach(var data in products)
            {
                var btn = new Button();
                btn.Name = data.Id.ToString();
                btn.Text = $"{data.Name} {data.Price.ToString("N2")}";
                btn.Height = 80;
                btn.Width = 85;
                btn.FlatStyle = FlatStyle.Popup;
                btn.BackColor = Color.DeepSkyBlue;
                btn.Click += OnProductBtnClick;
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
        private async void LoadClientComboBox()
        {
            Clients = await clientRepo.Fill();
            this.cbbClient.DataSource = Clients;
            this.cbbClient.DisplayMember = "FullClient";
            this.cbbClient.ValueMember = "Id";

        }

        private async void OnProductBtnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var product = await productRepo.GetById(int.Parse(btn.Name));
            int TempId = 1;
            if (OrderItems.Any())
            {
                TempId = OrderItems.Max(x => x.Id + 1);
            }

            OrderItems.Add(new Data.OrderItems
            {
                Id = TempId,
                ProductId = int.Parse(btn.Name),
                Amount = 1,
                Price = product.Price,
                ProductName = product.Name
            });
            LoadOrderList();
        }

        private void LoadOrderList()
        {
            dataGridView1.Rows.Clear();
            foreach(var data in OrderItems)
            {
                dataGridView1.Rows.Add(data.Id, data.ProductName, data.Price, data.Amount, data.Total);
            }

            btnSave.Text = $"Generar Pedido {OrderItems.Sum(x =>x.Total).ToString("N2")}";
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (OrderItems.Any())
            {
                try
                {
                    var orderData = new Order
                    {
                        ClientId = int.Parse(cbbClient.SelectedValue.ToString()),
                        OrderDate = DateTime.Now
                    };
                    orderData = await orderRepo.Add(orderData);

                    var items = new List<ProductOrder>();

                    foreach (var itm in OrderItems)
                    {
                        items.Add(new ProductOrder
                        {
                            OrderId = orderData.Id,
                            ProductId = itm.ProductId,
                            Amount = itm.Amount,
                            UnitPrice = itm.Price
                        });
                    }

                    await productOrderRepo.Add(items);

                    OrderItems.Clear();
                    LoadOrderList();
                    MessageBox.Show("El pedido se genero con exito");

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            var products = await productRepo.Fill();
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                LoadProductList(products.Where(x => x.Name.ToLower().Contains(textBox1.Text.ToLower())));
            }
            else
            {
                LoadProductList(await productRepo.Fill());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 5)
            {
                var data = OrderItems.FirstOrDefault(x => x.Id == int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                OrderItems.Remove(data);
                LoadOrderList();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (OrderItems != null && OrderItems.Any())
            {
                try
                {
                    var index = OrderItems.FindIndex(x => x.Id == int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    OrderItems[index].Price = double.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    OrderItems[index].Amount = double.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                    LoadOrderList();
                }
                catch(Exception)  {  }
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OrderList orderList = new OrderList();
            orderList.ShowDialog();
            orderList.Dispose();
        }
    }
}
