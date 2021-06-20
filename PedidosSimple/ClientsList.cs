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
    public partial class ClientsList : Form
    {
        
        private  ClientRepo clientRepo;
        private  OrderRepo orderRepo;
        public ClientsList()
        {
            InitializeComponent();
            clientRepo = new ClientRepo();
            orderRepo = new OrderRepo();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            UpsertClient client = new UpsertClient(0);
            client.ShowDialog();
            client.Dispose();
            clientRepo = new ClientRepo();
            LoadList(await clientRepo.Fill());
        }

        private async void ClientsList_Load(object sender, EventArgs e)
        {
            LoadList(await clientRepo.Fill());
        }

        private void LoadList(IEnumerable<Client> ClientList)
        {
            
            dataGridView1.Rows.Clear();
            foreach (var data in ClientList)
            {
                dataGridView1.Rows.Add(data.Id, data.Name + " " + data.SureName, data.PhoneNumber, data.Email, data.Address);
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 5)
            {
                try
                {
                    UpsertClient client = new UpsertClient(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    client.ShowDialog();
                    client.Dispose();
                    clientRepo = new ClientRepo();
                    orderRepo = new OrderRepo();
                    LoadList(await clientRepo.Fill());
                }
                catch
                {

                }
            }
            else if(e.ColumnIndex == 6)
            {
                if(MessageBox.Show(this,"Esta seguro que desea eliminar este cliente?", "Eliminar", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    if ( !await orderRepo.IsClientInOrder(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())))
                    {
                     var result =  await  clientRepo.Delete(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                        if (result)
                        {
                            LoadList(await clientRepo.Fill());
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
