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
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Pachislot_DataCounter.ViewModels
{
        public class MainWindowViewModel : BindableBase
        {
                // =======================================================
                // メンバ変数
                // =======================================================
                private readonly IRegionManager m_RegionManager;
                private SerialCom m_SerialCom;
                private DataManager m_DataManager;
                private string m_Title;
                private bool m_DuringBigBonus;
                private bool m_DuringRegularBonus;
                private bool m_DuringBonus;

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

                /// <summary>
                /// ビッグボーナス中フラグ
                /// </summary>
                public bool DuringBigBonus
                {
                        get { return m_DuringBigBonus; }
                        set { SetProperty( ref m_DuringBigBonus, value ); }
                }

                /// <summary>
                /// レギュラーボーナス中フラグ
                /// </summary>
                public bool DuringRegularBonus
                {
                        get { return m_DuringRegularBonus; }
                        set { SetProperty( ref m_DuringRegularBonus, value ); }
                }

                /// <summary>
                /// ボーナス中フラグ
                /// </summary>
                public bool DuringBonus
                {
                        get { return m_DuringBonus; }
                        set { SetProperty( ref m_DuringBonus, value ); }
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
                        m_Title = "金ぱとデータカウンター";

                        m_RegionManager = p_RegionManager;
                        m_RegionManager.RegisterViewWithRegion( "BigBonusCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "RegularBonusCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "AllGameCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "CurrentGameCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "InCoinCounter", typeof( Counter ) );
                        m_RegionManager.RegisterViewWithRegion( "OutCoinCounter", typeof( Counter ) );

                        m_SerialCom = new SerialCom( );
                        m_SerialCom.DataReceived += new SerialDataReceivedEventHandler( ReceivedGameData );
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

                /// <summary>
                /// SerialComのデータ受信イベントの処理
                /// </summary>
                /// <param name="sender">SerialComのインスタンス</param>
                /// <param name="e">SerialDataReceivedイベントデータ</param>
                private void ReceivedGameData( object sender, SerialDataReceivedEventArgs e )
                {
                        string l_SerialMessage = ( ( SerialCom )sender ).GetSerialMessage ( );

                        Application.Current.Dispatcher.Invoke( ( ) =>
                        {
                                m_DataManager.Convert( l_SerialMessage );
                                m_DataManager.UpdateCounters( );
                        } );
                }
        }
}
