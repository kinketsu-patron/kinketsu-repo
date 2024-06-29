/**
 * =============================================================
 * File         :DataManager.cs
 * Summary      :データ管理クラス
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/29
 * =============================================================
 */

// =======================================================
                // using
                // =======================================================
﻿using Pachislot_DataCounter.ViewModels;
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
                private readonly IRegionManager m_RegionManager;
                private ulong m_BigBonus;
                private ulong m_RegularBonus;
                private ulong m_AllGame;
                private ulong m_CurrentGame;
                private ulong m_InCoin;
                private ulong m_OutCoin;

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
                        m_RegionManager = p_RegionManager;
                        m_BigBonus      = 0;
                        m_RegularBonus  = 0;
                        m_AllGame       = 0;
                        m_CurrentGame   = 0;
                        m_InCoin        = 0;
                        m_OutCoin       = 0;
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
