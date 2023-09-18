using Imi.Project.Mobile.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Imi.Project.Mobile.Converters
{
    public class TotalTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Recipe recipe)
            {
                var totalMinutes = recipe.CookTime + recipe.PrepTime;
                var hours = totalMinutes / 60;
                var minutes = totalMinutes % 60;
                var totalTime = "";

                if (hours > 0)
                {
                    totalTime += $"{hours} hr ";
                }

                if (minutes > 0)
                {
                    totalTime += $"{minutes} min";
                }

                return totalTime;
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
