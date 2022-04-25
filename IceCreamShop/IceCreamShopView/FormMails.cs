using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.ViewModels;
using IceCreamShopContracts.BindingModels;
using System.Windows.Forms;

namespace IceCreamShopView
{
    public partial class FormMails : Form
    {
        private readonly IMessageInfoLogic _logic;

        public FormMails(IMessageInfoLogic mailLogic)
        {
            _logic = mailLogic;
            InitializeComponent();
        }

        private void FormMails_Load(object sender, EventArgs e)
        {
            var list = _logic.Read(null);
            if (list != null)
            {
                dataGridView.DataSource = list;
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
