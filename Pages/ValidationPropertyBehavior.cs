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

namespace MauiAppDemo.Pages
{
    public class ValidationPropertyBehavior : Behavior<Entry>
    {
        public string Property { get; set; }

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

        private void PropertyCheck(object sender)
        {
            var entry = sender as Entry;
            var value = entry.GetValue(Entry.TextProperty);

            var model = entry.BindingContext as PageViewModel;
            model.ClearModelError();

            var parameter = model.GetType().GetProperty(Property);
            var parameterType = parameter.PropertyType;
            var displayAttribute = GetCustomAttribute<DisplayAttribute>(parameter);
            var requiredAttribute = GetCustomAttribute<RequiredAttribute>(parameter);

            string msg = null;

            // コンバーターによるチェック
            var converter = new ValidationPropertyConverter();
            try
            {
                var conv = converter.ConvertBack(value, parameterType, null, null);
                if (requiredAttribute != null && conv == null)
                {
                    msg = requiredAttribute.FormatErrorMessage(displayAttribute.Name);
                }
                else if (conv == ValidationPropertyConverter.CONVERT_ERROR)
                {
                    msg = $"{displayAttribute.Name}の形式が不正です。";
                }
            }
            catch (Exception ex)
            {
                msg = $"{displayAttribute.Name}の形式が不正です。";
            }

            if (msg != null)
            {
                model.ClearError(Property);
                model.AddError(Property, msg);
            }
        }

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
}