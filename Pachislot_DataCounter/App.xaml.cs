/**
 * =============================================================
 * File         :App.xaml.cs
 * Summary      :App.xamlのコードビハインドクラス
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/18
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Pachislot_DataCounter.Views;
using Prism.Ioc;
using System.Windows;

namespace Pachislot_DataCounter
{
        /// <summary>
        /// Interaction logic for App.xaml
        /// </summary>
        public partial class App
        {
                /// <summary>
                /// Prismフレームワークで自動生成されるメソッド
                /// アプリ開始時に起動するWindowを設定する
                /// </summary>
                /// <returns>MainWindowのType</returns>
                protected override Window CreateShell()
                {
                        return Container.Resolve<MainWindow>();
                }

                /// <summary>
                /// ViewとViewModelの関連付けを登録する
                /// </summary>
                /// <param name="containerRegistry"></param>
                protected override void RegisterTypes(IContainerRegistry containerRegistry)
                {
            
                }
        }
}
