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
using System.IO.Ports;
using System.Text;
using System.Text.Json;

namespace Pachislot_DataCounter.Models
{
        public class SerialCom : SerialPort
        {
                #region 公開メソッド
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
                        WriteTimeout = 5000;
                        ReadTimeout = 5000;
                        DtrEnable = true;
                }

                /// <summary>
                /// 通信スタート
                /// </summary>
                public void ComStart( )
                {
                        try
                        {
                                Open( );
                        }
                        catch
                        {
                                throw;
                        }
                }

                /// <summary>
                /// 通信ストップ
                /// </summary>
                public void ComStop( )
                {
                        try
                        {
                                if ( IsOpen )
                                {
                                        Close( );
                                }
                        }
                        catch
                        {
                                throw;
                        }
                }

                /// <summary>
                /// シリアル通信で取得したJSONメッセージから各種ゲームデータのオブジェクトを生成して返す
                /// </summary>
                /// <returns>ゲーム情報のインスタンス</returns>
                public GameInfo GetGameInfo( )
                {
                        string l_Message;
                        GameInfo l_GameInfo = null;

                        try
                        {
                                l_Message = ReadLine( );
                                l_GameInfo = JsonSerializer.Deserialize<GameInfo>( l_Message );
                        }
                        catch
                        {
                                throw;
                        }

                        return l_GameInfo;
                }
                #endregion
        }
}
