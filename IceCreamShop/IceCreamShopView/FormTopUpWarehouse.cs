using System;
using System.Windows.Forms;
using System.Collections.Generic;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.ViewModels;

namespace IceCreamShopView
{
    public partial class FormTopUpWarehouse : Form
    {
        public FormTopUpWarehouse(IComponentLogic componentLogic, IWarehouseLogic warehouseLogic)
        {
            InitializeComponent();
            _warehouseLogic = warehouseLogic;
            List<ComponentViewModel> componentViews = componentLogic.Read(null);
            if (componentViews != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = componentViews;
                comboBoxComponent.SelectedItem = null;
            }
            List<WarehouseViewModel> warehouseViews = warehouseLogic.Read(null);
            if (warehouseViews != null)
            {
                comboBoxWarehouse.DisplayMember = "WarehouseName";
                comboBoxWarehouse.ValueMember = "Id";
                comboBoxWarehouse.DataSource = warehouseViews;
                comboBoxWarehouse.SelectedItem = null;
            }
        }

        private readonly IWarehouseLogic _warehouseLogic;

        public int WarehouseId
        {
            get { return Convert.ToInt32(comboBoxWarehouse.SelectedValue); }

            set { comboBoxWarehouse.SelectedValue = value; }
        }

        public int ComponentId
        {
            get { return Convert.ToInt32(comboBoxComponent.SelectedValue); }

            set { comboBoxComponent.SelectedValue = value; }
        }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }

            set { textBoxCount.Text = value.ToString(); }
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            if (textBoxCount.Text.Length == 0)
            {
                MessageBox.Show("Пустое поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxWarehouse.SelectedValue == null)
            {
                MessageBox.Show("Вы не выбрали склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Вы не выбрали компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int count = Convert.ToInt32(textBoxCount.Text);
                if (count < 1)
                {
                    throw new Exception("Надо пополнять, а не уменьшать");
                }
                _warehouseLogic.AddComponents(new WarehouseTopUpBindingModel
                {
                    WarehouseId = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                    ComponentId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
