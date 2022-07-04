using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Uwp.HCore.Converter.ByteToObject
{
    public  class ByteToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            byte[] imageBytes = (byte[])value;
            return ConvertByteToImage(imageBytes).Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private async Task<BitmapImage> ConvertByteToImage(byte[] imageBytes)
        {
            BitmapImage image = new BitmapImage();
            using (InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(imageBytes);
                    await writer.StoreAsync();
                    image.SetSourceAsync(randomAccessStream);
                }
            }

            return image;
        }
        
    }
}
