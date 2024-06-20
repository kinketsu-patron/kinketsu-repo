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
using Prism.Mvvm;
using Prism.Regions;

namespace Pachislot_DataCounter.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "金ぱと データカウンター";
        public string Title
        {
            get { return _title; }
            set { SetProperty ( ref _title, value ); }
        }

        public MainWindowViewModel ( IRegionManager pRegionManager )
        {
            this._regionManager = pRegionManager;
            this._regionManager.RegisterViewWithRegion ( "BigBonusCounter", typeof ( BonusCounter ) );
            this._regionManager.RegisterViewWithRegion ( "RegularBonusCounter", typeof ( BonusCounter ) );
            this._regionManager.RegisterViewWithRegion ( "AllGameCounter", typeof ( GameCounter ) );
            this._regionManager.RegisterViewWithRegion ( "CurrentGameCounter", typeof ( GameCounter ) );
            this._regionManager.RegisterViewWithRegion ( "InCoinCounter", typeof ( CoinCounter ) );
            this._regionManager.RegisterViewWithRegion ( "OutCoinCounter", typeof ( CoinCounter ) );
        }
    }
}
