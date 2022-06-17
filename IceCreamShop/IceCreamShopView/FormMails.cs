using System;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.BindingModels;
using System.Windows.Forms;
using Unity;

namespace IceCreamShopView
{
    public partial class FormMails : Form
    {
        private readonly IMessageInfoLogic _logic;

        private int pageNumber = 1;

        public FormMails(IMessageInfoLogic mailLogic)
        {
            _logic = mailLogic;
            InitializeComponent();
        }

        private void FormMails_Load(object sender, EventArgs e)
        {
            LoadData();
            textBoxPage.Text = pageNumber.ToString();
        }

        private void LoadData()
        {
            Program.ConfigGrid(_logic.Read(new MessageInfoBindingModel
            {
                PageNumber = pageNumber
            }), dataGridView);
            textBoxPage.Text = pageNumber.ToString();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            int stringsCountOnPage = _logic.Read(new MessageInfoBindingModel
            {
                PageNumber = pageNumber + 1
            }).Count;

            if (stringsCountOnPage != 0)
            {
                pageNumber++;
                LoadData();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (pageNumber > 1)
            {
                pageNumber--;
            }

            LoadData();
        }

        private void textBoxPage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxPage.Text != "")
                {
                    int pageNumberValue = Convert.ToInt32(textBoxPage.Text);

                    if (pageNumberValue < 1)
                    {
                        throw new Exception();
                    }

                    int stringsCountOnPage = _logic.Read(new MessageInfoBindingModel
                    {
                        PageNumber = pageNumberValue
                    }).Count;

                    if (stringsCountOnPage == 0)
                    {
                        throw new Exception();
                    }

                    pageNumber = pageNumberValue;
                    LoadData();
                }
            }
            catch (Exception)
            {
                textBoxPage.Text = pageNumber.ToString();
            }
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                string id = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                var form = Program.Container.Resolve<FormMailReply>();
                form.MessageId = id;
                _logic.CreateOrUpdate(new MessageInfoBindingModel
                {
                    MessageId = id,
                    IsRead = true
                });
                form.ShowDialog();
                LoadData();
            }
            
        }
    }
}
