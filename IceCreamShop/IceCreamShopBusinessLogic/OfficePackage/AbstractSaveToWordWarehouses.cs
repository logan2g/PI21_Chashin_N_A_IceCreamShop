using IceCreamShopBusinessLogic.OfficePackage.HelperEnums;
using IceCreamShopBusinessLogic.OfficePackage.HelperModels;
using System.Collections.Generic;

namespace IceCreamShopBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWordWarehouses : AbstractSaveToWord
    {
        new public void CreateDoc(WordInfo info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            foreach (var warehouse in info.Warehouses)
            {
                AddRowInWarehouses(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)>
                    {
                        (warehouse.WarehouseName, new WordTextProperties { Bold = false, Size = "20" }),
                        (warehouse.ResponsiblePerson, new WordTextProperties { Bold = false, Size = "20" }),
                        (warehouse.CreateDate.ToShortDateString(), new WordTextProperties { Bold = false, Size = "20" })
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "20",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }

            SaveWord(info);
        }

        protected abstract void AddRowInWarehouses(WordParagraph paragraph);
    }
}
