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
using Prism.Mvvm;

namespace Pachislot_DataCounter.Models
{
        public class DataManager : BindableBase
        {
                #region メンバ変数
                // =======================================================
                // メンバ変数
                // =======================================================
                private uint m_BigBonus;
                private uint m_RegularBonus;
                private uint m_AllGame;
                private uint m_CurrentGame;
                private uint m_InCoin;
                private uint m_OutCoin;
                private int m_Diff;
                private bool m_DuringRB;
                private bool m_DuringBB;
                private bool m_DuringBonus;
                #endregion

                #region プロパティ
                // =======================================================
                // プロパティ
                // =======================================================
                /// <summary>
                /// ビッグボーナス回数
                /// </summary>
                public uint BigBonus
                {
                        get { return m_BigBonus; }
                        set { SetProperty( ref m_BigBonus, value ); }
                }
                /// <summary>
                /// レギュラーボーナス回数
                /// </summary>
                public uint RegularBonus
                {
                        get { return m_RegularBonus; }
                        set { SetProperty( ref m_RegularBonus, value ); }
                }
                /// <summary>
                /// 累計ゲーム数
                /// </summary>
                public uint AllGame
                {
                        get { return m_AllGame; }
                        set { SetProperty( ref m_AllGame, value ); }
                }
                /// <summary>
                /// 現在のゲーム数
                /// </summary>
                public uint CurrentGame
                {
                        get { return m_CurrentGame; }
                        set { SetProperty( ref m_CurrentGame, value ); }
                }
                /// <summary>
                /// IN枚数
                /// </summary>
                public uint InCoin
                {
                        get { return m_InCoin; }
                        set { SetProperty( ref m_InCoin, value ); }
                }
                /// <summary>
                /// OUT枚数
                /// </summary>
                public uint OutCoin
                {
                        get { return m_OutCoin; }
                        set { SetProperty( ref m_OutCoin, value ); }
                }
                /// <summary>
                /// 差枚数
                /// </summary>
                public int Diff
                {
                        get { return m_Diff; }
                        set { SetProperty( ref m_Diff, value ); }
                }
                /// <summary>
                /// レギュラーボーナス中フラグ
                /// </summary>
                public bool DuringRB
                {
                        get { return m_DuringRB; }
                        set { SetProperty( ref m_DuringRB, value ); }
                }
                /// <summary>
                /// ビッグボーナス中フラグ
                /// </summary>
                public bool DuringBB
                {
                        get { return m_DuringBB; }
                        set { SetProperty( ref m_DuringBB, value ); }
                }
                /// <summary>
                /// ボーナス中フラグ
                /// </summary>
                public bool DuringBonus
                {
                        get { return m_DuringBonus; }
                        set { SetProperty( ref m_DuringBonus, value ); }
                }
                #endregion

                #region 公開メソッド
                /// <summary>
                /// コンストラクタ
                /// </summary>
                public DataManager( )
                {
                        BigBonus = 0;
                        RegularBonus = 0;
                        AllGame = 0;
                        CurrentGame = 0;
                        InCoin = 0;
                        OutCoin = 0;
                        Diff = 0;
                        DuringRB = false;
                        DuringBB = false;
                        DuringBonus = false;
                }

                /// <summary>
                /// ゲーム情報を保存する
                /// </summary>
                /// <param name="p_GameInfo">ゲーム情報</param>
                public void Store( GameInfo p_GameInfo )
                {
                        DuringRB = p_GameInfo.DuringRB;
                        DuringBB = p_GameInfo.DuringBB;

                        if ( DuringBB || DuringRB )
                        {
                                DuringBonus = true;
                        }
                        else
                        {
                                DuringBonus = false;
                        }

                        CurrentGame = p_GameInfo.Game;
                        AllGame = p_GameInfo.TotalGame;
                        InCoin = p_GameInfo.In;
                        OutCoin = p_GameInfo.Out;
                        Diff = p_GameInfo.Diff;
                        RegularBonus = p_GameInfo.RB;
                        BigBonus = p_GameInfo.BB;
                }
                #endregion
        }
}
