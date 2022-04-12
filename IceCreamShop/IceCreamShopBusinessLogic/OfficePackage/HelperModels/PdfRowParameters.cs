using IceCreamShopBusinessLogic.OfficePackage.HelperEnums;
using System.Collections.Generic;

namespace IceCreamShopBusinessLogic.OfficePackage.HelperModels
{
    public class PdfRowParameters
    {
        public List<string> Texts { get; set; }

        public string Style { get; set; }

        public PdfParagraphAlignmentType ParagraphAlignment { get; set; }
    }
}
