using System.Globalization;

namespace MauiAppDemo.Pages
{
    public class ValidationPropertyConverter : IValueConverter
    {
        public static readonly object CONVERT_ERROR = new object();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null || value.ToString().Trim().Length == 0)
            {
                return null;
            }

            // Nullable<T> convert T
            targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (targetType == typeof(string))
            {
                return value.ToString();
            }
            else
            {
                var method = targetType.GetMethod("Parse", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, new Type[] { typeof(string) });
                try
                {
                    return method.Invoke(null, new object[] { value.ToString() });
                }
                catch (Exception ex)
                {
                    return CONVERT_ERROR;
                }
            }
        }
    }
}