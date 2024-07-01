/**
 * =============================================================
 * File         :SerialCom.cs
 * Summary      :シリアル通信クラス
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/22
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
using System.Threading.Tasks;
using System.Windows;

namespace Pachislot_DataCounter.Models
{
        public class SerialCom : SerialPort
        {
                // =======================================================
                // メソッド
                // =======================================================
                /// <summary>
                /// コンストラクタ
                /// </summary>
                public SerialCom( )
                {
                        PortName = "COM3";
                        BaudRate = 115200;
                        DataBits = 8;
                        Parity = Parity.None;
                        Encoding = Encoding.UTF8;
                        WriteTimeout = 10000;
                        ReadTimeout = 10000;
                        DtrEnable = true;
                        ReadBufferSize = 4096;
                }

                public void ComStart( )
                {
                        try
                        {
                                Open( );
                        }
                        catch ( Exception ex )
                        {
                                MessageBox.Show( ex.Message );
                        }
                }

                public void ComStop( )
                {
                        try
                        {
                                if ( IsOpen )
                                {
                                        Close( );
                                }
                        }
                        catch ( Exception ex )
                        {
                                MessageBox.Show( ex.Message );
                        }
                }

                public string GetSerialMessage( )
                {
                        string l_Str;

                        if ( IsOpen == false )
                        {
                                return null;
                        }

                        l_Str = ReadLine( );

                        return l_Str;
                }
        }
}
