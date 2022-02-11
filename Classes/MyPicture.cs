using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharePoint.Classes
{
    public class MyPicture : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        // public static extern bool DeleteObject(IntPtr hObject);
        // private BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        // {
        //    IntPtr hBitmap = bitmap.GetHbitmap();
        //    BitmapImage retval;
        //    try
        //    {
        //        retval = (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(
        //                     hBitmap,
        //                     IntPtr.Zero,
        //                     Int32Rect.Empty,
        //                     BitmapSizeOptions.FromEmptyOptions());
        //    }
        //    finally
        //    {
        //        DeleteObject(hBitmap);
        //    }
        //    return retval;
        // }

        private static string DecodeRational64u(System.Drawing.Imaging.PropertyItem propertyItem)
        {
            uint dN = BitConverter.ToUInt32(propertyItem.Value, 0);
            uint dD = BitConverter.ToUInt32(propertyItem.Value, 4);
            uint mN = BitConverter.ToUInt32(propertyItem.Value, 8);
            uint mD = BitConverter.ToUInt32(propertyItem.Value, 12);
            uint sN = BitConverter.ToUInt32(propertyItem.Value, 16);
            uint sD = BitConverter.ToUInt32(propertyItem.Value, 20);

            decimal deg;
            decimal min;
            decimal sec;

            // Found some examples where you could get a zero denominator and no one likes to devide by zero
            if (dD > 0) { deg = (decimal)dN / dD; } else { deg = dN; }
            if (mD > 0) { min = (decimal)mN / mD; } else { min = mN; }
            if (sD > 0) { sec = (decimal)sN / sD; } else { sec = sN; }

            if (sec == 0) return string.Format("{0}° {1:0.###}'", deg, min);
            else return string.Format("{0}° {1:0}' {2:0.#}\"", deg, min, sec);
        }

        private void GetLocation(string path)
        {
            Image image = null;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            image = Image.FromStream(fs);
            if (Array.IndexOf<int>(image.PropertyIdList, 1) != -1 &&
                    Array.IndexOf<int>(image.PropertyIdList, 2) != -1 &&
                    Array.IndexOf<int>(image.PropertyIdList, 3) != -1 &&
                    Array.IndexOf<int>(image.PropertyIdList, 4) != -1)
            {

                string gpsLatitudeRef = BitConverter.ToChar(image.GetPropertyItem(1).Value, 0).ToString();
                string latitude = DecodeRational64u(image.GetPropertyItem(2));
                string gpsLongitudeRef = BitConverter.ToChar(image.GetPropertyItem(3).Value, 0).ToString();
                string longitude = DecodeRational64u(image.GetPropertyItem(4));
                Console.WriteLine("{0}\t{1} {2}, {3} {4}", path, gpsLatitudeRef, latitude, gpsLongitudeRef, longitude);
            }

        }

        ImageSource CreateResizedImage(ImageSource source, int width, int height)
        {
            Rect rect = new Rect(0, 0, width, height);

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(source, rect);
            }

            RenderTargetBitmap resizedImage = new RenderTargetBitmap(
                (int)rect.Width, (int)rect.Height,  // Resized dimensions
                96, 96,                             // Default DPI values
                PixelFormats.Default);
            resizedImage.Render(drawingVisual);

            return resizedImage;
        }

        public MyPicture(string path)
        {
            try
            {
                Bitmap bitmap = new Bitmap(path);
                double width = bitmap.Width;
                double height = bitmap.Height;
                if (height > 100)
                {
                    double factor = 100 / height;
                    height = 100;
                    width *= factor;
                }
                bitmap = null;

                BitmapImage bImage = new BitmapImage(new Uri(path));
                Picture = CreateResizedImage(bImage, (int)width, (int)height);
                MiniPic = CreateResizedImage(bImage, 32, 32);

                GetLocation(path);

                return;

                Bitmap resized = new Bitmap(bitmap, new System.Drawing.Size((int)width, (int)height));
                MemoryStream ms = new MemoryStream();

                using (MemoryStream memory = new MemoryStream())
                {
                    string ext = Path.GetExtension(path).ToLower();
                    System.Drawing.Imaging.ImageFormat myf = System.Drawing.Imaging.ImageFormat.Bmp;
                    if (ext == ".jpg")
                        myf = System.Drawing.Imaging.ImageFormat.Jpeg;
                    else if (ext == ".png")
                        myf = System.Drawing.Imaging.ImageFormat.Png;
                    else if (ext == ".emf")
                        myf = System.Drawing.Imaging.ImageFormat.Emf;
                    else if (ext == ".gif")
                        myf = System.Drawing.Imaging.ImageFormat.Gif;

                    resized.Save(memory, myf);
                    ms.Position = 0;
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                    bi.Freeze();
                    Picture = bi;
                }
            }
            catch (Exception exc)
            {
                invalid = true;
            }

        }
        public bool invalid = false;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private ImageSource _miniPic = null;
        public ImageSource MiniPic
        {
            get => _miniPic;
            set
            {
                _miniPic = value;
                OnPropertyChanged("MiniPic");
            }
        }

        private ImageSource _picture = null;
        public ImageSource Picture
        {
            get => _picture;
            set
            {
                _picture = value;
                OnPropertyChanged("Picture");
            }
        }
    }
}
