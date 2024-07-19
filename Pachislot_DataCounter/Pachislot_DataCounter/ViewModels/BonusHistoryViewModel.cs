/**
 * =============================================================
 * File         :BonusHistoryViewModel.cs
 * Summary      :BonusHistoryViewModelのビューモデル
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/07/15
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Pachislot_DataCounter.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace Pachislot_DataCounter.ViewModels
{
        public class BonusHistoryViewModel : BindableBase
        {
                #region メンバ変数
                // =======================================================
                // メンバ変数
                // =======================================================
                private DataManager m_DataManager;
                protected CompositeDisposable m_Disposables;
                #endregion

                #region プロパティ
                // =======================================================
                // プロパティ
                // =======================================================
                /// <summary>
                /// 現在のゲーム数に合わせたバー表示リスト
                /// </summary>
                public ObservableCollection<BonusHistoryVisible> CurrentGameBar { get; set; }
                /// <summary>
                /// 1回前から10回前のボーナス履歴のバー表示リスト
                /// </summary>
                public ObservableCollection<BonusHistoryVisible> BonusHistory { get; set; }
                /// <summary>
                /// 現在のゲーム数
                /// </summary>
                public ReactiveProperty<uint> CurrentGame { get; }
                /// <summary>
                /// レギュラーボーナス中フラグ
                /// </summary>
                public ReactiveProperty<bool> DuringRB { get; }
                /// <summary>
                /// ビッグボーナス中フラグ
                /// </summary>
                public ReactiveProperty<bool> DuringBB { get; }
                #endregion

                #region 公開メソッド
                /// <summary>
                /// コンストラクタ
                /// </summary>
                public BonusHistoryViewModel( DataManager p_DataManager )
                {
                        m_DataManager = p_DataManager;
                        m_Disposables = new CompositeDisposable( );

                        CurrentGameBar = new ObservableCollection<BonusHistoryVisible>( );
                        CurrentGameBar.Add( new BonusHistoryVisible
                        {
                                Bar_Ten = Visibility.Hidden,
                                Bar_Nine = Visibility.Hidden,
                                Bar_Eight = Visibility.Hidden,
                                Bar_Seven = Visibility.Hidden,
                                Bar_Six = Visibility.Hidden,
                                Bar_Five = Visibility.Hidden,
                                Bar_Four = Visibility.Hidden,
                                Bar_Three = Visibility.Hidden,
                                Bar_Two = Visibility.Hidden,
                                Bar_One = Visibility.Visible,
                                TimesAgo = "現在",
                                KindOfBonus = "NONE",
                                Games = 0
                        } );

                        BonusHistory = new ObservableCollection<BonusHistoryVisible>( );
                        for ( int i = 0; i < 10; i++ )
                        {
                                BonusHistory.Add( new BonusHistoryVisible
                                {
                                        Bar_Ten = Visibility.Hidden,
                                        Bar_Nine = Visibility.Hidden,
                                        Bar_Eight = Visibility.Hidden,
                                        Bar_Seven = Visibility.Hidden,
                                        Bar_Six = Visibility.Hidden,
                                        Bar_Five = Visibility.Hidden,
                                        Bar_Four = Visibility.Hidden,
                                        Bar_Three = Visibility.Hidden,
                                        Bar_Two = Visibility.Hidden,
                                        Bar_One = Visibility.Visible,
                                        TimesAgo = ( i + 1 ) + "回前",
                                        KindOfBonus = "NONE",
                                        Games = 0
                                } );
                        }

                        CurrentGame = m_DataManager.ToReactivePropertyAsSynchronized( m => m.CurrentGame ).AddTo( m_Disposables );
                        CurrentGame.Subscribe( _ => UpdateGames( ) );
                        DuringRB = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringRB ).AddTo( m_Disposables );
                        DuringRB.Skip( 1 ).Subscribe( isbonus =>
                        {
                                if ( isbonus == false )
                                {
                                        UpdateBonusHistory( "REGULAR_BONUS" );
                                }
                        } );
                        DuringBB = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringBB ).AddTo( m_Disposables );
                        DuringBB.Skip( 1 ).Subscribe( isbonus =>
                        {
                                if ( isbonus == false )
                                {
                                        UpdateBonusHistory( "BIG_BONUS" );
                                }
                        } );
                }
                #endregion

                #region 非公開メソッド
                /// <summary>
                /// ゲーム数が更新されたときに現在ゲーム数のバー表示も更新する
                /// </summary>
                private void UpdateGames( )
                {
                        CurrentGameBar[ 0 ].Games = CurrentGame.Value;

                        if ( CurrentGame.Value < 100 )
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                        else if ( 100 <= CurrentGame.Value && CurrentGame.Value < 200 )
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                        else if ( 200 <= CurrentGame.Value && CurrentGame.Value < 300 )
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                        else if ( 300 <= CurrentGame.Value && CurrentGame.Value < 400 )
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                        else if ( 400 <= CurrentGame.Value && CurrentGame.Value < 500 )
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                        else if ( 500 <= CurrentGame.Value && CurrentGame.Value < 600 )
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                        else if ( 600 <= CurrentGame.Value && CurrentGame.Value < 700 )
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                        else if ( 700 <= CurrentGame.Value && CurrentGame.Value < 800 )
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                        else if ( 800 <= CurrentGame.Value && CurrentGame.Value < 900 )
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                        else
                        {
                                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
                        }
                }

                /// <summary>
                /// ボーナス終了時に呼び出されてBonusHistoryの中のBonusHistoryVisibleオブジェクトをシフトする
                /// </summary>
                private void UpdateBonusHistory( string p_Bonus )
                {
                        BonusHistory.Remove( BonusHistory.Last( ) );
                        switch ( p_Bonus )
                        {
                                case "REGULAR_BONUS":
                                        CurrentGameBar[ 0 ].KindOfBonus = "RB";
                                        break;
                                case "BIG_BONUS":
                                        CurrentGameBar[ 0 ].KindOfBonus = "BB";
                                        break;
                                default:
                                        CurrentGameBar[ 0 ].KindOfBonus = "NONE";
                                        break;
                        }

                        BonusHistory.Insert( 0, CurrentGameBar.First( ).Clone( ) );               // CurrentGameBarに入っているオブジェクトのディープコピーをBonusHisotyの先頭に追加する

                        for ( int i = 0; i < BonusHistory.Count; i++ )
                        {
                                BonusHistory[ i ].TimesAgo = ( i + 1 ).ToString( ) + "回前";      // X回前の表示を+1して過去のものにする
                        }
                }
                #endregion
        }
}
