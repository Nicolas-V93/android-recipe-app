using System;
using System.Globalization;
using Xamarin.Forms;

namespace Imi.Project.Mobile.Converters
{
    public class BookmarkToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageName;

            if (value is bool isBookmarked && isBookmarked)
            {
                imageName = (Device.RuntimePlatform == Device.UWP) ? "Assets/heart_full.png" : "heart_full.png";
            }
            else
            {
                imageName = (Device.RuntimePlatform == Device.UWP) ? "Assets/TabIcons/heart.png" : "heart.png";
            }

            return ImageSource.FromFile(imageName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
