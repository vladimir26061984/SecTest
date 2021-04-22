using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using BorderStyle = MigraDoc.DocumentObjectModel.BorderStyle;
using TabAlignment = MigraDoc.DocumentObjectModel.TabAlignment;

namespace SecurityTest
{
    public class reportGenerator
    {
        private Document document;
        private TextFrame addressFrame;
        private TextFrame resultFrame;
        private Table table;
        private Color TableBorder = Colors.Black;
        private Color TableBlue = Colors.White; //Colors.LightBlue;
        private Color TableGray = Colors.White; //Colors.LightGray;

        public reportGenerator(string pathName, List<string> userParam, List<Question> list, string Text)
        {
            //Create a new MigraDoc document
            document = new Document();
            document.Info.Title = "A sample invoice";
            document.Info.Subject = "Demonstrates how to create an invoice.";
            document.Info.Author = "Stefan Lange";

            DefineStyles();

            CreatePage(userParam[0]);

            FillContent(userParam, list);
            //return this.document;
            //CreateFile(pathName);
            //document.Document.SA

          
            const bool unicode = true;

            const PdfFontEmbedding embedding = PdfFontEmbedding.Always;

            // ========================================================================================

            
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode, embedding);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(pathName);
            // ...and start a viewer.
            System.Diagnostics.Process.Start(pathName);
        }

        public static void Call(string pathName, List<string> userParam, List<Question> list, string Text)
        {
            //CreateFile(pathName);

        }


        void DefineStyles()
        {
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Arial Unicode MS";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Arial Unicode MS";
            //style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }

        void CreatePage(string FIO)
        {
            // Each MigraDoc document needs at least one section.
            Section section = this.document.AddSection();

            // Put a logo in the header
            //Image image = section.Headers.Primary.AddImage(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "result", "icon.bmp")); //("../../PowerBooks.png");
            Image image = section.AddImage(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "result", "icon.bmp")); //("../../PowerBooks.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Проверяемый ____________________ " + FIO);
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Font.Name = "Arial Unicode MS";
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address
            this.addressFrame = section.AddTextFrame();
            this.addressFrame.Height = "3.0cm";
            this.addressFrame.Width = "14.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.Top = "3.0cm";
            this.addressFrame.RelativeVertical = RelativeVertical.Page;

            // Put sender in address frame
            paragraph = this.addressFrame.AddParagraph("Индивидуальный оценочный лист");
            paragraph.Format.Font.Name = "Arial Unicode MS";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            this.resultFrame = section.AddTextFrame();
            this.resultFrame.Height = "3.0cm";
            this.resultFrame.Width = "14.0cm";
            this.resultFrame.Left = ShapePosition.Left;
            this.resultFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.resultFrame.Top = "3.0cm";
            this.resultFrame.RelativeVertical = RelativeVertical.Page;

            

            //Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "4cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Таблица с результатами тестирования", TextFormat.Bold);
            //paragraph.AddTab();
            //paragraph.AddText("Cologne, ");
            //paragraph.AddDateField("dd.MM.yyyy");

            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            Column column = this.table.AddColumn("10cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = this.table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Format.Font.Name = "Arial Unicode MS";
            row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Вопрос");
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            //row.Cells[0].MergeDown = 1;
            row.Cells[1].AddParagraph("Ответ");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[1].MergeRight = 3;
            row.Cells[2].AddParagraph("Верный ответ");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].AddParagraph("Результат");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            //row.Cells[5].AddParagraph("Extended Price");
            //row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
            //row.Cells[5].MergeDown = 1;

            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            //row.Cells[1].AddParagraph("Quantity");
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[2].AddParagraph("Unit Price");
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[3].AddParagraph("Discount (%)");
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[4].AddParagraph("Taxable");
            //row.Cells[4].Format.Alignment = ParagraphAlignment.Left;

            this.table.SetEdge(0, 0, 4, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }

        void FillContent(List<string> userParam, List<Question> list)
        {
            //List<string> LIST = new List<string>();
            //LIST.Add("Традиционная, рыночная, командная, смешанная экономика. Традиционная экономика основана на традициях, передающихся из поколения в поколение.Эти традиции определяют, какие товары и услуги производить, для кого и каким образом.Перечень благ, технология производства и распределение базируются на обычаях данной страны.Экономические роли членов общества определяются наследственностью и кастовой принадлежностью.");
            //LIST.Add("Рыночная экономика характеризуется частной собственностью на ресурсы и использованием системы рынков и цен для координации экономической деятельности и управления ею. Что, как и для кого производить, определяют рынок, цены, прибыль и убытки хозяйствующих субъектов. Производитель стремится выпускать ту продукцию, которая удовлетворяет потребности покупателя и приносит ему наибольшую прибыль. Потребитель сам решает, какой товар ему купить и сколько за него заплатить денег.");
            //LIST.Add("Командная основана на государственной собственности на все материальные ресурсы. Все экономические решения принимаются государственными органами, осуществляющими централизованное (директивное) планирование. Каждому предприятию производственный план предписывает, что, в каком объеме производить; выделяются определенные ресурсы, техника, рабочая сила, материалы и др., что определяет решение вопроса, как производить; указываются не только поставщики, но и покупатели, т. е. для кого производить.");
            //LIST.Add("Традиционная, рыночная, командная, смешанная экономика. Традиционная экономика основана на традициях, передающихся из поколения в поколение.Эти традиции определяют, какие товары и услуги производить, для кого и каким образом.Перечень благ, технология производства и распределение базируются на обычаях данной страны.Экономические роли членов общества определяются наследственностью и кастовой принадлежностью.");
            //LIST.Add("Рыночная экономика характеризуется частной собственностью на ресурсы и использованием системы рынков и цен для координации экономической деятельности и управления ею. Что, как и для кого производить, определяют рынок, цены, прибыль и убытки хозяйствующих субъектов. Производитель стремится выпускать ту продукцию, которая удовлетворяет потребности покупателя и приносит ему наибольшую прибыль. Потребитель сам решает, какой товар ему купить и сколько за него заплатить денег.");
            //LIST.Add("Командная основана на государственной собственности на все материальные ресурсы. Все экономические решения принимаются государственными органами, осуществляющими централизованное (директивное) планирование. Каждому предприятию производственный план предписывает, что, в каком объеме производить; выделяются определенные ресурсы, техника, рабочая сила, материалы и др., что определяет решение вопроса, как производить; указываются не только поставщики, но и покупатели, т. е. для кого производить.");
            //LIST.Add("Традиционная, рыночная, командная, смешанная экономика. Традиционная экономика основана на традициях, передающихся из поколения в поколение.Эти традиции определяют, какие товары и услуги производить, для кого и каким образом.Перечень благ, технология производства и распределение базируются на обычаях данной страны.Экономические роли членов общества определяются наследственностью и кастовой принадлежностью.");
            //LIST.Add("Рыночная экономика характеризуется частной собственностью на ресурсы и использованием системы рынков и цен для координации экономической деятельности и управления ею. Что, как и для кого производить, определяют рынок, цены, прибыль и убытки хозяйствующих субъектов. Производитель стремится выпускать ту продукцию, которая удовлетворяет потребности покупателя и приносит ему наибольшую прибыль. Потребитель сам решает, какой товар ему купить и сколько за него заплатить денег.");
            //LIST.Add("Командная основана на государственной собственности на все материальные ресурсы. Все экономические решения принимаются государственными органами, осуществляющими централизованное (директивное) планирование. Каждому предприятию производственный план предписывает, что, в каком объеме производить; выделяются определенные ресурсы, техника, рабочая сила, материалы и др., что определяет решение вопроса, как производить; указываются не только поставщики, но и покупатели, т. е. для кого производить.");
            // Fill address in address text frame

            Paragraph paragraph = this.addressFrame.AddParagraph();
            paragraph.AddText("Проверяемый: " + userParam[0].Replace(Environment.NewLine, ""));
            paragraph.AddLineBreak();
            paragraph.AddText("Подразделение: " + userParam[3].Replace(Environment.NewLine, ""));
            paragraph.AddLineBreak();
            paragraph.AddText("Должность: " + userParam[2].Replace(Environment.NewLine, ""));
            paragraph.AddLineBreak();
            paragraph.AddText("Тема проверки знаний: " + userParam[4]);
            paragraph.AddLineBreak();
            paragraph.AddText("Дата/время проведения: " + userParam[1]);

            // Iterate the invoice items
            double totalExtendedPrice = 0;
            
            for (int i = 0; i < list.Count; i++)
            {
                // Each item fills two rows
                Row row1 = this.table.AddRow();
                //Row row2 = this.table.AddRow();
                row1.TopPadding = 1.5;
                row1.Cells[0].Shading.Color = TableGray;
                row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                //row1.Cells[0].MergeDown = 1;
                row1.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                row1.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[2].Format.Alignment = ParagraphAlignment.Center;
                row1.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[3].Format.Alignment = ParagraphAlignment.Center;
                row1.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                //row1.Cells[1].MergeRight = 3;
                //row1.Cells[5].Shading.Color = TableGray;
                //row1.Cells[5].MergeDown = 1;

                string paragText = list[i].Text;
                foreach (Answers SSS in list[i].Answer)
                    paragText += Environment.NewLine + SSS.Text;
                row1.Cells[0].AddParagraph(paragText);
                row1.Cells[1].AddParagraph(list[i].UserAnswer.ToString());
                row1.Cells[2].AddParagraph(list[i].ValidAnswer.ToString());
                if (list[i].isOk)
                {
                    row1.Cells[3].AddParagraph("Верно");
                    row1.Cells[3].Format.Font.Bold = false;
                    row1.Cells[3].Format.Font.Color = Colors.DarkSlateGray;
                }
                else
                {
                    row1.Cells[3].AddParagraph("Неверно");
                    row1.Cells[3].Format.Font.Bold = true;

                }

                this.table.SetEdge(0, this.table.Rows.Count - 1, 4, 1, Edge.Box, BorderStyle.Single, 0.75);
            }

            // Add an invisible row as a space line to the table
            //Row row = this.table.AddRow();
            //row.Borders.Visible = false;

            //// Add the total price row
            //row = this.table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("Total Price");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            ////row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");

            //// Add the VAT row
            //row = this.table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("VAT (19%)");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            ////row.Cells[5].AddParagraph((0.19 * totalExtendedPrice).ToString("0.00") + " €");

            //// Add the additional fee row
            //row = this.table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("Shipping and Handling");
            ////row.Cells[5].AddParagraph(0.ToString("0.00") + " €");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;

            //// Add the total due row
            //row = this.table.AddRow();
            //row.Cells[0].AddParagraph("Total Due");
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            //totalExtendedPrice += 0.19 * totalExtendedPrice;
            ////row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");

            //// Set the borders of the specified cell range
            //this.table.SetEdge(5, this.table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);




            // Add the notes paragraph
            paragraph = this.document.LastSection.AddParagraph();
            paragraph.Format.SpaceBefore = "1cm";
            paragraph.Format.Borders.Width = 0.75;
            paragraph.Format.Borders.Distance = 3;
            paragraph.Format.Borders.Color = TableBorder;
            paragraph.AddText("Время на ответы:");
            paragraph.AddLineBreak();
            paragraph.AddText("установленное 15:00");
            paragraph.AddLineBreak();
            //paragraph.AddText("затраченное   " + userParam[5]);
            paragraph.AddText("затраченное   ");
            paragraph.AddText("   ");
            paragraph.AddText(userParam[5]);
            
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddText("Количество ответов:");
            paragraph.AddLineBreak();
            paragraph.AddText("правильных   " + userParam[6]);
            paragraph.AddLineBreak();
            paragraph.AddText("неправильных " + userParam[7]);
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();

            int crit = Form1.CriteriaList[0];
            if (int.Parse(userParam[6]) >= crit)
                paragraph.AddText("Результат проверки: сдал");
            else
                paragraph.AddText("Результат проверки: не сдал");
            paragraph.AddLineBreak();
            //paragraph.AddText("Дата/время проведения: " + userParam[1]);
            //paragraph.Format.Shading.Color = TableGray;

        }

        
    }
}
