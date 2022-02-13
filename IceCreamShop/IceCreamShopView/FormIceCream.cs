using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace IceCreamShopView
{
    public partial class FormIceCream : Form
    {
        public int Id { set { id = value; } }

        private readonly IIceCreamLogic _logic;

        private int? id;

        private Dictionary<int, (string, int)> iceCreamComponents; 
 
        public FormIceCream(IIceCreamLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormIceCream_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    IceCreamViewModel view = _logic.Read(new IceCreamBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.IceCreamName;
                        textBoxPrice.Text = view.Price.ToString();
                        iceCreamComponents = view.IceCreamComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                iceCreamComponents = new Dictionary<int, (string, int) > ();
            }
        }

        private void LoadData()
        {
            try
            {
                if (iceCreamComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in iceCreamComponents)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormIceCreamComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (iceCreamComponents.ContainsKey(form.Id))
                {
                    iceCreamComponents[form.Id] = (form.ComponentName, form.Count);
                }
                else
                {
                    iceCreamComponents.Add(form.Id, (form.ComponentName, form.Count));
                }
                LoadData();
            }
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormIceCreamComponent>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = iceCreamComponents[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    iceCreamComponents[form.Id] = (form.ComponentName, form.Count);
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        iceCreamComponents.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (iceCreamComponents == null || iceCreamComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new IceCreamBindingModel
                {
                    Id = id,
                    IceCreamName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    IceCreamComponents = iceCreamComponents
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
