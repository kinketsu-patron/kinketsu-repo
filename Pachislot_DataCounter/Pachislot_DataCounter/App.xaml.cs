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
using System.Threading;
using System.Windows;

namespace Pachislot_DataCounter
{
        /// <summary>
        /// Interaction logic for App.xaml
        /// </summary>
        public partial class App : PrismApplication
        {
                private Mutex m_Mutex = new Mutex( false, "Pachislot_DataCounter" );

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
                        p_ContainerRegistry.RegisterForNavigation<BonusHistory, BonusHistoryViewModel>( );
                }

                /// <summary>
                /// モジュールがあるときに使用する
                /// </summary>
                /// <param name="p_ModuleCatalog"></param>
                protected override void ConfigureModuleCatalog( IModuleCatalog p_ModuleCatalog )
                {
                }

                /// <summary>
                /// スタートアップ時の処理
                /// </summary>
                /// <param name="sender">イベント元オブジェクト</param>
                /// <param name="e">スタートアップイベントデータ</param>
                private void PrismApplication_Startup( object sender, StartupEventArgs e )
                {
                        if ( m_Mutex.WaitOne( 0, false ) )
                        {
                                return;
                        }
                        MessageBox.Show( "二重起動できません", "情報", MessageBoxButton.OK, MessageBoxImage.Information );
                        m_Mutex.Close( );
                        m_Mutex = null;
                        Shutdown( );
                }

                /// <summary>
                /// アプリ終了時の処理
                /// </summary>
                /// <param name="sender">イベント元オブジェクト</param>
                /// <param name="e">終了イベントデータ</param>
                private void PrismApplication_Exit( object sender, ExitEventArgs e )
                {
                        if ( m_Mutex != null )
                        {
                                m_Mutex.ReleaseMutex( );
                                m_Mutex.Close( );
                        }
                }
        }
}
