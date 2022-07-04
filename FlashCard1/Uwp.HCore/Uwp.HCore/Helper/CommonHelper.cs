using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ViewManagement;

namespace Uwp.HCore.Helper
{
    public sealed class CommonHelper
    {
        //Thay đổi size launch screen
        static public void ChangeSizeLaunchScreen(Size size)
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(size);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = size;
        }

        //Get Random String
        static public string GetRandomString(int length)
        {
            string str = "1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./`~!@#$%^&*()_+{}|:\"<>?";
            string randomStr = "";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                randomStr += str[random.Next(0, str.Length)];
            }
            return randomStr;
        }

        //Convert file image -> byte[]
        public static async Task<byte[]> ConvertImageToByte(StorageFile file)
        {
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                var readStream = inputStream.AsStreamForRead();

                var byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                return byteArray;
            }

        }
    }
}
