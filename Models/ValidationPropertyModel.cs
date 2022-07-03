using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MauiAppDemo.Models;

/// <summary>
/// ページ毎のビューモデル。
/// </summary>
public class ValidationPropertyModel : INotifyPropertyChanged
{
    /// <summary>
    /// 複合エラーなど、モデル全体に関するエラーを管理するための定数。
    /// </summary>
    public const string MODEL_ERROR = "_MODEL_";

    /// <summary>
    /// プロパティが変更されたことを通知するイベントハンドラ。
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// エラーメッセージを、プロパティ毎に保持する。
    /// </summary>
    private Dictionary<string, ObservableCollection<string>> _errors = new Dictionary<string, ObservableCollection<string>>();

    /// <summary>
    /// プロパティが変更されたときに呼び出す。
    /// DataAnnotationsでプロパティの検証を行う。
    /// </summary>
    /// <param name="PropertyName">プロパティ名</param>
    protected void SetProperty<T>(ref T Property, T Value, [CallerMemberName] string PropertyName = null)
    {
        // 値を設定
        Property = Value;

        // 呼び出し元のプロパティを検証
        ValidationContext ctx = new ValidationContext(this) { MemberName = PropertyName };
        var results = new List<ValidationResult>();
        Validator.TryValidateProperty(GetType().GetProperty(PropertyName).GetValue(this), ctx, results);

        // エラーメッセージを蓄積
        ObservableCollection<string> errors = new ObservableCollection<string>();
        foreach (var error in results)
        {
            errors.Add(error.ErrorMessage);
        }

        ClearError(PropertyName, false);
        AddError(PropertyName, errors);

        // プロパティ変更を通知
        OnPropertyChanged(PropertyName);
    }

    /// <summary>
    /// 検証処理。
    /// </summary>
    /// <returns>エラーがない場合はtrue</returns>
    public bool Validate()
    {
        // すでにエラーがある場合は、false
        if (HasErrors)
        {
            return false;
        }

        // すべてのプロパティを検証
        ValidationContext ctx = new ValidationContext(this);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(this, ctx, results, true);

        // エラーメッセージを蓄積
        Dictionary<string, ObservableCollection<string>> errorsDict = new Dictionary<string, ObservableCollection<string>>();
        foreach (var result in results)
        {
            foreach (var property in result.MemberNames)
            {
                if (!errorsDict.ContainsKey(property))
                {
                    errorsDict[property] = new ObservableCollection<string>();
                }

                errorsDict[property].Add(result.ErrorMessage);
            }
        }
        _errors = errorsDict;

        OnPropertyChanged(nameof(HasErrors));
        OnPropertyChanged(nameof(Errors));

        // 検証結果を返す
        return !HasErrors;
    }

    /// <summary>
    /// 検証エラーが存在するか。
    /// </summary>
    public bool HasErrors
    {
        get
        {
            return _errors.Sum(r => r.Value.Count) > 0;
        }
    }

    /// <summary>
    /// すべてのエラーメッセージ。
    /// </summary>
    public ObservableCollection<string> Errors
    {
        get
        {
            ObservableCollection<string> errors = new ObservableCollection<string>();
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

    /// <summary>
    /// 特定のプロパティのエラーを取得する。
    /// </summary>
    /// <param name="PropertyName">プロパティ名</param>
    /// <returns></returns>
    public ObservableCollection<string> PropertyErrors(string PropertyName)
    {
        return _errors.ContainsKey(PropertyName) ? _errors[PropertyName] : new ObservableCollection<string>();
    }

    /// <summary>
    ///  複合エラーなど、モデル全体に関するエラーを追加する。
    /// </summary>
    /// <param name="errorMessage"></param>
    public void AddModelError(string errorMessage)
    {
        AddError(MODEL_ERROR, errorMessage);
    }

    /// <summary>
    /// 複合エラーなど、モデル全体に関するエラーを削除する。
    /// </summary>
    internal void ClearModelError()
    {
        ClearError(MODEL_ERROR);
    }

    /// <summary>
    /// 特定のプロパティのエラーを追加する。
    /// </summary>
    /// <param name="PropertyName">プロパティ名</param>
    /// <param name="ErrorMessage">エラーメッセージ</param>
    public void AddError(string PropertyName, string ErrorMessage)
    {
        if (!_errors.ContainsKey(PropertyName))
        {
            _errors[PropertyName] = new ObservableCollection<string>();
        }

        _errors[PropertyName].Add(ErrorMessage);

        OnPropertyChanged(nameof(HasErrors));
        OnPropertyChanged(nameof(Errors));
    }

    public void AddError(string PropertyName, ObservableCollection<string> ErrorMessages)
    {
        _errors[PropertyName] = ErrorMessages;

        OnPropertyChanged(nameof(HasErrors));
        OnPropertyChanged(nameof(Errors));
    }

    /// <summary>
    /// すべてのエラーをクリアする。
    /// </summary>
    public void ClearErrors(bool NotifyChanged = true)
    {
        _errors.Clear();

        if (NotifyChanged)
        {
            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged(nameof(Errors));
        }
    }

    /// <summary>
    /// 特定のエラーをクリアする。
    /// </summary>
    /// <param name="PropertyName">プロパティ名</param>
    public void ClearError(string PropertyName, bool NotifyChanged = true)
    {
        if (_errors.ContainsKey(PropertyName))
        {
            _errors[PropertyName].Clear();
        }

        if (NotifyChanged)
        {
            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged(nameof(Errors));
        }
    }

    /// <summary>
    /// PorpoertyChangedイベントを発火する。
    /// </summary>
    /// <param name="PropertyName">プロパティ名</param>
    private void OnPropertyChanged(string PropertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }
}
