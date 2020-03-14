using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MergePDFs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string[] umowa = 
            { 
                "E:/dom_pegow_skany/umowa_przedwstepna_01.pdf",
                "E:/dom_pegow_skany/umowa_przedwstepna_02.pdf",
                "E:/dom_pegow_skany/umowa_przedwstepna_03.pdf",
                "E:/dom_pegow_skany/umowa_przedwstepna_04.pdf",
                "E:/dom_pegow_skany/umowa_przedwstepna_05.pdf",
                "E:/dom_pegow_skany/umowa_przedwstepna_06.pdf"
            };
            string output_umowa = "E:/dom_pegow_skany_output/umowa_przedwstepna.pdf";

            string[] aktNotSluzebnosc =
            {
                "E:/dom_pegow_skany/akt_notarialny_sluzebnosc_01.pdf",
                "E:/dom_pegow_skany/akt_notarialny_sluzebnosc_02.pdf",
                "E:/dom_pegow_skany/akt_notarialny_sluzebnosc_03.pdf",
                "E:/dom_pegow_skany/akt_notarialny_sluzebnosc_04.pdf",
                "E:/dom_pegow_skany/akt_notarialny_sluzebnosc_poprawka01.pdf",
            };
            string output_akt_notarialny_sluzebnosc = "E:/dom_pegow_skany_output/akt_notarialny_sluzebnosc.pdf";

            string[] aktNotSprzedaz =
            {
                "E:/dom_pegow_skany/akt_notarialny_sprzedaz_01.pdf",
                "E:/dom_pegow_skany/akt_notarialny_sprzedaz_02.pdf",
                "E:/dom_pegow_skany/akt_notarialny_sprzedaz_03.pdf",
                "E:/dom_pegow_skany/akt_notarialny_sprzedaz_04.pdf",
                "E:/dom_pegow_skany/akt_notarialny_sprzedaz_poprawka01_01.pdf",
                "E:/dom_pegow_skany/akt_notarialny_sprzedaz_poprawka01_02.pdf",
            };
            string output_akt_notarialny_sprzedaz = "E:/dom_pegow_skany_output/akt_notarialny_sprzedaz.pdf";

            string[] wyrok =
            {
                "E:/dom_pegow_skany/mjaworska_wyrok_01.pdf",
                "E:/dom_pegow_skany/mjaworska_wyrok_02.pdf",
            };
            string output_wyrok = "E:/dom_pegow_skany_output/mjaworska_wyrok.pdf";

            string[] swBz =
            {
                "E:/dom_pegow_skany/mjaworska_swiadectwo_bzwbk_01.pdf",
                "E:/dom_pegow_skany/mjaworska_swiadectwo_bzwbk_02.pdf",
            };
            string output_swbz = "E:/dom_pegow_skany_output/mjaworska_swiadectwo_bzwbk.pdf";

            string[] swifrima =
            {
                "E:/dom_pegow_skany/mjaworska_swiadectwo_ifirma_01.pdf",
                "E:/dom_pegow_skany/mjaworska_swiadectwo_ifirma_02.pdf",
            };
            string output_ifirma = "E:/dom_pegow_skany_output/mjaworska_ifirma.pdf";

            Merge(umowa, output_umowa);
            Merge(aktNotSluzebnosc, output_akt_notarialny_sluzebnosc);
            Merge(aktNotSprzedaz ,output_akt_notarialny_sprzedaz);
            Merge(wyrok, output_wyrok);
            Merge(swBz, output_swbz);
            Merge(swifrima, output_ifirma);
        }

        private static void Merge(string[] fileArray, string outputPdfPath)
        {
            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;

            sourceDocument = new Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

            //output file Open  
            sourceDocument.Open();


            //files list wise Loop  
            for (int f = 0; f < fileArray.Length; f++)
            {
                int pages = TotalPageCount(fileArray[f]);

                reader = new PdfReader(fileArray[f]);
                //Add pages in new file  
                for (int i = 1; i <= pages; i++)
                {
                    importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                    pdfCopyProvider.AddPage(importedPage);
                }

                reader.Close();
            }
            //save the output file  
            sourceDocument.Close();
        }

        private static int TotalPageCount(string file)
        {
            using (StreamReader sr = new StreamReader(System.IO.File.OpenRead(file)))
            {
                Regex regex = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matches = regex.Matches(sr.ReadToEnd());

                return matches.Count;
            }
        }

    }
}
