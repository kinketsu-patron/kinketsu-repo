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
using Pachislot_DataCounter.Models;
using Pachislot_DataCounter.ViewModels;
using Pachislot_DataCounter.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace Pachislot_DataCounter
{
        /// <summary>
        /// Interaction logic for App.xaml
        /// </summary>
        public partial class App : PrismApplication
        {
                /// <summary>
                /// Prismフレームワークで自動生成されるメソッド
                /// アプリ開始時に起動するWindowを設定する
                /// </summary>
                /// <returns>MainWindowのインスタンス</returns>
                protected override Window CreateShell( )
                {
                        return Container.Resolve<MainWindow>( );
                }

                /// <summary>
                /// ViewとViewModelの関連付けを行いDIコンテナに登録する
                /// </summary>
                /// <param name="containerRegistry">DIコンテナ</param>
                protected override void RegisterTypes( IContainerRegistry p_ContainerRegistry )
                {
                        p_ContainerRegistry.RegisterSingleton<DataManager>( );
                        p_ContainerRegistry.RegisterForNavigation<AllGameCounter, AllGameCounterViewModel>( );
                        p_ContainerRegistry.RegisterForNavigation<BBCounter, BBCounterViewModel>( );
                        p_ContainerRegistry.RegisterForNavigation<CurrentGameCounter, CurrentGameCounterViewModel>( );
                        p_ContainerRegistry.RegisterForNavigation<InCoinCounter, InCoinCounterViewModel>( );
                        p_ContainerRegistry.RegisterForNavigation<OutCoinCounter, OutCoinCounterViewModel>( );
                        p_ContainerRegistry.RegisterForNavigation<RBCounter, RBCounterViewModel>( );
                }

                /// <summary>
                /// モジュールがあるときに使用する
                /// </summary>
                /// <param name="p_ModuleCatalog"></param>
                protected override void ConfigureModuleCatalog( IModuleCatalog p_ModuleCatalog )
                {
                }
        }
}
