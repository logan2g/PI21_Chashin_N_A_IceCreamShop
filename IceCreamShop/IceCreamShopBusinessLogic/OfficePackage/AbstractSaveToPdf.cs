﻿using IceCreamShopBusinessLogic.OfficePackage.HelperEnums;
using IceCreamShopBusinessLogic.OfficePackage.HelperModels;
using System.Collections.Generic;

namespace IceCreamShopBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf(info);

            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }", Style = "Normal"
            });

            CreateTable(new List<string> { "3cm", "6cm", "3cm", "2cm", "3cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата заказа", "Изделие", "Количество", "Сумма", "Статус" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var order in info.Orders)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { order.DateCreate.ToShortDateString(), order.IceCreamName, order.Count.ToString(), order.Sum.ToString(), order.Status.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }

            SavePdf(info);
        }

        public void CreateDocFullPeriod(PdfInfo info)
        {
            CreatePdf(info);

            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph
            {
                Text = $"Заказы за весь период",
                Style = "Normal"
            });

            CreateTable(new List<string> { "5cm", "4cm", "3cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата", "Количество", "Сумма" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var order in info.Orders)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { order.DateCreate.ToShortDateString(), order.Count.ToString(), order.Sum.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }

            SavePdf(info);
        }

        protected abstract void CreatePdf(PdfInfo info);

        protected abstract void CreateParagraph(PdfParagraph paragraph);

        protected abstract void CreateTable(List<string> columns);

        protected abstract void CreateRow(PdfRowParameters rowParameters);

        protected abstract void SavePdf(PdfInfo info);
    }
}
