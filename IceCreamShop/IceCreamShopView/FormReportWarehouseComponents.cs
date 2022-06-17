using System;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.ViewModels;

namespace IceCreamShopView
{
    public partial class FormReportWarehouseComponents : Form
    {
        public FormReportWarehouseComponents(IReportLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private readonly IReportLogic _logic;

        private void FormReportProductComponents_Load(object sender, EventArgs e)
        {
            try
            {
                MethodInfo method = _logic.GetType().GetMethod("GetWarehouses");
                List<ReportWarehouseComponentViewModel> dict = (List<ReportWarehouseComponentViewModel>)method.Invoke(_logic, new object[] { });
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Rows.Add(new object[] { elem.WarehouseName, "", ""});
                        foreach (var listElem in elem.Components)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                        dataGridView.Rows.Add(Array.Empty<object>());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveToExcel_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MethodInfo method = _logic.GetType().GetMethod("SaveWarehousesComponentToExcelFile");
                    method.Invoke(_logic, new object[] { new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    } });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
