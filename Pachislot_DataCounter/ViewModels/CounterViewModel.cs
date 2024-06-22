﻿/**
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
using System.Linq;
using System.Windows.Media.Imaging;

namespace Pachislot_DataCounter.ViewModels
{
    public class CounterViewModel : BindableBase
    {
        /// <summary>
        /// 数字と数字画像の1対1の対応付けを行うディクショナリ
        /// </summary>
        private Dictionary<ulong, string> _NumDictionary = new Dictionary<ulong, string>
        {
            { 0, "pack://application:,,,/Resource/数字/数字(0).png" },
            { 1, "pack://application:,,,/Resource/数字/数字(1).png" },
            { 2, "pack://application:,,,/Resource/数字/数字(2).png" },
            { 3, "pack://application:,,,/Resource/数字/数字(3).png" },
            { 4, "pack://application:,,,/Resource/数字/数字(4).png" },
            { 5, "pack://application:,,,/Resource/数字/数字(5).png" },
            { 6, "pack://application:,,,/Resource/数字/数字(6).png" },
            { 7, "pack://application:,,,/Resource/数字/数字(7).png" },
            { 8, "pack://application:,,,/Resource/数字/数字(8).png" },
            { 9, "pack://application:,,,/Resource/数字/数字(9).png" }
        };

        // =======================================================
        // プロパティのメンバ変数
        // =======================================================
        private BitmapImage _SixthDigit;
        private BitmapImage _FifthDigit;
        private BitmapImage _ForthDigit;
        private BitmapImage _ThirdDigit;
        private BitmapImage _SecondDigit;
        private BitmapImage _FirstDigit;

        /// <summary>
        /// 6桁目の数値
        /// </summary>
        public BitmapImage SixthDigit
        {
            get { return _SixthDigit; }
            set { SetProperty ( ref _SixthDigit, value ); }
        }

        /// <summary>
        /// 5桁目の数値
        /// </summary>
        public BitmapImage FifthDigit
        {
            get { return _FifthDigit; }
            set { SetProperty ( ref _FifthDigit, value ); }
        }

        /// <summary>
        /// 4桁目の数値
        /// </summary>
        public BitmapImage ForthDigit
        {
            get { return _ForthDigit; }
            set { SetProperty ( ref _ForthDigit, value ); }
        }

        /// <summary>
        /// 3桁目の数値
        /// </summary>
        public BitmapImage ThirdDigit
        {
            get { return _ThirdDigit; }
            set { SetProperty ( ref _ThirdDigit, value ); }
        }

        /// <summary>
        /// 2桁目の数値
        /// </summary>
        public BitmapImage SecondDigit
        {
            get { return _SecondDigit; }
            set { SetProperty ( ref _SecondDigit, value ); }
        }

        /// <summary>
        /// 1桁目の数値
        /// </summary>
        public BitmapImage FirstDigit
        {
            get { return _FirstDigit; }
            set { SetProperty ( ref _FirstDigit, value ); }
        }

        /// <summary>
        /// Counterのビューモデルのコンストラクタ
        /// </summary>
        public CounterViewModel ( )
        {
            SixthDigit = null;
            FifthDigit = null;
            ForthDigit = null;
            ThirdDigit = null;
            SecondDigit = null;
            FirstDigit = create_bitmap_image ( _NumDictionary[ 0 ] );
        }

        /// <summary>
        /// 整数型の数値を設定すると適切に数値画像を選択して表示してくれる
        /// </summary>
        /// <param name="pNumber">カウント値</param>
        public void SetNumber ( ulong pNumber )
        {
            ulong l_Temp;

            if( pNumber < 0 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = null;
                ThirdDigit = null;
                SecondDigit = null;
                FirstDigit = null;
            }
            else if( pNumber >= 0 && pNumber < 10 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = null;
                ThirdDigit = null;
                SecondDigit = null;
                FirstDigit = create_bitmap_image ( _NumDictionary[ pNumber ] );
            }
            else if( pNumber >= 10 && pNumber < 100 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = null;
                ThirdDigit = null;
                SecondDigit = create_bitmap_image ( _NumDictionary[ pNumber / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _NumDictionary[ l_Temp ] );
            }
            else if( pNumber >= 100 && pNumber < 1000 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = null;
                ThirdDigit = create_bitmap_image ( _NumDictionary[ pNumber / 100 ] );
                l_Temp = pNumber % 100;
                SecondDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _NumDictionary[ l_Temp ] );
            }
            else if( pNumber >= 1000 && pNumber < 10000 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = create_bitmap_image ( _NumDictionary[ pNumber / 1000 ] );
                l_Temp = pNumber % 1000;
                ThirdDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 100 ] );
                l_Temp = pNumber % 100;
                SecondDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _NumDictionary[ l_Temp ] );
            }
            else if( pNumber >= 10000 && pNumber < 100000 )
            {
                SixthDigit = null;
                FifthDigit = create_bitmap_image ( _NumDictionary[ pNumber / 10000 ] );
                l_Temp = pNumber % 10000;
                ForthDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 1000 ] );
                l_Temp = pNumber % 1000;
                ThirdDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 100 ] );
                l_Temp = pNumber % 100;
                SecondDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _NumDictionary[ l_Temp ] );
            }
            else if( pNumber >= 100000 && pNumber < 1000000 )
            {
                SixthDigit = create_bitmap_image ( _NumDictionary[ pNumber / 100000 ] );
                l_Temp = pNumber % 100000;
                FifthDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 10000 ] );
                l_Temp = pNumber % 10000;
                ForthDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 1000 ] );
                l_Temp = pNumber % 1000;
                ThirdDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 100 ] );
                l_Temp = pNumber % 100;
                SecondDigit = create_bitmap_image ( _NumDictionary[ l_Temp / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _NumDictionary[ l_Temp ] );
            }
            else
            {
                SixthDigit = create_bitmap_image ( _NumDictionary[ 9 ] );
                FirstDigit = create_bitmap_image ( _NumDictionary[ 9 ] );
                SecondDigit = create_bitmap_image ( _NumDictionary[ 9 ] );
                ThirdDigit = create_bitmap_image ( _NumDictionary[ 9 ] );
                ForthDigit = create_bitmap_image ( _NumDictionary[ 9 ] );
                FifthDigit = create_bitmap_image ( _NumDictionary[ 9 ] );
            }
        }

        /// <summary>
        /// 数字画像のパスを指定するとBitmapImageクラスのインスタンスにして返す
        /// </summary>
        /// <param name="pFilePath">数字画像のパス</param>
        /// <returns>数字画像のBitmapImage</returns>
        private BitmapImage create_bitmap_image ( string pFilePath )
        {
            BitmapImage l_Img = new BitmapImage( );

            l_Img.BeginInit ( );
            l_Img.CacheOption = BitmapCacheOption.OnLoad;
            l_Img.UriSource = new Uri ( pFilePath, UriKind.Absolute );
            l_Img.EndInit ( );

            return l_Img;
        }
    }
}
