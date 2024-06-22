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
using System.Linq;

namespace Pachislot_DataCounter.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private readonly IRegionManager _RegionManager = null;
        private SerialCom _SerialCom = null;
        private string _Title = "金ぱと データカウンター";

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
            get { return _Title; }
            set { SetProperty ( ref _Title, value ); }
        }

        // =======================================================
        // メソッド
        // =======================================================
        /// <summary>
        /// MainWindowのビューモデルのコンストラクタ
        /// </summary>
        /// <param name="pRegionManager">リージョン</param>
        public MainWindowViewModel ( IRegionManager pRegionManager )
        {
            this._RegionManager = pRegionManager;
            this._RegionManager.RegisterViewWithRegion ( "BigBonusCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "RegularBonusCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "AllGameCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "CurrentGameCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "InCoinCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "OutCoinCounter", typeof ( Counter ) );

            this._SerialCom = new SerialCom ( _RegionManager );
            this.Click_Connect = new DelegateCommand ( OnConnectClicked );
            this.Click_Exit = new DelegateCommand<MainWindow> ( OnExitClicked );


        }

        /// <summary>
        /// 接続ボタンクリック時の処理
        /// </summary>
        private void OnConnectClicked ( )
        {
            _SerialCom.ComStart ( );                    // シリアル通信を開始する
        }

        /// <summary>
        /// 終了ボタンクリック時の処理
        /// </summary>
        private void OnExitClicked ( MainWindow pWindow )
        {
            _SerialCom.ComStop ( ); // シリアル通信を停止する
            pWindow?.Close ( );     // nullでなければウィンドウを閉じる
        }
    }
}
