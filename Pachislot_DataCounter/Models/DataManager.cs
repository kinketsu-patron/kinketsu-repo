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
                private readonly IRegionManager m_RegionManager = null;
                private ulong m_BigBonus = 0;
                private ulong m_RegularBonus = 0;
                private ulong m_AllGame = 0;
                private ulong m_CurrentGame = 0;
                private ulong m_InCoin = 0;
                private ulong m_OutCoin = 0;

                // =======================================================
                // プロパティ
                // =======================================================
                public ulong BigBonus
                {
                        get { return m_BigBonus; }
                        set { m_BigBonus = value; }
                }
                public ulong RegularBonus
                {
                        get { return m_RegularBonus; }
                        set { m_RegularBonus = value; }
                }
                public ulong AllGame
                {
                        get { return m_AllGame; }
                        set { m_AllGame = value; }
                }
                public ulong CurrentGame
                {
                        get { return m_CurrentGame; }
                        set { m_CurrentGame = value; }
                }
                public ulong InCoin
                {
                        get { return m_InCoin; }
                        set { m_InCoin = value; }
                }
                public ulong OutCoin
                {
                        get { return m_OutCoin; }
                        set { m_OutCoin = value; }
                }

                /// <summary>
                /// コンストラクタ
                /// </summary>
                public DataManager( IRegionManager p_RegionManager )
                {
                        this.m_RegionManager = p_RegionManager;
                }

                public void Convert( string p_ReceivedData )
                {

                }

                public void UpdateCounters( )
                {
                        update_counter( "BigBonusCounter", m_BigBonus );
                        update_counter( "RegularBonusCounter", m_RegularBonus );
                        update_counter( "AllGameCounter", m_AllGame );
                        update_counter( "CurrentGameCounter", m_CurrentGame );
                        update_counter( "InCoinCounter", m_InCoin );
                        update_counter( "OutCoinCounter", m_OutCoin );
                }

                private void update_counter( string p_CounterName, ulong p_Property )
                {
                        var l_ContentRegion = m_RegionManager.Regions[ p_CounterName ];
                        var l_ContentView = l_ContentRegion.Views.FirstOrDefault ( ) as Counter;
                        var l_ContentViewModel = l_ContentView.DataContext as CounterViewModel;
                        l_ContentViewModel.SetNumber( p_Property );
                }
        }
}
