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
        private Dictionary<ulong, string> _num_dictionary = new Dictionary<ulong, string>
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

        public BitmapImage _sixthdigit;
        public BitmapImage SixthDigit
        {
            get { return _sixthdigit; }
            set { SetProperty ( ref _sixthdigit, value ); }
        }
        public BitmapImage _fifthdigit;
        public BitmapImage FifthDigit
        {
            get { return _fifthdigit; }
            set { SetProperty ( ref _fifthdigit, value ); }
        }
        public BitmapImage _forthdigit;
        public BitmapImage ForthDigit
        {
            get { return _forthdigit; }
            set { SetProperty ( ref _forthdigit, value ); }
        }
        public BitmapImage _thirddigit;
        public BitmapImage ThirdDigit
        {
            get { return _thirddigit; }
            set { SetProperty ( ref _thirddigit, value ); }
        }
        public BitmapImage _seconddigit;
        public BitmapImage SecondDigit
        {
            get { return _seconddigit; }
            set { SetProperty ( ref _seconddigit, value ); }
        }
        public BitmapImage _firstdigit;
        public BitmapImage FirstDigit
        {
            get { return _firstdigit; }
            set { SetProperty ( ref _firstdigit, value ); }
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
            FirstDigit = create_bitmap_image ( _num_dictionary[ 0 ] );
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
                FirstDigit = create_bitmap_image ( _num_dictionary[ pNumber ] );
            }
            else if( pNumber >= 10 && pNumber < 100 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = null;
                ThirdDigit = null;
                SecondDigit = create_bitmap_image ( _num_dictionary[ pNumber / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _num_dictionary[ l_Temp ] );
            }
            else if( pNumber >= 100 && pNumber < 1000 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = null;
                ThirdDigit = create_bitmap_image ( _num_dictionary[ pNumber / 100 ] );
                l_Temp = pNumber % 100;
                SecondDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _num_dictionary[ l_Temp ] );
            }
            else if( pNumber >= 1000 && pNumber < 10000 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = create_bitmap_image ( _num_dictionary[ pNumber / 1000 ] );
                l_Temp = pNumber % 1000;
                ThirdDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 100 ] );
                l_Temp = pNumber % 100;
                SecondDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _num_dictionary[ l_Temp ] );
            }
            else if( pNumber >= 10000 && pNumber < 100000 )
            {
                SixthDigit = null;
                FifthDigit = create_bitmap_image ( _num_dictionary[ pNumber / 10000 ] );
                l_Temp = pNumber % 10000;
                ForthDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 1000 ] );
                l_Temp = pNumber % 1000;
                ThirdDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 100 ] );
                l_Temp = pNumber % 100;
                SecondDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _num_dictionary[ l_Temp ] );
            }
            else if( pNumber >= 100000 && pNumber < 1000000 )
            {
                SixthDigit = create_bitmap_image ( _num_dictionary[ pNumber / 100000 ] );
                l_Temp = pNumber % 100000;
                FifthDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 10000 ] );
                l_Temp = pNumber % 10000;
                ForthDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 1000 ] );
                l_Temp = pNumber % 1000;
                ThirdDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 100 ] );
                l_Temp = pNumber % 100;
                SecondDigit = create_bitmap_image ( _num_dictionary[ l_Temp / 10 ] );
                l_Temp = pNumber % 10;
                FirstDigit = create_bitmap_image ( _num_dictionary[ l_Temp ] );
            }
            else
            {
                SixthDigit = create_bitmap_image ( _num_dictionary[ 9 ] );
                FirstDigit = create_bitmap_image ( _num_dictionary[ 9 ] );
                SecondDigit = create_bitmap_image ( _num_dictionary[ 9 ] );
                ThirdDigit = create_bitmap_image ( _num_dictionary[ 9 ] );
                ForthDigit = create_bitmap_image ( _num_dictionary[ 9 ] );
                FifthDigit = create_bitmap_image ( _num_dictionary[ 9 ] );
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
