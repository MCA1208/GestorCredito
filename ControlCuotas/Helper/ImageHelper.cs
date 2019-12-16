using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace ControlSheet.Helper
{
    public class ImageHelper
    {
        public static string ImageFileToB64(string file)
        {
            string imgB64 = string.Empty;
            byte[] imgBytes = null;

            using (StreamReader sr = new StreamReader(file))
            {
                using (Image image = Image.FromStream(sr.BaseStream))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Jpeg);

                        imgBytes = ms.ToArray();

                        ms.Flush();
                        ms.Close();

                        imgB64 = Convert.ToBase64String(imgBytes);

                        sr.Close();
                        sr.Dispose();
                        image.Dispose();
                    }
                }
            }

            return imgB64;
        }
    }
}