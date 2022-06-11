using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MauiAppDemo.Pages;

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
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    /// <summary>
    /// プロパティが変更されたときに呼び出す。
    /// DataAnnotationsでプロパティの検証を行う。
    /// </summary>
    /// <param name="PropertyName">プロパティ名</param>
    protected void OnPropertyChanged([CallerMemberName] string PropertyName = null)
    {
        // 呼び出し元のプロパティを検証
        ValidationContext ctx = new ValidationContext(this) { MemberName = PropertyName };
        var results = new List<ValidationResult>();
        Validator.TryValidateProperty(GetType().GetProperty(PropertyName).GetValue(this), ctx, results);

        // エラーメッセージを蓄積
        ClearError(PropertyName);
        foreach (var error in results)
        {
            AddError(PropertyName, error.ErrorMessage);
        }

        // プロパティ変更を通知
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
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
        foreach (var result in results)
        {
            foreach (var property in result.MemberNames)
            {
                AddError(property, result.ErrorMessage);
            }
        }

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

    /// <summary>
    /// 特定のプロパティのエラーを取得する。
    /// </summary>
    /// <param name="PropertyName">プロパティ名</param>
    /// <returns></returns>
    public List<string> PropertyErrors(string PropertyName)
    {
        return _errors.ContainsKey(PropertyName) ? _errors[PropertyName] : new List<string>();
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
            _errors[PropertyName] = new List<string>();
        }

        _errors[PropertyName].Add(ErrorMessage);

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Errors)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasErrors)));
    }

    /// <summary>
    /// すべてのエラーをクリアする。
    /// </summary>
    public void ClearErrors()
    {
        _errors.Clear();

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Errors)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasErrors)));
    }

    /// <summary>
    /// 特定のエラーをクリアする。
    /// </summary>
    /// <param name="PropertyName">プロパティ名</param>
    public void ClearError(string PropertyName)
    {
        if (_errors.ContainsKey(PropertyName))
        {
            _errors[PropertyName].Clear();
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Errors)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasErrors)));
    }
}
