using System.Globalization;

namespace MauiAppDemo.Pages;

/// <summary>
/// ValidationPropertyBehaviorで使用するための型変換処理。
/// </summary>
public class ValidationPropertyConverter : IValueConverter
{
    /// <summary>
    /// 値の変換失敗時に返す値。
    /// </summary>
    public static readonly object CONVERT_ERROR = new object();

    /// <summary>
    /// 処理→画面の変換処理。
    /// </summary>
    /// <param name="value">変換前の値</param>
    /// <param name="targetType">変換後の型</param>
    /// <param name="parameter">パラメータ</param>
    /// <param name="culture">カルチャ</param>
    /// <returns>変換後の値</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }

    /// <summary>
    /// 画面→処理の変換処理
    /// </summary>
    /// <param name="value">変換前の値</param>
    /// <param name="targetType">変換後の型</param>
    /// <param name="parameter">パラメータ</param>
    /// <param name="culture">カルチャ</param>
    /// <returns>変換後の値</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // NULL、空文字の場合は、NULLに
        if(value == null 
            || (value.GetType() == typeof(string) && value.ToString().Trim().Length == 0))
        {
            return null;
        }

        // NULL許容型の場合、内包型を取得する
        // Nullable<T> convert T
        targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        // 文字列の場合
        if (targetType == typeof(string))
        {
            return value.ToString();
        }
        // その他の場合
        else
        {
            // public static object Parse(object obj)メソッドを保持する型のみサポートする。
            var method = targetType.GetMethod("Parse", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, new Type[] { typeof(string) });
            try
            {
                return method.Invoke(null, new object[] { value.ToString() });
            }
            catch /* (Exception ex) */
            {
                // 変換エラー
                return CONVERT_ERROR;
            }
        }
    }
}