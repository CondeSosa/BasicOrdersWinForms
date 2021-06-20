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
    public partial class UpsertProduct : Form
    {
        private Product product;
        private int ProductId;
        private readonly ProductRepo productRepo;
        public UpsertProduct(int? productId)
        {
            InitializeComponent();
            productRepo = new ProductRepo();
            this.ProductId = productId.Value;
        
        }

        private void loadClientData()
        {
            txtName.Text = product.Name;
            txtPrice.Text = product.Price.ToString();
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                try
                {
                    product.Name = txtName.Text;
                    product.Price = double.Parse(txtPrice.Text);

                    if (product.Id != 0)
                    {
                        if (await productRepo.Update(product))
                        {
                            MessageBox.Show("Se actualizo con exito");
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error en la operacion");
                            return;
                        }
                    }
                    else
                    {
                        if (await productRepo.Add(product))
                        {
                            MessageBox.Show("Se ingreso con exito");
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error en la operacion");
                            return;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Ocurrio un error en la operacion");
                    return;
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Falta datos por ingresar");
            }
        }

        private async void UpsertProduct_Load(object sender, EventArgs e)
        {
            if (ProductId != 0)
            {
                product = await productRepo.GetById(ProductId);
                loadClientData();
            }
            else
            {
                product = new Product();
            }
        }
    }
}
