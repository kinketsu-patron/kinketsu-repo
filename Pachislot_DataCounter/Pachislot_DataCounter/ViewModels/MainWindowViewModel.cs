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
using LiveCharts;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Pachislot_DataCounter.Models;
using Pachislot_DataCounter.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;
using System;
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
                private SerialCom m_SerialCom;
                private DataManager m_DataManager;
                private string m_Title;
                private MetroWindow m_MetroWindow;
                private ChartValues<int> m_CoinDiff;
                private int m_Max_X;
                private int m_Min_Y;
                private int m_Max_Y;
                protected CompositeDisposable m_Disposables;

                // =======================================================
                // コマンド
                // =======================================================
                /// <summary>
                /// Connectボタンクリックコマンド
                /// </summary>
                public DelegateCommand Click_Connect { get; private set; }

                /// <summary>
                /// Exitボタンクリックコマンド
                /// </summary>
                public DelegateCommand<MainWindow> Click_Exit { get; private set; }

                // =======================================================
                // プロパティ
                // =======================================================
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
                public ReactiveProperty<bool> DuringBigBonus { get; }

                /// <summary>
                /// レギュラーボーナス中フラグ
                /// </summary>
                public ReactiveProperty<bool> DuringRegularBonus { get; }

                /// <summary>
                /// ボーナス中フラグ
                /// </summary>
                public ReactiveProperty<bool> DuringBonus { get; }

                public ReactiveProperty<uint> CurrentGame { get; }

                /// <summary>
                /// コイン差枚数
                /// </summary>
                public ReactiveProperty<int> Diff { get; }

                /// <summary>
                /// コイン差枚数
                /// </summary>
                public ChartValues<int> CoinDiff
                {
                        get { return m_CoinDiff; }
                        set { SetProperty( ref m_CoinDiff, value ); }
                }

                public int Max_X
                {
                        get { return m_Max_X; }
                        set { SetProperty( ref m_Max_X, value ); }
                }

                public int Min_Y
                {
                        get { return m_Min_Y; }
                        set { SetProperty( ref m_Min_Y, value ); }
                }

                public int Max_Y
                {
                        get { return m_Max_Y; }
                        set { SetProperty( ref m_Max_Y, value ); }
                }

                // =======================================================
                // メソッド
                // =======================================================
                /// <summary>
                /// MainWindowのビューモデルのコンストラクタ
                /// </summary>
                public MainWindowViewModel( IRegionManager p_RegionManager, DataManager p_DataManager )
                {
                        p_RegionManager.RegisterViewWithRegion( "BigBonusCounter", typeof( BBCounter ) );
                        p_RegionManager.RegisterViewWithRegion( "RegularBonusCounter", typeof( RBCounter ) );
                        p_RegionManager.RegisterViewWithRegion( "AllGameCounter", typeof( AllGameCounter ) );
                        p_RegionManager.RegisterViewWithRegion( "CurrentGameCounter", typeof( CurrentGameCounter ) );
                        p_RegionManager.RegisterViewWithRegion( "InCoinCounter", typeof( InCoinCounter ) );
                        p_RegionManager.RegisterViewWithRegion( "OutCoinCounter", typeof( OutCoinCounter ) );

                        m_Title = "金ぱとデータカウンター";
                        m_CoinDiff = new ChartValues<int>( );

                        m_MetroWindow = Application.Current.MainWindow as MetroWindow;
                        m_DataManager = p_DataManager;
                        m_SerialCom = new SerialCom( );
                        m_Disposables = new CompositeDisposable( );

                        m_SerialCom.DataReceived += new SerialDataReceivedEventHandler( ReceivedGameData );
                        Click_Connect = new DelegateCommand( OnConnectClicked );
                        Click_Exit = new DelegateCommand<MainWindow>( OnExitClicked );

                        DuringBigBonus = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringBB ).AddTo( m_Disposables );
                        DuringRegularBonus = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringRB ).AddTo( m_Disposables );
                        DuringBonus = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringBonus ).AddTo( m_Disposables );
                        Diff = m_DataManager.ToReactivePropertyAsSynchronized( m => m.Diff ).AddTo( m_Disposables );
                        CurrentGame = m_DataManager.ToReactivePropertyAsSynchronized( m => m.CurrentGame ).AddTo( m_Disposables );
                        CurrentGame.Subscribe( _ => DrawGraph( Diff.Value ) );
                }

                /// <summary>
                /// 接続ボタンクリック時の処理
                /// </summary>
                private void OnConnectClicked( )
                {
                        try
                        {
                                m_SerialCom.ComStart( ); // シリアル通信を開始する
                        }
                        catch ( Exception ex )
                        {
                                ShowMessageBox( "エラー", ex.Message );
                        }


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
                        try
                        {
                                GameInfo l_GameInfo = ( ( SerialCom )sender ).GetGameInfo( );

                                Application.Current.Dispatcher.Invoke( ( ) =>
                                {
                                        m_DataManager.Store( l_GameInfo );
                                } );
                        }
                        catch ( Exception ex )
                        {
                                ShowMessageBox( "エラー", ex.Message );
                        }
                }

                /// <summary>
                /// メッセージダイアログを表示
                /// </summary>
                /// <param name="p_Title">メッセージボックスタイトル</param>
                /// <param name="p_Message">メッセージ</param>
                private async void ShowMessageBox( string p_Title, string p_Message )
                {
                        await m_MetroWindow.ShowMessageAsync( p_Title, p_Message );
                }

                /// <summary>
                /// グラフを描画・更新
                /// </summary>
                /// <param name="p_CoinDiff">追加する差枚数情報</param>
                private void DrawGraph( int p_CoinDiff )
                {
                        Max_X = ( decimal.ToInt32( Math.Truncate( ( ( decimal )m_CoinDiff.Count * 10 / 8 ) / 200 ) ) + 1 ) * 200;
                        Min_Y = -1000;
                        Max_Y = 1000;
                        CoinDiff.Add( p_CoinDiff );
                }
        }
}