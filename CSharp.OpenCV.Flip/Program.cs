using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace CSharp.OpenCV.Flip
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("==================================================");
                Console.WriteLine("=                    USAGE                       =");
                Console.WriteLine("==================================================");
                Console.WriteLine("1st argument : Image Source Path");
                Console.WriteLine("2nd argument : Image Target Path");
                Console.WriteLine("3rd argument : Image Flipping-axis, X, Y or XY");
                Console.WriteLine("4th argument : Image Prefix\n");

                Console.WriteLine("ERROR : Number of arguments must be 4.");
                return;
            }

            string sourcePath   = args[0];
            string targetPath   = args[1];
            string flipMode     = args[2];
            string Prefix       = args[3];

            // load images from image path
            string[] fileEntries = Directory.GetFiles(sourcePath);

            string strImageFilename = "";
            string origFilename = "";
            string targetFullPath = "";
            for (int k = 0; k < fileEntries.Length; k++)
            {
                strImageFilename = fileEntries[k];

                string[] words = strImageFilename.Split('\\');
                origFilename = words[words.Length - 1];

                using (var src = new Mat(strImageFilename, ImreadModes.Unchanged))
                {
                    var flipped = new Mat(strImageFilename, ImreadModes.Unchanged);

                    switch (flipMode)
                    {
                        case "X": // flipping image along X-axis
                            flipped = src.Flip(FlipMode.X);
                            break;

                        case "Y": // flipping image along Y-axis
                            flipped = src.Flip(FlipMode.Y);
                            break;

                        case "XY": // flipping image along both axes
                            flipped = src.Flip(FlipMode.XY);
                            break;

                        default:
                            Console.WriteLine("No flipping axis defined.");
                            break;
                    }

                    if(Prefix.Length > 0)
                    {
                        targetFullPath = Path.Combine(targetPath, "" + Prefix + "-" + origFilename);
                    }
                    else
                    {
                        targetFullPath = Path.Combine(targetPath, "" + origFilename);
                    }

                    Cv2.ImWrite(targetFullPath, flipped);
                }

                Console.WriteLine("Processed : {0:f2}%", 100.0f * (k + 1) / fileEntries.Length);
            }

        }
    }
}