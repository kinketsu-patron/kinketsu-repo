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
using Pachislot_DataCounter.ViewModels;
using Pachislot_DataCounter.Views;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pachislot_DataCounter.Models
{
        public class DataManager
        {
                // =======================================================
                // メンバ変数
                // =======================================================
                private readonly IRegionManager m_RegionManager;
                private uint m_BigBonus;
                private uint m_RegularBonus;
                private uint m_AllGame;
                private uint m_CurrentGame;
                private uint m_InCoin;
                private uint m_OutCoin;
                private int m_Diff;
                private bool m_DuringRB;
                private bool m_DuringBB;

                // =======================================================
                // プロパティ
                // =======================================================
                /// <summary>
                /// ビッグボーナス回数
                /// </summary>
                public uint BigBonus
                {
                        get { return m_BigBonus; }
                        set { m_BigBonus = value; }
                }
                /// <summary>
                /// レギュラーボーナス回数
                /// </summary>
                public uint RegularBonus
                {
                        get { return m_RegularBonus; }
                        set { m_RegularBonus = value; }
                }
                /// <summary>
                /// 累計ゲーム数
                /// </summary>
                public uint AllGame
                {
                        get { return m_AllGame; }
                        set { m_AllGame = value; }
                }
                /// <summary>
                /// 現在のゲーム数
                /// </summary>
                public uint CurrentGame
                {
                        get { return m_CurrentGame; }
                        set { m_CurrentGame = value; }
                }
                /// <summary>
                /// IN枚数
                /// </summary>
                public uint InCoin
                {
                        get { return m_InCoin; }
                        set { m_InCoin = value; }
                }
                /// <summary>
                /// OUT枚数
                /// </summary>
                public uint OutCoin
                {
                        get { return m_OutCoin; }
                        set { m_OutCoin = value; }
                }
                /// <summary>
                /// 差枚数
                /// </summary>
                public int Diff
                {
                        get { return m_Diff; }
                        set { m_Diff = value; }
                }
                /// <summary>
                /// レギュラーボーナス中フラグ
                /// </summary>
                public bool DuringRB
                {
                        get { return m_DuringRB; }
                        set { m_DuringRB = value; }
                }
                /// <summary>
                /// ビッグボーナス中フラグ
                /// </summary>
                public bool DuringBB
                {
                        get { return m_DuringBB; }
                        set { m_DuringBB = value; }
                }

                /// <summary>
                /// コンストラクタ
                /// </summary>
                public DataManager( IRegionManager p_RegionManager )
                {
                        m_RegionManager = p_RegionManager;
                        m_BigBonus = 0;
                        m_RegularBonus = 0;
                        m_AllGame = 0;
                        m_CurrentGame = 0;
                        m_InCoin = 0;
                        m_OutCoin = 0;
                        m_Diff = 0;
                        m_DuringRB = false;
                        m_DuringBB = false;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="p_ReceivedData">受信したJSONデータ</param>
                public void Convert( string p_ReceivedData )
                {
                        if ( String.IsNullOrEmpty( p_ReceivedData ) )
                        {
                                return;
                        }

                        try
                        {
                                GameInfo l_GameInfo = JsonSerializer.Deserialize<GameInfo>( p_ReceivedData );
                                m_CurrentGame = l_GameInfo.Game;
                                m_AllGame = l_GameInfo.TotalGame;
                                m_InCoin = l_GameInfo.In;
                                m_OutCoin = l_GameInfo.Out;
                                m_Diff = l_GameInfo.Diff;
                                m_RegularBonus = l_GameInfo.RB;
                                m_BigBonus = l_GameInfo.BB;
                                m_DuringRB = l_GameInfo.DuringRB;
                                m_DuringBB = l_GameInfo.DuringBB;
                        }
                        catch ( JsonException e )
                        {
                                Console.WriteLine( e.Message );
                                return;
                        }
                }

                /// <summary>
                /// カウンターのViewModelを更新する
                /// </summary>
                public void UpdateCounters( )
                {
                        update_counter( "BigBonusCounter", m_BigBonus );
                        update_counter( "RegularBonusCounter", m_RegularBonus );
                        update_counter( "AllGameCounter", m_AllGame );
                        update_counter( "CurrentGameCounter", m_CurrentGame );
                        update_counter( "InCoinCounter", m_InCoin );
                        update_counter( "OutCoinCounter", m_OutCoin );
                }

                /// <summary>
                /// カウンターを指定して、そのViewModelを呼び出し、数値を入れて表示を更新する
                /// </summary>
                /// <param name="p_CounterName"></param>
                /// <param name="p_Property"></param>
                private void update_counter( string p_CounterName, uint p_Property )
                {
                        var l_ContentRegion = m_RegionManager.Regions[ p_CounterName ];
                        var l_ContentView = l_ContentRegion.Views.FirstOrDefault ( ) as Counter;
                        var l_ContentViewModel = l_ContentView.DataContext as CounterViewModel;
                        l_ContentViewModel.SetNumber( p_Property );
                }
        }
}
