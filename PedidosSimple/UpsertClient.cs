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
    public partial class UpsertClient : Form
    {
        private Client Client;
        private int ClientId;
        private readonly ClientRepo clientRepo;
        public UpsertClient(int? ClientId)
        {
            InitializeComponent();
            clientRepo = new ClientRepo();
          
           this.ClientId = ClientId.Value;
        }

        private void loadClientData()
        {
            txtName.Text = Client.Name;
            txtSurename.Text = Client.SureName;
            txtPhone.Text = Client.PhoneNumber;
            txtEmail.Text = Client.Email;
            txtAddress.Text = Client.Address;
        }

    private async void UpsertClient_Load(object sender, EventArgs e)
    {
        if (ClientId != 0)
        {
            Client = await clientRepo.GetById(ClientId);
            loadClientData();
        }
        else
        {
                Client = new Client();
        }
    }
        private async void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                Client.Name = txtName.Text;
                Client.SureName = txtSurename.Text;
                Client.PhoneNumber = txtPhone.Text;
                Client.Email = txtEmail.Text;
                Client.Address = txtAddress.Text;
                if (Client.Id != 0)
                {
                    if (await clientRepo.Update(Client))
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
                    if (await clientRepo.Add(Client))
                    {
                        MessageBox.Show("Se ingreso con exito");
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error en la operacion");
                        return;
                    }
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Falta datos por ingresar");
            }
        }
    }
}
