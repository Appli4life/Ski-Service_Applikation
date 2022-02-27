using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ski_Service_Applikation.Core
{
    public static class PDF
    {
        public static void Generate_Bill(miete m)
        {
            // PDF erstellen
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Rechnung_" + DateTime.Now.ToString("f");

            // New Page
            PdfPage page = pdf.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            XFont font2 = new XFont("Verdana", 15, XFontStyle.BoldItalic);
            XFont font3 = new XFont("Verdana", 12, XFontStyle.Regular);
            XFont font4 = new XFont("Verdana", 12, XFontStyle.Underline);

            string tage = m.Rueckgabe_Datum.Subtract(m.Miet_Datum).ToString("dd");
            double preis_pro_stück = Convert.ToInt32(tage) * m.angebot.Preis_pro_Tag * m.altersgruppe.Preismultiplikator;
            double Gesamtpreis = m.Menge * preis_pro_stück;

            gfx.DrawString("Jet-Stream Service", font, XBrushes.Black, new XPoint(page.Width / 3, 30));
            gfx.DrawString("Rechung vom: " + DateTime.Now.ToString("d") + " " + DateTime.Now.ToString("HH:mm"), font2, XBrushes.DarkBlue, new XPoint(page.Width / 3.5, 60));

            gfx.DrawString("Bestellung: ", font2, XBrushes.DarkBlue, new XPoint(10 / 2, 90));
            gfx.DrawString("Marke: " + m.angebot.marke.Marke1 + " Kategorie: " + m.angebot.kategorie.Kategorie1, font3, XBrushes.Blue, new XPoint(10, 120));

            gfx.DrawString("Preis pro Stück: " + preis_pro_stück + " CHF (" + m.angebot.Preis_pro_Tag * m.altersgruppe.Preismultiplikator + " CHF (Preis pro Tag für " + m.altersgruppe.Altersgruppe1 + ") * " + tage + " Tage)", font3, XBrushes.Blue, new XPoint(10, 140));
            gfx.DrawString("Gesamtpreis : " + Gesamtpreis + " CHF (Menge: " + m.Menge + " * Preis pro Stück: " + preis_pro_stück + " CHF)", font4, XBrushes.Blue, new XPoint(10, 160));

            gfx.DrawString("Gemietet von: " + m.Miet_Datum.ToString("d") + " Bis: " + m.Rueckgabe_Datum.ToString("d"), font3, XBrushes.Blue, new XPoint(10, 180));
            gfx.DrawString("Körpergrösse: " + m.Koerpergroesse + " cm Geschlecht: " + m.geschlecht.Geschlecht1, font3, XBrushes.Blue, new XPoint(10, 200));

            gfx.DrawString("Danke für Ihre Bestellung", font2, XBrushes.DarkBlue, new XPoint(page.Width / 3.5, 240));

            string filename = "C:\\Users\\" + Environment.UserName + "\\Downloads\\Jet-Stream_Rechnung_vom_" + DateTime.Now.ToString("d") + "_" + DateTime.Now.ToString("HH.mm") + ".pdf";
            pdf.Save(filename);

        }
    }
}