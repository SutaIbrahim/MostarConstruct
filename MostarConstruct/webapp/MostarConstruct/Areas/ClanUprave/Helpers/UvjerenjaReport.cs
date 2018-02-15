using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using MostarConstruct.Data;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.Helpers
{
    public class UvjerenjaReport
    {
        private DatabaseContext _db;
        public UvjerenjaReport(DatabaseContext db)
        {
            _db = db;
        }
        int _totalColumns = 5;
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable = new PdfPTable(5);
        PdfPCell _pdfPCell;
        MemoryStream _memoryStream = new MemoryStream();
        Uvjerenje _uvjerenje = new Uvjerenje();

        public byte[] PrepareReport(Uvjerenje uvjerenje)
        {
            _uvjerenje = uvjerenje;

            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            _pdfTable.SetWidths(new float[] { 60f, 150f, 100f,100f,100f });
            

            this.ReportHeader();
            this.ReportBody();
            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);

            _document.Close();
            return _memoryStream.ToArray();
        }
        private void ReportHeader()
        {
            Image slika = Image.GetInstance("https://image.ibb.co/cSeymS/logo.png");
            _document.Add(slika);

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Tel/Fax: 036-001-100", _fontStyle));
            _pdfPCell.Colspan = _totalColumns;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Ziraat Bank: 01920192041892", _fontStyle));
            _pdfPCell.Colspan = _totalColumns;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("PDV broj: 989821", _fontStyle));
            _pdfPCell.Colspan = _totalColumns;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("E-mail: uprava@mostar-construct.ba", _fontStyle));
            _pdfPCell.Colspan = _totalColumns;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Adresa: Bisce Polje b.b", _fontStyle));
            _pdfPCell.Colspan = _totalColumns;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();

           


           

            _fontStyle = FontFactory.GetFont("Tahoma", 15f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Uvjerenje", _fontStyle));
            _pdfPCell.Colspan = _totalColumns;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.MinimumHeight = 60f;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();


            _document.AddAuthor("MostarConstruct");
            _document.AddTitle("Uvjerenje");
            
        }
        private void ReportBody()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);

            _pdfPCell = new PdfPCell(new Phrase("Broj protokola", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;

            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Datum izdavanja", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Za radnika", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Svrha", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Napomena", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();


            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
            int serialNumber = 1;

            _pdfPCell = new PdfPCell(new Phrase(_uvjerenje.BrojProtokola.ToString(), _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_uvjerenje.DatumIzdavanja.ToShortDateString(), _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_db.Radnici.Include(x => x.Osoba).Where(x => x.RadnikID == _uvjerenje.RadnikID).FirstOrDefault().Osoba.Ime + " " + _db.Radnici.Include(x => x.Osoba).Where(x => x.RadnikID == _uvjerenje.RadnikID).FirstOrDefault().Osoba.Prezime, _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_uvjerenje.Svrha.ToString(), _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_uvjerenje.Napomena.ToString(), _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfTable.AddCell(_pdfPCell);

            _pdfTable.CompleteRow();


            


            _pdfPCell = new PdfPCell(new Phrase("Potpis narucioca uvjerenja                                                                                                                                     Potpis i pecat izdavatelja uvjerenja\n\n\n-----------------------------------------                                                                                                                           ---------------------------------------------------", _fontStyle));
            _pdfPCell.Colspan = _totalColumns;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.PaddingTop = 550f;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfTable.AddCell(_pdfPCell);
            _pdfTable.CompleteRow();
            
            


        }
       
    }
}
