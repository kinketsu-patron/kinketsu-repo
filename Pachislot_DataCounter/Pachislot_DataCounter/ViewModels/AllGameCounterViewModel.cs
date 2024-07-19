/**
 * =============================================================
 * File         :AllGameCounterViewModel.cs
 * Summary      :AllGameCounterViewModelのビューモデル
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/07/05
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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Pachislot_DataCounter.ViewModels
{
        public class AllGameCounterViewModel : BindableBase
        {
                #region メンバ変数
                // =======================================================
                // メンバ変数
                // =======================================================
                /// <summary>
                /// 数字と数字画像の1対1の対応付けを行うディクショナリ
                /// </summary>
                private Dictionary<uint, BitmapImage> m_NumDictionary;
                private DataManager m_DataManager;
                protected CompositeDisposable m_Disposables;
                private BitmapImage m_FifthDigit;
                private BitmapImage m_ForthDigit;
                private BitmapImage m_ThirdDigit;
                private BitmapImage m_SecondDigit;
                private BitmapImage m_FirstDigit;
                #endregion

                #region プロパティ
                // =======================================================
                // プロパティ
                // =======================================================
                /// <summary>
                /// 5桁目の数値
                /// </summary>
                public BitmapImage FifthDigit
                {
                        get { return m_FifthDigit; }
                        set { SetProperty( ref m_FifthDigit, value ); }
                }
                /// <summary>
                /// 4桁目の数値
                /// </summary>
                public BitmapImage ForthDigit
                {
                        get { return m_ForthDigit; }
                        set { SetProperty( ref m_ForthDigit, value ); }
                }
                /// <summary>
                /// 3桁目の数値
                /// </summary>
                public BitmapImage ThirdDigit
                {
                        get { return m_ThirdDigit; }
                        set { SetProperty( ref m_ThirdDigit, value ); }
                }
                /// <summary>
                /// 2桁目の数値
                /// </summary>
                public BitmapImage SecondDigit
                {
                        get { return m_SecondDigit; }
                        set { SetProperty( ref m_SecondDigit, value ); }
                }
                /// <summary>
                /// 1桁目の数値
                /// </summary>
                public BitmapImage FirstDigit
                {
                        get { return m_FirstDigit; }
                        set { SetProperty( ref m_FirstDigit, value ); }
                }
                /// <summary>
                /// 累計ゲーム数
                /// </summary>
                public ReactiveProperty<uint> AllGame { get; }
                #endregion

                #region 公開メソッド
                /// <summary>
                /// コンストラクタ
                /// </summary>
                public AllGameCounterViewModel( DataManager p_DataManager )
                {
                        m_NumDictionary = new Dictionary<uint, BitmapImage>
                        {
                                { 0, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(0).png" ) },
                                { 1, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(1).png" ) },
                                { 2, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(2).png" ) },
                                { 3, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(3).png" ) },
                                { 4, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(4).png" ) },
                                { 5, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(5).png" ) },
                                { 6, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(6).png" ) },
                                { 7, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(7).png" ) },
                                { 8, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(8).png" ) },
                                { 9, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(9).png" ) }
                        };

                        m_DataManager = p_DataManager;
                        m_Disposables = new CompositeDisposable( );
                        AllGame = m_DataManager.ToReactivePropertyAsSynchronized( m => m.AllGame ).AddTo( m_Disposables );
                        AllGame.Subscribe( allgame => set_number( allgame ) );

                        FifthDigit = null;
                        ForthDigit = null;
                        ThirdDigit = null;
                        SecondDigit = null;
                        FirstDigit = m_NumDictionary[ 0 ];
                }
                #endregion

                #region 非公開メソッド
                /// <summary>
                /// 整数型の数値を設定すると適切に数値画像を選択して表示してくれる
                /// </summary>
                /// <param name="p_Number">カウント値</param>
                private void set_number( uint p_Number )
                {
                        uint l_Temp;

                        if ( p_Number < 0 )
                        {
                                FifthDigit = null;
                                ForthDigit = null;
                                ThirdDigit = null;
                                SecondDigit = null;
                                FirstDigit = null;
                        }
                        else if ( p_Number >= 0 && p_Number < 10 )
                        {
                                FifthDigit = null;
                                ForthDigit = null;
                                ThirdDigit = null;
                                SecondDigit = null;
                                FirstDigit = m_NumDictionary[ p_Number ];
                        }
                        else if ( p_Number >= 10 && p_Number < 100 )
                        {
                                FifthDigit = null;
                                ForthDigit = null;
                                ThirdDigit = null;
                                SecondDigit = m_NumDictionary[ p_Number / 10 ];
                                l_Temp = p_Number % 10;
                                FirstDigit = m_NumDictionary[ l_Temp ];
                        }
                        else if ( p_Number >= 100 && p_Number < 1000 )
                        {
                                FifthDigit = null;
                                ForthDigit = null;
                                ThirdDigit = m_NumDictionary[ p_Number / 100 ];
                                l_Temp = p_Number % 100;
                                SecondDigit = m_NumDictionary[ l_Temp / 10 ];
                                l_Temp = p_Number % 10;
                                FirstDigit = m_NumDictionary[ l_Temp ];
                        }
                        else if ( p_Number >= 1000 && p_Number < 10000 )
                        {
                                FifthDigit = null;
                                ForthDigit = m_NumDictionary[ p_Number / 1000 ];
                                l_Temp = p_Number % 1000;
                                ThirdDigit = m_NumDictionary[ l_Temp / 100 ];
                                l_Temp = p_Number % 100;
                                SecondDigit = m_NumDictionary[ l_Temp / 10 ];
                                l_Temp = p_Number % 10;
                                FirstDigit = m_NumDictionary[ l_Temp ];
                        }
                        else if ( p_Number >= 10000 && p_Number < 100000 )
                        {
                                FifthDigit = m_NumDictionary[ p_Number / 10000 ];
                                l_Temp = p_Number % 10000;
                                ForthDigit = m_NumDictionary[ l_Temp / 1000 ];
                                l_Temp = p_Number % 1000;
                                ThirdDigit = m_NumDictionary[ l_Temp / 100 ];
                                l_Temp = p_Number % 100;
                                SecondDigit = m_NumDictionary[ l_Temp / 10 ];
                                l_Temp = p_Number % 10;
                                FirstDigit = m_NumDictionary[ l_Temp ];
                        }
                        else
                        {
                                FirstDigit = m_NumDictionary[ 9 ];
                                SecondDigit = m_NumDictionary[ 9 ];
                                ThirdDigit = m_NumDictionary[ 9 ];
                                ForthDigit = m_NumDictionary[ 9 ];
                                FifthDigit = m_NumDictionary[ 9 ];
                        }
                }

                /// <summary>
                /// 数字画像のパスを指定するとBitmapImageクラスのインスタンスにして返す
                /// </summary>
                /// <param name="p_FilePath">数字画像のパス</param>
                /// <returns>数字画像のBitmapImage</returns>
                private BitmapImage create_bitmap_image( string p_FilePath )
                {
                        BitmapImage l_Img = new BitmapImage( );

                        try
                        {
                                l_Img.BeginInit( );
                                l_Img.CacheOption = BitmapCacheOption.OnLoad;
                                l_Img.UriSource = new Uri( p_FilePath, UriKind.Absolute );
                                l_Img.EndInit( );
                                l_Img.Freeze( );
                        }
                        catch ( Exception ex )
                        {
                                MessageBox.Show( ex.Message );
                        }

                        return l_Img;
                }
                #endregion
        }
}
