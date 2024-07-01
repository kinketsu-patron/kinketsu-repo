/**
 * =============================================================
 * File         :CounterViewModel.cs
 * Summary      :Counterコントロールのビューモデル
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/21
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Pachislot_DataCounter.ViewModels
{
        public class CounterViewModel : BindableBase
        {
                /// <summary>
                /// 数字と数字画像の1対1の対応付けを行うディクショナリ
                /// </summary>
                private Dictionary<uint, BitmapImage> m_NumDictionary;

                // =======================================================
                // プロパティのメンバ変数
                // =======================================================
                private BitmapImage m_SixthDigit;
                private BitmapImage m_FifthDigit;
                private BitmapImage m_ForthDigit;
                private BitmapImage m_ThirdDigit;
                private BitmapImage m_SecondDigit;
                private BitmapImage m_FirstDigit;

                /// <summary>
                /// 6桁目の数値
                /// </summary>
                public BitmapImage SixthDigit
                {
                        get { return m_SixthDigit; }
                        set { SetProperty( ref m_SixthDigit, value ); }
                }

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
                /// Counterのビューモデルのコンストラクタ
                /// </summary>
                public CounterViewModel( )
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
                        SixthDigit = null;
                        FifthDigit = null;
                        ForthDigit = null;
                        ThirdDigit = null;
                        SecondDigit = null;
                        FirstDigit = m_NumDictionary[ 0 ];
                }

                /// <summary>
                /// 整数型の数値を設定すると適切に数値画像を選択して表示してくれる
                /// </summary>
                /// <param name="p_Number">カウント値</param>
                public void SetNumber( uint p_Number )
                {
                        uint l_Temp;

                        if ( p_Number < 0 )
                        {
                                SixthDigit = null;
                                FifthDigit = null;
                                ForthDigit = null;
                                ThirdDigit = null;
                                SecondDigit = null;
                                FirstDigit = null;
                        }
                        else if ( p_Number >= 0 && p_Number < 10 )
                        {
                                SixthDigit = null;
                                FifthDigit = null;
                                ForthDigit = null;
                                ThirdDigit = null;
                                SecondDigit = null;
                                FirstDigit = m_NumDictionary[ p_Number ];
                        }
                        else if ( p_Number >= 10 && p_Number < 100 )
                        {
                                SixthDigit = null;
                                FifthDigit = null;
                                ForthDigit = null;
                                ThirdDigit = null;
                                SecondDigit = m_NumDictionary[ p_Number / 10 ];
                                l_Temp = p_Number % 10;
                                FirstDigit = m_NumDictionary[ l_Temp ];
                        }
                        else if ( p_Number >= 100 && p_Number < 1000 )
                        {
                                SixthDigit = null;
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
                                SixthDigit = null;
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
                                SixthDigit = null;
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
                        else if ( p_Number >= 100000 && p_Number < 1000000 )
                        {
                                SixthDigit = m_NumDictionary[ p_Number / 100000 ];
                                l_Temp = p_Number % 100000;
                                FifthDigit = m_NumDictionary[ l_Temp / 10000 ];
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
                                SixthDigit = m_NumDictionary[ 9 ];
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
                        catch ( Exception e )
                        {
                                Debug.WriteLine( e.Message );
                        }

                        return l_Img;
                }
        }
}
