using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace KontrolViewFileInfo
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            args = new string[1];
            args[0] = @"C:\Users\imbod\AppData\Local\Discord\app-1.0.9012\Discord.exe";
#endif
            #region PARAMETER CHECK
            if (args.Length > 1)
            {
                throw new Exception("Too many arguments\r\nIt is exactly 1 argument expected");
            }

            if (args.Length == 0)
            {
                throw new Exception("No argument given");
            }
            #endregion

            string arg = args[0];
            var img = Icon.ExtractAssociatedIcon(arg);
            var fileInfo = FileVersionInfo.GetVersionInfo(arg);
            var cp = new ControlGroup
            {
                name = fileInfo.ProductName,
                icon = "data:image/png;base64," + ConvertImageToBase64String(img)
            };
            Console.WriteLine(cp.ToString());
        }

        /// <summary>
        /// Converts Icon to png to base64 string representation
        /// </summary>
        /// <param name="image"></param>
        /// <returns><see cref="Convert.ToBase64String"/></returns>
        static string ConvertImageToBase64String(Icon image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap bm = image.ToBitmap();
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}

/// <summary>
/// Small Class to improve readability 
/// </summary>
class ControlGroup
{
    public string name { get; set; }
    public string icon { get; set; }

    public override string ToString()
    {
        return $"{{\"name\": \"{this.name}\",\"icon\": \"{this.icon}\"}}";
    }
}