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
using ScottPlot;
using ScottPlot.WPF;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace Pachislot_DataCounter.ViewModels
{
        public class MainWindowViewModel : BindableBase
        {
                #region メンバ変数
                // =======================================================
                // メンバ変数
                // =======================================================
                private SerialCom m_SerialCom;
                private DataManager m_DataManager;
                private string m_Title;
                private MetroWindow m_MetroWindow;
                private WpfPlot m_SlumpGraph;
                private List<uint> m_GamesList;
                private List<int> m_CoinDiffList;
                protected CompositeDisposable m_Disposables;
                #endregion

                #region コマンド
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
                #endregion

                #region プロパティ
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
                /// <summary>
                /// 累計ゲーム数
                /// </summary>
                public ReactiveProperty<uint> AllGame { get; }
                /// <summary>
                /// コイン差枚数
                /// </summary>
                public ReactiveProperty<int> Diff { get; }
                /// <summary>
                /// スランプグラフ
                /// </summary>
                public WpfPlot SlumpGraph
                {
                        get { return m_SlumpGraph; }
                        set { SetProperty( ref m_SlumpGraph, value ); }
                }
                /// <summary>
                /// ゲーム数(スランプグラフ表示用)
                /// </summary>
                public List<uint> GamesList
                {
                        get { return m_GamesList; }
                        set { SetProperty( ref m_GamesList, value ); }
                }
                /// <summary>
                /// コイン差枚数(スランプグラフ表示用)
                /// </summary>
                public List<int> CoinDiffList
                {
                        get { return m_CoinDiffList; }
                        set { SetProperty( ref m_CoinDiffList, value ); }
                }
                #endregion

                #region 公開メソッド
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
                        p_RegionManager.RegisterViewWithRegion( "BonusHistory", typeof( BonusHistory ) );

                        m_Title = "金ぱとデータカウンター";

                        m_MetroWindow = Application.Current.MainWindow as MetroWindow;
                        m_DataManager = p_DataManager;
                        m_SerialCom = new SerialCom( );
                        m_SlumpGraph = new WpfPlot( );
                        m_GamesList = new List<uint>( ) { 0 };
                        m_CoinDiffList = new List<int>( ) { 0 };
                        m_Disposables = new CompositeDisposable( );

                        Initialize_SlumpGraph( );

                        m_SerialCom.DataReceived += new SerialDataReceivedEventHandler( ReceivedGameData );
                        Click_Connect = new DelegateCommand( OnConnectClicked );
                        Click_Exit = new DelegateCommand<MainWindow>( OnExitClicked );

                        DuringBigBonus = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringBB ).AddTo( m_Disposables );
                        DuringRegularBonus = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringRB ).AddTo( m_Disposables );
                        DuringBonus = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringBonus ).AddTo( m_Disposables );
                        Diff = m_DataManager.ToReactivePropertyAsSynchronized( m => m.Diff ).AddTo( m_Disposables );
                        AllGame = m_DataManager.ToReactivePropertyAsSynchronized( m => m.AllGame ).AddTo( m_Disposables );
                        // 最初のコンストラクタが走った時を除き、10の倍数回のときにDrawGraphを呼ぶ
                        AllGame.Skip( 1 ).Where( game => game % 10 == 0 ).Subscribe( game => DrawGraph( game, Diff.Value ) );
                }
                #endregion

                #region 非公開メソッド
                /// <summary>
                /// スランプグラフの初期設定
                /// </summary>
                private void Initialize_SlumpGraph( )
                {
                        // Y軸
                        SlumpGraph.Plot.Axes.Left.Label.Text = "差枚数";
                        SlumpGraph.Plot.Axes.Left.Label.FontName = "BIZ UDゴシック";
                        SlumpGraph.Plot.Axes.Left.Label.ForeColor = Colors.Azure;
                        SlumpGraph.Plot.Axes.Left.Label.FontSize = 20;
                        SlumpGraph.Plot.Axes.Left.Label.Bold = true;
                        SlumpGraph.Plot.Axes.Left.MajorTickStyle.Color = Colors.GoldenRod;
                        SlumpGraph.Plot.Axes.Left.MinorTickStyle.Color = Colors.Transparent;
                        SlumpGraph.Plot.Axes.Left.MinorTickStyle.Length = 0;
                        SlumpGraph.Plot.Axes.Left.TickLabelStyle.ForeColor = Colors.Azure;
                        SlumpGraph.Plot.Axes.Left.TickLabelStyle.FontSize = 20;
                        SlumpGraph.Plot.Axes.Left.TickLabelStyle.Bold = true;
                        SlumpGraph.Plot.Axes.Left.FrameLineStyle.Color = Colors.Azure;
                        SlumpGraph.Plot.Axes.Left.FrameLineStyle.Width = 4;

                        ScottPlot.TickGenerators.NumericAutomatic l_TickGenY = new ScottPlot.TickGenerators.NumericAutomatic();
                        l_TickGenY.TargetTickCount = 5;
                        SlumpGraph.Plot.Axes.Left.TickGenerator = l_TickGenY;


                        // X軸
                        SlumpGraph.Plot.Axes.Bottom.Label.Text = "ゲーム数";
                        SlumpGraph.Plot.Axes.Bottom.Label.FontName = "BIZ UDゴシック";
                        SlumpGraph.Plot.Axes.Bottom.Label.ForeColor = Colors.Azure;
                        SlumpGraph.Plot.Axes.Bottom.Label.FontSize = 20;
                        SlumpGraph.Plot.Axes.Bottom.Label.Bold = true;
                        SlumpGraph.Plot.Axes.Bottom.MajorTickStyle.Color = Colors.GoldenRod;
                        SlumpGraph.Plot.Axes.Bottom.MinorTickStyle.Color = Colors.Transparent;
                        SlumpGraph.Plot.Axes.Bottom.MinorTickStyle.Length = 0;
                        SlumpGraph.Plot.Axes.Bottom.TickLabelStyle.ForeColor = Colors.Azure;
                        SlumpGraph.Plot.Axes.Bottom.TickLabelStyle.FontSize = 20;
                        SlumpGraph.Plot.Axes.Bottom.TickLabelStyle.Bold = true;
                        SlumpGraph.Plot.Axes.Bottom.FrameLineStyle.Color = Colors.Azure;
                        SlumpGraph.Plot.Axes.Bottom.FrameLineStyle.Width = 4;

                        ScottPlot.TickGenerators.NumericAutomatic l_TickGenX = new ScottPlot.TickGenerators.NumericAutomatic();
                        l_TickGenX.TargetTickCount = 6;
                        SlumpGraph.Plot.Axes.Bottom.TickGenerator = l_TickGenX;

                        // グリッド
                        SlumpGraph.Plot.Grid.MajorLineColor = Colors.Azure.WithOpacity( 0.2 );

                        // 全体
                        SlumpGraph.Plot.FigureBackground.Color = Colors.Transparent;
                        SlumpGraph.Plot.DataBackground.Color = Color.FromHex( "#1F1F1F" );

                        // 最初の軸最大最小を設定
                        SlumpGraph.Plot.Axes.SetLimits( 0, 1000, -1000, 1000 );

                        // 初期データを設定
                        var l_Line = SlumpGraph.Plot.Add.ScatterLine( GamesList, CoinDiffList );
                        l_Line.Color = Colors.Gold;
                        l_Line.LineWidth = 6;
                        l_Line.MarkerSize = 0;
                        SlumpGraph.Refresh( );
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
                /// <param name="p_Game">追加するゲーム数情報</param>
                /// <param name="p_CoinDiff">追加する差枚数情報</param>
                private void DrawGraph( uint p_Game, int p_CoinDiff )
                {
                        GamesList.Add( p_Game );
                        CoinDiffList.Add( p_CoinDiff );
                        var l_Line = SlumpGraph.Plot.Add.ScatterLine( GamesList, CoinDiffList );
                        l_Line.Color = Colors.Gold;
                        l_Line.LineWidth = 6;
                        l_Line.MarkerSize = 0;

                        AxisLimits l_Limits = SlumpGraph.Plot.Axes.GetLimits( );
                        double l_Min_X = l_Limits.Left;
                        double l_Max_X = l_Limits.Right;
                        double l_Min_Y = l_Limits.Bottom;
                        double l_Max_Y = l_Limits.Top;

                        if ( p_Game >= ( l_Max_X * 0.8 ) )
                        {
                                l_Max_X = l_Max_X * 2.0;
                        }

                        if ( CoinDiffList.Min( ) <= ( l_Min_Y * 0.8 ) )
                        {
                                l_Min_Y = l_Min_Y * 2.0;
                        }

                        if ( CoinDiffList.Max( ) >= ( l_Max_Y * 0.8 ) )
                        {
                                l_Max_Y = l_Max_Y * 2.0;
                        }

                        SlumpGraph.Plot.Axes.SetLimits( l_Min_X, l_Max_X, l_Min_Y, l_Max_Y );
                        SlumpGraph.Refresh( );
                }
                #endregion
        }
}