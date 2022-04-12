using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.ViewModels;
using Microsoft.Reporting.WinForms;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

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
                List<ReportOrderViewModel> dataSource = null;
                if (_fullPeriod)
                {
                    dataSource = _logic.GetOrdersFull();
                }
                else
                {
                    dataSource = _logic.GetOrders(new ReportBindingModel
                    {
                        DateFrom = dateTimePickerFrom.Value,
                        DateTo = dateTimePickerTo.Value
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
                    if (_fullPeriod)
                    {
                        _logic.SaveOrdersFullToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
                    }
                    else
                    {
                        _logic.SaveOrdersByDateToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = dateTimePickerFrom.Value,
                            DateTo = dateTimePickerTo.Value
                        });
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
