using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiAppDemo.Pages
{
    /// <summary>
    /// ビューモデルの共通クラス。
    /// </summary>
    public class ValidationPropertyViewModel : ValidationPropertyModel
    {
        /// <summary>
        /// コマンド一覧
        /// </summary>
        private List<ICommand> _Commands = new List<ICommand>();

        /// <summary>
        /// コマンドを登録する
        /// </summary>
        /// <param name="commands">コマンド一覧</param>
        protected void AddCommands(params ICommand[] commands)
        {
            foreach (var cmd in commands)
            {
                _Commands.Add(cmd);
            }
        }

        /// <summary>
        /// コマンドの実行可否判定イベントを発火する。
        /// </summary>
        public void ChangeCanExecute()
        {
            ChangeCanExecute(_Commands.ToArray());
        }

        /// <summary>
        /// コマンドの実行可否判定イベントを発火する。
        /// </summary>
        /// <param name="commands">対象のコマンド一覧</param>
        public void ChangeCanExecute(params ICommand[] commands)
        {
            App.Current.Dispatcher.Dispatch(() =>
            {
                foreach (var cmd in commands)
                {
                    (cmd as Command)?.ChangeCanExecute();
                }
            });
        }
    }
}
