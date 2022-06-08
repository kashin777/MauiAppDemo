using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MauiAppDemo.Pages;

public class PageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        ValidationContext ctx = new ValidationContext(this) { MemberName = name };
        var results = new List<ValidationResult>();
        Validator.TryValidateProperty(GetType().GetProperty(name).GetValue(this), ctx, results);

        ClearError(name);
        foreach (var error in results)
        {
            AddError(name, error.ErrorMessage);
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public bool Validate()
    {
        // すでにエラーがある場合は検証しない
        if (HasErrors)
        {
            return false;
        }

        ValidationContext ctx = new ValidationContext(this);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(this, ctx, results, true);

        foreach (var result in results)
        {
            foreach (var property in result.MemberNames)
            {
                AddError(property, result.ErrorMessage);
            }
        }

        return !HasErrors;
    }


    public bool HasErrors
    {
        get
        {
            return _errors.Sum(r => r.Value.Count) > 0;
        }
    }

    public List<string> Errors
    {
        get
        {
            List<string> errors = new List<string>();

            foreach (var values in _errors.Values)
            {
                foreach (var value in values)
                {
                    errors.Add(value);
                }
            }
            return errors;
        }
    }

    internal void AddError(string name, string errorMessage)
    {
        if (!_errors.ContainsKey(name))
        {
            _errors[name] = new List<string>();
        }

        _errors[name].Add(errorMessage);

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Errors)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasErrors)));
    }


    public void ClearErrors()
    {
        _errors.Clear();

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Errors)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasErrors)));
    }

    public void ClearError(string name)
    {
        if (_errors.ContainsKey(name))
        {
            _errors[name].Clear();
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Errors)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasErrors)));
    }
}
