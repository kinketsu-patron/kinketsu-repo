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
using Pachislot_DataCounter.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Pachislot_DataCounter.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// リージョンマネージャー
        /// </summary>
        private readonly IRegionManager _RegionManager;

        /// <summary>
        /// Connectボタンクリックコマンド
        /// </summary>
        public DelegateCommand Click_Connect { get; private set; }

        /// <summary>
        /// Exitボタンクリックコマンド
        /// </summary>
        public DelegateCommand<MainWindow> Click_Exit { get; private set; }

        private string _Title = "金ぱと データカウンター";
        /// <summary>
        /// アプリタイトルのプロパティ
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { SetProperty ( ref _Title, value ); }
        }

        /// <summary>
        /// MainWindowのビューモデルのコンストラクタ
        /// </summary>
        /// <param name="pRegionManager"></param>
        public MainWindowViewModel ( IRegionManager pRegionManager )
        {
            this._RegionManager = pRegionManager;
            this._RegionManager.RegisterViewWithRegion ( "BigBonusCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "RegularBonusCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "AllGameCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "CurrentGameCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "InCoinCounter", typeof ( Counter ) );
            this._RegionManager.RegisterViewWithRegion ( "OutCoinCounter", typeof ( Counter ) );

            this.Click_Connect = new DelegateCommand ( OnConnectClicked );
            this.Click_Exit = new DelegateCommand<MainWindow> ( OnExitClicked );
        }

        /// <summary>
        /// 接続ボタンクリック時の処理
        /// </summary>
        private void OnConnectClicked ( )
        {

        }

        /// <summary>
        /// 終了ボタンクリック時の処理
        /// </summary>
        private void OnExitClicked ( MainWindow pWindow )
        {
            pWindow?.Close ( );     // nullでなければウィンドウを閉じる
        }
    }
}
