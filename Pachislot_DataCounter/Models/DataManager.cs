using Pachislot_DataCounter.ViewModels;
using Pachislot_DataCounter.Views;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pachislot_DataCounter.Models
{
    public class DataManager
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private readonly IRegionManager _RegionManager = null;
        private ulong _BigBonus = 0;
        private ulong _RegularBonus = 0;
        private ulong _AllGame = 0;
        private ulong _CurrentGame = 0;
        private ulong _InCoin = 0;
        private ulong _OutCoin = 0;

        // =======================================================
        // プロパティ
        // =======================================================
        public ulong BigBonus
        {
            get { return _BigBonus; }
            set { _BigBonus = value; }
        }
        public ulong RegularBonus
        {
            get { return _RegularBonus; }
            set { _RegularBonus = value; }
        }
        public ulong AllGame
        {
            get { return _AllGame; }
            set { _AllGame = value; }
        }
        public ulong CurrentGame
        {
            get { return _CurrentGame; }
            set { _CurrentGame = value; }
        }
        public ulong InCoin
        {
            get { return _InCoin; }
            set { _InCoin = value; }
        }
        public ulong OutCoin
        {
            get { return _OutCoin; }
            set { _OutCoin = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataManager ( IRegionManager pRegionManager )
        {
            this._RegionManager = pRegionManager;
        }

        public void Convert ( string pReceivedData )
        {

        }

        public void UpdateCounters ( )
        {
            update_counter ( "BigBonusCounter", _BigBonus );
            update_counter ( "RegularBonusCounter", _RegularBonus );
            update_counter ( "AllGameCounter", _AllGame );
            update_counter ( "CurrentGameCounter", _CurrentGame );
            update_counter ( "InCoinCounter", _InCoin );
            update_counter ( "OutCoinCounter", _OutCoin );
        }

        private void update_counter ( string pCounterName, ulong pProperty )
        {
            var l_ContentRegion = _RegionManager.Regions[ pCounterName ];
            var l_ContentView = l_ContentRegion.Views.FirstOrDefault ( ) as Counter;
            var l_ContentViewModel = l_ContentView.DataContext as CounterViewModel;
            l_ContentViewModel.SetNumber ( pProperty );
        }
    }
}
