using IceCreamShopBusinessLogic.OfficePackage.HelperEnums;
using IceCreamShopBusinessLogic.OfficePackage.HelperModels;
using System.Collections.Generic;

namespace IceCreamShopBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
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

            foreach (var iceCream in info.IceCreams)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {
                        (iceCream.IceCreamName + ": ", new WordTextProperties { Bold = true, Size = "24" }),
                        (iceCream.Price.ToString(), new WordTextProperties { Bold = false, Size = "24" } )
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }

            SaveWord(info);
        }

        protected abstract void CreateWord(WordInfo info);

        protected abstract void CreateParagraph(WordParagraph paragraph);

        protected abstract void SaveWord(WordInfo info);
    }
}
