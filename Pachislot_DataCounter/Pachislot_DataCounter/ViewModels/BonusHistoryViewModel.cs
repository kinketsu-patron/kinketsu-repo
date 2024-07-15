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
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Pachislot_DataCounter.ViewModels
{
        public class BonusHistoryViewModel : BindableBase
        {
                // =======================================================
                // メンバ変数
                // =======================================================
                private Dictionary<uint, Visibility> m_HistoryDictionary;
                private List<Dictionary<uint, Visibility>> m_HistoryList;
                private DataManager m_DataManager;
                private Visibility m_Bar_CurTen;
                private Visibility m_Bar_CurNine;
                private Visibility m_Bar_CurEight;
                private Visibility m_Bar_CurSeven;
                private Visibility m_Bar_CurSix;
                private Visibility m_Bar_CurFive;
                private Visibility m_Bar_CurFour;
                private Visibility m_Bar_CurThree;
                private Visibility m_Bar_CurTwo;
                private Visibility m_Bar_CurOne;
                private string m_CurrentGameCount;
                protected CompositeDisposable m_Disposables;

                // =======================================================
                // プロパティ
                // =======================================================
                public Visibility Bar_CurTen
                {
                        get { return m_Bar_CurTen; }
                        set { SetProperty( ref m_Bar_CurTen, value ); }
                }
                public Visibility Bar_CurNine
                {
                        get { return m_Bar_CurNine; }
                        set { SetProperty( ref m_Bar_CurNine, value ); }
                }
                public Visibility Bar_CurEight
                {
                        get { return m_Bar_CurEight; }
                        set { SetProperty( ref m_Bar_CurEight, value ); }
                }
                public Visibility Bar_CurSeven
                {
                        get { return m_Bar_CurSeven; }
                        set { SetProperty( ref m_Bar_CurSeven, value ); }
                }
                public Visibility Bar_CurSix
                {
                        get { return m_Bar_CurSix; }
                        set { SetProperty( ref m_Bar_CurSix, value ); }
                }
                public Visibility Bar_CurFive
                {
                        get { return m_Bar_CurFive; }
                        set { SetProperty( ref m_Bar_CurFive, value ); }
                }
                public Visibility Bar_CurFour
                {
                        get { return m_Bar_CurFour; }
                        set { SetProperty( ref m_Bar_CurFour, value ); }
                }
                public Visibility Bar_CurThree
                {
                        get { return m_Bar_CurThree; }
                        set { SetProperty( ref m_Bar_CurThree, value ); }
                }
                public Visibility Bar_CurTwo
                {
                        get { return m_Bar_CurTwo; }
                        set { SetProperty( ref m_Bar_CurTwo, value ); }
                }
                public Visibility Bar_CurOne
                {
                        get { return m_Bar_CurOne; }
                        set { SetProperty( ref m_Bar_CurOne, value ); }
                }

                /// <summary>
                /// 現在のゲーム数
                /// </summary>
                public ReactiveProperty<uint> CurrentGame { get; }

                /// <summary>
                /// コンストラクタ
                /// </summary>
                public BonusHistoryViewModel( DataManager p_DataManager )
                {
                        m_DataManager = p_DataManager;
                        m_Disposables = new CompositeDisposable( );
                        CurrentGame = m_DataManager.ToReactivePropertyAsSynchronized( m => m.CurrentGame ).AddTo( m_Disposables );
                        CurrentGame.Subscribe( _ => Convert( ) );

                        for ( int i = 0; i < 11; i++ )
                        {
                                m_HistoryDictionary = new Dictionary<uint, Visibility>
                                {
                                        { 0, Visibility.Visible },
                                        { 1, Visibility.Hidden },
                                        { 2, Visibility.Hidden },
                                        { 3, Visibility.Hidden },
                                        { 4, Visibility.Hidden },
                                        { 5, Visibility.Hidden },
                                        { 6, Visibility.Hidden },
                                        { 7, Visibility.Hidden },
                                        { 8, Visibility.Hidden },
                                        { 9, Visibility.Hidden },
                                        { 10, Visibility.Hidden }
                                };
                                m_HistoryList.Add( m_HistoryDictionary );
                        }
                }

                private void Convert( )
                {
                        if ( CurrentGame.Value < 100 )
                        {
                                Bar_CurThree = Visibility.Hidden;
                                Bar_CurTwo = Visibility.Hidden;
                                Bar_CurOne = Visibility.Visible;
                        }
                }
        }
}
