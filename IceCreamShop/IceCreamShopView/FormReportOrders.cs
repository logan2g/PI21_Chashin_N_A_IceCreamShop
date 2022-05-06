using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.ViewModels;
using Microsoft.Reporting.WinForms;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;

namespace IceCreamShopView
{
    public partial class FormReportOrders : Form
    {
        private readonly ReportViewer reportViewer;

        private readonly IReportLogic _logic;

        private readonly bool _fullPeriod;

        public FormReportOrders(IReportLogic logic, bool fullPeriod)
        {
            InitializeComponent();
            if (fullPeriod)
            {
                label1.Visible = false;
                label2.Visible = false;
                dateTimePickerFrom.Visible = false;
                dateTimePickerTo.Visible = false;
            }
            _logic = logic;
            _fullPeriod = fullPeriod;
            reportViewer = new ReportViewer
            {
                Dock = DockStyle.Fill
            };
            if (fullPeriod)
            {
                reportViewer.LocalReport.LoadReportDefinition(new FileStream("ReportOrderFull.rdlc", FileMode.Open));
            }
            else
            {
                reportViewer.LocalReport.LoadReportDefinition(new FileStream("ReportOrderPeriod.rdlc", FileMode.Open));
            }
            Controls.Clear();
            Controls.Add(panel);
            Controls.Add(reportViewer);
        }

        private void BtnMake_Click(object sender, EventArgs e)
        {
            if (!_fullPeriod && dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                MethodInfo method = null;
                List<ReportOrderViewModel> dataSource = null;
                if (_fullPeriod)
                {
                    method = _logic.GetType().GetMethod("GetOrdersFull");
                    dataSource = (List<ReportOrderViewModel>)method.Invoke(_logic, new object[] { });
                }
                else
                {
                    method = _logic.GetType().GetMethod("GetOrders");
                    dataSource = (List<ReportOrderViewModel>)method.Invoke(_logic, new object[]
                    {new ReportBindingModel
                    {
                        DateFrom = dateTimePickerFrom.Value,
                        DateTo = dateTimePickerTo.Value
                    }
                    });
                }
                var source = new ReportDataSource("DataSetOrders", dataSource);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                if (!_fullPeriod)
                {
                    var parameters = new[] { new ReportParameter("ReportParameterPeriod", "c " + dateTimePickerFrom.Value.ToShortDateString() + " по " + dateTimePickerTo.Value.ToShortDateString()) };
                    reportViewer.LocalReport.SetParameters(parameters);
                }
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnToPDF_Click(object sender, EventArgs e)
        {
            if (!_fullPeriod && dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MethodInfo method = null;
                    if (_fullPeriod)
                    {
                        method = _logic.GetType().GetMethod("SaveOrdersFullToPdfFile");
                        method.Invoke(_logic, new object[] { new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        } });
                    }
                    else
                    {
                        method = _logic.GetType().GetMethod("SaveOrdersByDateToPdfFile");
                        method.Invoke(_logic, new object[] { new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = dateTimePickerFrom.Value,
                            DateTo = dateTimePickerTo.Value
                        } });
                    }
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
