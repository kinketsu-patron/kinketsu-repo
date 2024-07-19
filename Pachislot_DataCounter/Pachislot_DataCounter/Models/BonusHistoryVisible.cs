/**
 * =============================================================
 * File         :BonusHistoryVisible.cs
 * Summary      :ボーナス履歴表示クラス
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/07/19
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Prism.Mvvm;
using System.Windows;

namespace Pachislot_DataCounter.Models
{
        public class BonusHistoryVisible : BindableBase
        {
                #region メンバ変数
                // =======================================================
                // メンバ変数
                // =======================================================
                private Visibility m_Bar_One;
                private Visibility m_Bar_Two;
                private Visibility m_Bar_Three;
                private Visibility m_Bar_Four;
                private Visibility m_Bar_Five;
                private Visibility m_Bar_Six;
                private Visibility m_Bar_Seven;
                private Visibility m_Bar_Eight;
                private Visibility m_Bar_Nine;
                private Visibility m_Bar_Ten;
                private string m_TimesAgo;
                private string m_KindOfBonus;
                private uint m_Games;
                #endregion

                #region プロパティ
                public Visibility Bar_Ten
                {
                        get { return m_Bar_Ten; }
                        set { SetProperty( ref m_Bar_Ten, value ); }
                }

                public Visibility Bar_Nine
                {
                        get { return m_Bar_Nine; }
                        set { SetProperty( ref m_Bar_Nine, value ); }
                }

                public Visibility Bar_Eight
                {
                        get { return m_Bar_Eight; }
                        set { SetProperty( ref m_Bar_Eight, value ); }
                }

                public Visibility Bar_Seven
                {
                        get { return m_Bar_Seven; }
                        set { SetProperty( ref m_Bar_Seven, value ); }
                }

                public Visibility Bar_Six
                {
                        get { return m_Bar_Six; }
                        set { SetProperty( ref m_Bar_Six, value ); }
                }

                public Visibility Bar_Five
                {
                        get { return m_Bar_Five; }
                        set { SetProperty( ref m_Bar_Five, value ); }
                }

                public Visibility Bar_Four
                {
                        get { return m_Bar_Four; }
                        set { SetProperty( ref m_Bar_Four, value ); }
                }

                public Visibility Bar_Three
                {
                        get { return m_Bar_Three; }
                        set { SetProperty( ref m_Bar_Three, value ); }
                }

                public Visibility Bar_Two
                {
                        get { return m_Bar_Two; }
                        set { SetProperty( ref m_Bar_Two, value ); }
                }

                public Visibility Bar_One
                {
                        get { return m_Bar_One; }
                        set { SetProperty( ref m_Bar_One, value ); }
                }

                public string TimesAgo
                {
                        get { return m_TimesAgo; }
                        set { SetProperty( ref m_TimesAgo, value ); }
                }

                public string KindOfBonus
                {
                        get { return m_KindOfBonus; }
                        set { SetProperty( ref m_KindOfBonus, value ); }
                }

                public uint Games
                {
                        get { return m_Games; }
                        set { SetProperty( ref m_Games, value ); }
                }
                #endregion

                #region 公開メソッド
                /// <summary>
                /// クローンインスタンスを生成する
                /// </summary>
                /// <returns>BonusHistoryVisibleのクローンインスタンス</returns>
                public BonusHistoryVisible Clone( )
                {
                        return ( BonusHistoryVisible )MemberwiseClone( );
                }
                #endregion
        }
}
