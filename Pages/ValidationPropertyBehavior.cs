using MauiAppDemo.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppDemo.Pages;

/// <summary>
/// ValidationPropertyModelのプロパティを検証する。
/// </summary>
public class ValidationPropertyBehavior : Behavior<Entry>
{
    /// <summary>
    /// 対象のプロパティ名。
    /// </summary>
    public string PropertyName { get; set; }

    protected override void OnAttachedTo(Entry bindable)
    {
        bindable.TextChanged += Bindable_TextChanged;
        //bindable.Unfocused += Bindable_Unfocused;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        bindable.TextChanged -= Bindable_TextChanged;
        //bindable.Unfocused -= Bindable_Unfocused;
        base.OnDetachingFrom(bindable);
    }

    private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
    {
        PropertyCheck(sender);
    }

    private void Bindable_Unfocused(object sender, FocusEventArgs e)
    {
        PropertyCheck(sender);
    }

    /// <summary>
    /// プロパティの検証処理
    /// </summary>
    /// <param name="sender">対象のUIコンポネント</param>
    private void PropertyCheck(object sender)
    {
        // 入力値を取得する
        var entry = sender as Entry;
        var value = entry.GetValue(Entry.TextProperty);

        // データモデルを取得する
        var model = entry.BindingContext as ValidationPropertyModel;

        // 複合エラーをクリア
        model.ClearModelError();

        // プロパティ情報
        var property = model.GetType().GetProperty(PropertyName);
        // プロパティの型
        var propertyType = property.PropertyType;
        // プロパティのDisplay属性
        var displayAttribute = GetCustomAttribute<DisplayAttribute>(property);
        // プロパティのRequired属性
        var requiredAttribute = GetCustomAttribute<RequiredAttribute>(property);

        // コンバーターによる変換
        var converter = new ValidationPropertyConverter();        
        var convertedValue = converter.ConvertBack(value, propertyType, null, null);

        // 変換結果によりエラーを返す
        string msg = null;

        // 必須なのにNULL
        if (requiredAttribute != null && convertedValue == null)
        {
            msg = requiredAttribute.FormatErrorMessage(displayAttribute.GetName());
        }
        // 形式エラー
        else if (convertedValue == ValidationPropertyConverter.CONVERT_ERROR)
        {
            msg = string.Format(Messages.Error_Converter, displayAttribute.GetName());
        }

        // エラーがあれば設定
        if (msg != null)
        {
            model.ClearError(PropertyName);
            model.AddError(PropertyName, msg);
        }
    }

    /// <summary>
    /// プロパティの属性を取得する
    /// </summary>
    /// <typeparam name="T">属性の型</typeparam>
    /// <param name="property">プロパティ情報</param>
    /// <returns>プロパティの属性</returns>
    private T GetCustomAttribute<T>(PropertyInfo property)
    {
        var attributes = property.GetCustomAttributes(typeof(T), true);

        if(attributes != null && attributes.Length > 0)
        {
            return (T) attributes[0];
        }

        return default(T);
    }
}