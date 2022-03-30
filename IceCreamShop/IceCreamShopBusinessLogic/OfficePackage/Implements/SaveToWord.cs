using IceCreamShopBusinessLogic.OfficePackage.HelperEnums;
using IceCreamShopBusinessLogic.OfficePackage.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace IceCreamShopBusinessLogic.OfficePackage.Implements
{
    public class SaveToWord : AbstractSaveToWordWarehouses
    {
        private WordprocessingDocument _wordDocument;

        private Body _docBody;

        private Table _tableWarehouses;

        private static JustificationValues GetJustificationValues(WordJustificationType type)
        {
            return type switch
            {
                WordJustificationType.Both => JustificationValues.Both,
                WordJustificationType.Center => JustificationValues.Center,
                _ => JustificationValues.Left,
            };
        }

        private static SectionProperties CreateSectionProperties()
        {
            var properties = new SectionProperties();
            var pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }

        private static ParagraphProperties CreateParagraphProperties(WordTextProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                var properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = GetJustificationValues(paragraphProperties.JustificationType)
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                var paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize 
                    {
                        Val = paragraphProperties.Size
                    });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }

        private void InitTable()
        {
            var tableProp = new TableProperties();
            tableProp.AppendChild(new TableLayout { Type = TableLayoutValues.Fixed });
            tableProp.AppendChild(new TableBorders(
                new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
                ));
            tableProp.AppendChild(new TableWidth { Type = TableWidthUnitValues.Auto });
            _tableWarehouses.AppendChild(tableProp);
            TableGrid tableGrid = new TableGrid();
            for (int j = 0; j < 3; j++)
            {
                tableGrid.AppendChild(new GridColumn() { Width = "3200" });
            }
            _tableWarehouses.AppendChild(tableGrid);
        }

        protected override void CreateWord(WordInfo info)
        {
            _wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document);
            MainDocumentPart mainPart = _wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            _docBody = mainPart.Document.AppendChild(new Body());
            if(info.Warehouses != null)
            {
                _tableWarehouses = new Table();
                InitTable();
            }
        }

        protected override void AddRowInWarehouses(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                TableRow warehouseInfo = new TableRow();
                foreach (var run in paragraph.Texts)
                {
                    var docParagraph = new Paragraph();
                    docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                    var docRun = new Run();
                    var properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Item1,
                        Space = SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                    TableCell cell = new TableCell();
                    cell.AppendChild(docParagraph);
                    warehouseInfo.AppendChild(cell);
                }
                _tableWarehouses.AppendChild(warehouseInfo);
            }
        }

        protected override void CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                var docParagraph = new Paragraph();
                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    var docRun = new Run();
                    var properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Item1,
                        Space = SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                _docBody.AppendChild(docParagraph);
            }
        }

        protected override void SaveWord(WordInfo info)
        {
            _docBody.AppendChild(CreateSectionProperties());
            if (info.Warehouses != null)
            {
                _docBody.AppendChild(_tableWarehouses);
            }
            _wordDocument.MainDocumentPart.Document.Save();
            _wordDocument.Close();
        }
    }
}
