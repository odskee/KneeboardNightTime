// Author: Odskee



using System.Drawing;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Setup
        string CWD = AppDomain.CurrentDomain.BaseDirectory;
        string currentImageToManipulate = "";
        string[] fileInDir = Directory.GetFiles(CWD);

        // Set Alpha & Name
        int alpha = SetAlphaFromUser();
        string darkConversionName = SetDarkFileName();


        // Output
        Console.WriteLine("Looking for images in: {0}", CWD);


        // Iterate through each file in current directory
        foreach (string file in fileInDir.Where(a => a.ToLower().EndsWith(".jpeg") || a.ToLower().EndsWith(".jpg") || a.ToLower().EndsWith(".png")))
        {

            if (!string.IsNullOrEmpty(file))
            {
                string fileName = file.Split("\\").Last().Split(".").First();
                Console.WriteLine("Processing Image: {0}", fileName);
                Bitmap bmp = new Bitmap(file);
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (Brush cloud_brush = new SolidBrush(Color.FromArgb(alpha, Color.Black)))
                    {
                        g.FillRectangle(cloud_brush, rect);
                    }
                }

                // Save out results
                string outFile = file.Replace(fileName, fileName + "_" + darkConversionName);
                Console.WriteLine("Saving converted file as {0}", outFile);
                bmp.Save(outFile);
            }
        }


        Console.WriteLine("All images have been processed, you can now close this program!");
        _ = Console.ReadLine();
    }

    static int SetAlphaFromUser()
    {
        string userAlpha;
        int Alpha = 0;
        bool alphaSet = false;

        while (alphaSet == false)
        {
            Console.WriteLine("What alpha do you want?");
            userAlpha = Console.ReadLine();

            try
            {
                if (!string.IsNullOrEmpty(userAlpha))
                {
                    Alpha = Int32.Parse(userAlpha);
                    if (Alpha >= 0 && Alpha <= 255)
                    {
                        alphaSet = true;
                    }
                    else
                    {
                        throw new Exception("Alpha out of range!");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Alpha is not valid!");
            }
        }

        return Alpha;
    }


    static string SetDarkFileName()
    {
        string darkFileName = "";
        Console.WriteLine("What name would you like appended?  Default is \"Dark\" i.e MyImage_Dark.png");
        darkFileName = Console.ReadLine();

        if (string.IsNullOrEmpty(darkFileName))
        {
            darkFileName = "Dark";
        }

        return darkFileName;
    }

}
