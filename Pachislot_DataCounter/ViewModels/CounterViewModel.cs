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
                // =======================================================
                // プロパティのメンバ変数
                // =======================================================
                private int m_SixthDigit;
                private int m_FifthDigit;
                private int m_ForthDigit;
                private int m_ThirdDigit;
                private int m_SecondDigit;
                private int m_FirstDigit;

                /// <summary>
                /// 6桁目の数値
                /// </summary>
                public int SixthDigit
                {
                        get { return m_SixthDigit; }
                        set { SetProperty( ref m_SixthDigit, value ); }
                }

                /// <summary>
                /// 5桁目の数値
                /// </summary>
                public int FifthDigit
                {
                        get { return m_FifthDigit; }
                        set { SetProperty( ref m_FifthDigit, value ); }
                }

                /// <summary>
                /// 4桁目の数値
                /// </summary>
                public int ForthDigit
                {
                        get { return m_ForthDigit; }
                        set { SetProperty( ref m_ForthDigit, value ); }
                }

                /// <summary>
                /// 3桁目の数値
                /// </summary>
                public int ThirdDigit
                {
                        get { return m_ThirdDigit; }
                        set { SetProperty( ref m_ThirdDigit, value ); }
                }

                /// <summary>
                /// 2桁目の数値
                /// </summary>
                public int SecondDigit
                {
                        get { return m_SecondDigit; }
                        set { SetProperty( ref m_SecondDigit, value ); }
                }

                /// <summary>
                /// 1桁目の数値
                /// </summary>
                public int FirstDigit
                {
                        get { return m_FirstDigit; }
                        set { SetProperty( ref m_FirstDigit, value ); }
                }

                /// <summary>
                /// Counterのビューモデルのコンストラクタ
                /// </summary>
                public CounterViewModel( )
                {
                        SixthDigit = -1;
                        FifthDigit = -1;
                        ForthDigit = -1;
                        ThirdDigit = -1;
                        SecondDigit = -1;
                        FirstDigit = 0;
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
                                SixthDigit = -1;
                                FifthDigit = -1;
                                ForthDigit = -1;
                                ThirdDigit = -1;
                                SecondDigit = -1;
                                FirstDigit = -1;
                        }
                        else if ( p_Number >= 0 && p_Number < 10 )
                        {
                                SixthDigit = -1;
                                FifthDigit = -1;
                                ForthDigit = -1;
                                ThirdDigit = -1;
                                SecondDigit = -1;
                                FirstDigit = ( int )p_Number;
                        }
                        else if ( p_Number >= 10 && p_Number < 100 )
                        {
                                SixthDigit = -1;
                                FifthDigit = -1;
                                ForthDigit = -1;
                                ThirdDigit = -1;
                                SecondDigit = ( int )p_Number / 10;
                                l_Temp = p_Number % 10;
                                FirstDigit = ( int )l_Temp;
                        }
                        else if ( p_Number >= 100 && p_Number < 1000 )
                        {
                                SixthDigit = -1;
                                FifthDigit = -1;
                                ForthDigit = -1;
                                ThirdDigit = ( int )p_Number / 100;
                                l_Temp = p_Number % 100;
                                SecondDigit = ( int )l_Temp / 10;
                                l_Temp = p_Number % 10;
                                FirstDigit = ( int )l_Temp;
                        }
                        else if ( p_Number >= 1000 && p_Number < 10000 )
                        {
                                SixthDigit = -1;
                                FifthDigit = -1;
                                ForthDigit = ( int )p_Number / 1000;
                                l_Temp = p_Number % 1000;
                                ThirdDigit = ( int )l_Temp / 100;
                                l_Temp = p_Number % 100;
                                SecondDigit = ( int )l_Temp / 10;
                                l_Temp = p_Number % 10;
                                FirstDigit = ( int )l_Temp;
                        }
                        else if ( p_Number >= 10000 && p_Number < 100000 )
                        {
                                SixthDigit = -1;
                                FifthDigit = ( int )p_Number / 10000;
                                l_Temp = p_Number % 10000;
                                ForthDigit = ( int )l_Temp / 1000;
                                l_Temp = p_Number % 1000;
                                ThirdDigit = ( int )l_Temp / 100;
                                l_Temp = p_Number % 100;
                                SecondDigit = ( int )l_Temp / 10;
                                l_Temp = p_Number % 10;
                                FirstDigit = ( int )l_Temp;
                        }
                        else if ( p_Number >= 100000 && p_Number < 1000000 )
                        {
                                SixthDigit = ( int )p_Number / 100000;
                                l_Temp = p_Number % 100000;
                                FifthDigit = ( int )l_Temp / 10000;
                                l_Temp = p_Number % 10000;
                                ForthDigit = ( int )l_Temp / 1000;
                                l_Temp = p_Number % 1000;
                                ThirdDigit = ( int )l_Temp / 100;
                                l_Temp = p_Number % 100;
                                SecondDigit = ( int )l_Temp / 10;
                                l_Temp = p_Number % 10;
                                FirstDigit = ( int )l_Temp;
                        }
                        else
                        {
                                SixthDigit = ( int )9;
                                FirstDigit = ( int )9;
                                SecondDigit = ( int )9;
                                ThirdDigit = ( int )9;
                                ForthDigit = ( int )9;
                                FifthDigit = ( int )9;
                        }
                }

                /// <summary>
                /// 数字画像のパスを指定するとBitmapImageクラスのインスタンスにして返す
                /// </summary>
                /// <param name="p_FilePath">数字画像のパス</param>
                /// <returns>数字画像のBitmapImage</returns>
                //private BitmapImage create_bitmap_image( string p_FilePath )
                //{
                //        BitmapImage l_Img = new BitmapImage( );

                //        try
                //        {
                //                l_Img.BeginInit( );
                //                l_Img.CacheOption = BitmapCacheOption.OnLoad;
                //                l_Img.UriSource = new Uri( p_FilePath, UriKind.Absolute );
                //                l_Img.EndInit( );
                //                l_Img.Freeze( );
                //        }
                //        catch ( Exception e )
                //        {
                //                Debug.WriteLine( e.Message );
                //        }

                //        return l_Img;
                //}
        }
}
