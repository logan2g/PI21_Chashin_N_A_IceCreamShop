using IceCreamShopBusinessLogic.OfficePackage;
using IceCreamShopBusinessLogic.OfficePackage.HelperModels;
using IceCreamShopContracts.BindingModels;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopContracts.StoragesContracts;
using IceCreamShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IceCreamShopBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;

        private readonly IIceCreamStorage _iceCreamStorage;

        private readonly IOrderStorage _orderStorage;

        private readonly AbstractSaveToExcel _saveToExcel;

        private readonly AbstractSaveToWord _saveToWord;

        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportLogic(IIceCreamStorage iceCreamStorage, IComponentStorage componentStorage, IOrderStorage orderStorage,
            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _iceCreamStorage = iceCreamStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;

            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        } 
 
        public List<ReportIceCreamComponentViewModel> GetIceCreams()
        {
            var components = _componentStorage.GetFullList();
            var iceCreams = _iceCreamStorage.GetFullList();
            var list = new List<ReportIceCreamComponentViewModel>();
            foreach (var iceCream in iceCreams)
            {
                var record = new ReportIceCreamComponentViewModel
                {
                    IceCreamName = iceCream.IceCreamName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in components)
                {
                    if (iceCream.IceCreamComponents.ContainsKey(component.Id))
                    {
                        record.Components.Add(new Tuple<string, int>(component.ComponentName, iceCream.IceCreamComponents[component.Id].Item2));
                        record.TotalCount += iceCream.IceCreamComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportOrderViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).Select(x => new ReportOrderViewModel
            {
                DateCreate = x.DateCreate,
                IceCreamName = x.IceCreamName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status.ToString()
            }).ToList();
        }

        public void SaveIceCreamsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список мороженых",
                IceCreams = _iceCreamStorage.GetFullList()
            });
        }

        public void SaveIceCreamComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент по изделиям",
                IceCreams = GetIceCreams()
            });
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

    }
}
