using IceCreamShopBusinessLogic.OfficePackage.HelperEnums;
using IceCreamShopBusinessLogic.OfficePackage.HelperModels;
using System.Collections.Generic;

namespace IceCreamShopBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdfFull : AbstractSaveToPdf
    {
        new public void CreateDoc(PdfInfo info)
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
    }
}
