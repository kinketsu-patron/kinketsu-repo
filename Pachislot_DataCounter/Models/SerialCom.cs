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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;
using Pachislot_DataCounter.Views;
using Pachislot_DataCounter.ViewModels;

namespace Pachislot_DataCounter.Models
{
    public class SerialCom
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private SerialPort _SerialPort = null;
        private IRegionManager _RegionManager = null;

        // =======================================================
        // メソッド
        // =======================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SerialCom ( IRegionManager pRegionManager )
        {
            _RegionManager = pRegionManager;

            _SerialPort = new SerialPort
            {
                PortName = "COM3",
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                Encoding = Encoding.UTF8,
                WriteTimeout = 5000,
                ReadTimeout = 5000,
                DtrEnable = true
            };
        }

        public void ComStart ( )
        {
            try
            {
                _SerialPort.Open ( );
                _SerialPort.DataReceived += new SerialDataReceivedEventHandler ( com_received );
            }
            catch( Exception ex )
            {
                MessageBox.Show ( ex.Message );
            }
        }

        public void ComStop ( )
        {
            try
            {
                if( _SerialPort != null )
                {
                    if( _SerialPort.IsOpen )
                    {
                        _SerialPort.Close ( );
                    }
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show ( ex.Message );
            }
        }

        private void com_received ( object sender, SerialDataReceivedEventArgs e )
        {
            if( _SerialPort == null )
            {
                return;
            }

            if( _SerialPort.IsOpen == false )
            {
                return;
            }

            try
            {
                string l_Data = _SerialPort.ReadLine ( );
                split_data ( l_Data );
                var l_ContentRegion = _RegionManager.Regions[ "BigBonusCounter" ];
                var l_ContentView = l_ContentRegion.Views.FirstOrDefault() as Counter;
                var l_ContentViewModel = (CounterViewModel)l_ContentView.DataContext;
            }
            catch( Exception ex )
            {
                MessageBox.Show ( ex.Message );
            }
        }

        private string[ ] split_data ( string pString )
        {
            string[ ] l_SplitString;

            l_SplitString = pString.Split ( ',' );

            return l_SplitString;
        }
    }
}
