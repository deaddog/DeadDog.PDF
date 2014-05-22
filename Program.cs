using System;
using System.Collections.Generic;
using System.Drawing;

namespace DeadDog.PDF
{
    static class Program
    {
        private static FontInfo font;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Loading fonts...");
            Font f = new Font("Calibri", 12f);
            font = new FontInfo(f);
            f.Dispose();
            Console.WriteLine("Fonts loaded...");
            Console.WriteLine("");

            string st = "";
            for (int i = 0; i < 10; i++)
            {
                if (i + 1 == 5)
                {
                }
                st += "Jeg hedder Mikkel. ";
                string[] text = split(st, 5);
                string line = (i + 1).ToString("00") + " Length: " + st.Length + " ->";
                foreach (string s in text)
                    line += " " + s.Length.ToString("00");
                Console.WriteLine(line);
                Console.ReadKey(true);
            }

            PDFDocument pdf = new PDFDocument();
            TextLine t = new TextLine("Jeg hedder Mikkel.", font, new PointF(3, 3),
                TextAlignment.Left, Color.DarkBlue);

            RectangleF rect = new RectangleF(4, 7, t.Width, t.Height);

            t.Location = rect.Location;
            Line l = new Line(new RectangleF(0, 0, rect.X, rect.Y), Color.Black);
            Box b = new Box(rect, true); b.BorderWidth = 0.1f;
            b.HasFill = false;
            Page p = new Page(PageSize.A4);
            p.Objects.Add(b); p.Objects.Add(l); p.Objects.Add(t); 
            pdf.Pages.Add(p);
            pdf.Create("C:\\new.pdf");
            ShowPDF("C:\\new.pdf");
        }

        private static string[] split(string s, float maxWidth)
        {
            List<string> list = new List<string>();
            list.Add(s);
            int index = 0;
            while (index < list.Count)
            {
                int i = check(list[index], 0, list[index].Length, maxWidth);

                string tempS = list[index].Substring(0, i);
                if (list[index].Length > i && i > 0)
                {// Might "-" come before " "? Gain both indexes and examine.
                    if (tempS.Contains(" "))
                    {
                        tempS = tempS.Substring(0, tempS.LastIndexOf(' '));
                        list[index] = list[index].Substring(tempS.Length + 1);
                    }
                    else if (tempS.Contains("-"))
                    {
                        tempS = tempS.Substring(0, tempS.LastIndexOf('-') + 1);
                        list[index] = list[index].Substring(tempS.Length);
                    }
                    else
                    {
                        list[index] = list[index].Substring(tempS.Length);
                    }
                    list.Insert(index, tempS);
                }

                index++;
            }
            return list.ToArray();
        }
        private static int check(string s, int offset, int length, float maxWidth)
        {
            if (length == 1)
            {
                if (validlength(s, offset + length, maxWidth))
                    return offset + 1;
                else
                    return offset;
            }

            int a = (length + 1) / 2;
            if (validlength(s, offset + a, maxWidth))
                return check(s, offset + a, length - a, maxWidth);
            else
                return check(s, offset, a, maxWidth);
        }
        private static bool validlength(string s, int length, float maxWidth)
        {
            float f = font.MeasureStringWidth(s.Substring(0, length).Trim());
            return f <= maxWidth;
        }
        private static void ShowPDF(string filename)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = filename;
            //proc.StartInfo.Arguments = ss.Arguments;
            //proc.StartInfo.WindowStyle = ss.WindowStyle;
            //proc.StartInfo.WorkingDirectory = ss.WorkingDirectory;
            proc.Start();
        }
    }
}