/**
 * =============================================================
 * File         :MainWindowViewModel.cs
 * Summary      :MainWindowのビューモデル
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/18
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Pachislot_DataCounter.Models;
using Pachislot_DataCounter.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.IO.Ports;
using System.Linq;
using System.Windows;

namespace Pachislot_DataCounter.ViewModels
{
        public class MainWindowViewModel : BindableBase
        {
                // =======================================================
                // メンバ変数
                // =======================================================
                private readonly IRegionManager m_RegionManager = null;
                private SerialCom m_SerialCom = null;
                private DataManager m_DataManager = null;
                private string m_Title = "金ぱと データカウンター";

                // =======================================================
                // delegate
                // =======================================================
                public delegate void ThreadReturn( );

                // =======================================================
                // プロパティ
                // =======================================================
                /// <summary>
                /// Connectボタンクリックコマンド
                /// </summary>
                public DelegateCommand Click_Connect { get; private set; }

                /// <summary>
                /// Exitボタンクリックコマンド
                /// </summary>
                public DelegateCommand<MainWindow> Click_Exit { get; private set; }

                /// <summary>
                /// アプリタイトルのプロパティ
                /// </summary>
                public string Title
                {
                        get { return m_Title; }
                        set { SetProperty( ref m_Title, value ); }
                }

                // =======================================================
                // メソッド
                // =======================================================
                /// <summary>
                /// MainWindowのビューモデルのコンストラクタ
                /// </summary>
                /// <param name="p_RegionManager">リージョン</param>
                public MainWindowViewModel( IRegionManager p_RegionManager )
                {
                        m_RegionManager = p_RegionManager;
                        m_RegionManager.RegisterViewWithRegion( "BigBonusCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "RegularBonusCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "AllGameCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "CurrentGameCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "InCoinCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "OutCoinCounter", typeof( Counter ) );

                        m_SerialCom = new SerialCom( );
                        m_SerialCom.DataReceived += ReceivedGameData;
                        m_DataManager = new DataManager( p_RegionManager );
                        Click_Connect = new DelegateCommand( OnConnectClicked );
                        Click_Exit = new DelegateCommand<MainWindow>( OnExitClicked );
                }

                /// <summary>
                /// 接続ボタンクリック時の処理
                /// </summary>
                private void OnConnectClicked( )
                {
                        m_SerialCom.ComStart( ); // シリアル通信を開始する
                }

                /// <summary>
                /// 終了ボタンクリック時の処理
                /// </summary>
                private void OnExitClicked( MainWindow p_Window )
                {
                        m_SerialCom.ComStop( ); // シリアル通信を停止する
                        p_Window?.Close( );     // nullでなければウィンドウを閉じる
                }

                private void ReceivedGameData( object sender, SerialDataReceivedEventArgs e )
                {
                        string l_SerialMessage = m_SerialCom.GetSerialMessage ( );
                        m_DataManager.Convert( l_SerialMessage );
                        Application.Current.Dispatcher.Invoke( new ThreadReturn( m_DataManager.UpdateCounters ) );
                }
        }
}
