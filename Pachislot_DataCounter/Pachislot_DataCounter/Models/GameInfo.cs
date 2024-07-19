/**
 * =============================================================
 * File         :GameInfo.cs
 * Summary      :ゲーム情報クラス
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/30
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using System.Text.Json.Serialization;

namespace Pachislot_DataCounter.Models
{
        /// <summary>
        /// ゲーム情報クラス
        /// </summary>
        public class GameInfo
        {
                #region プロパティ
                [JsonPropertyName( "game" )]
                public uint Game { get; set; }

                [JsonPropertyName( "totalgame" )]
                public uint TotalGame { get; set; }

                [JsonPropertyName( "in" )]
                public uint In { get; set; }

                [JsonPropertyName( "out" )]
                public uint Out { get; set; }

                [JsonPropertyName( "diff" )]
                public int Diff { get; set; }

                [JsonPropertyName( "rb" )]
                public uint RB { get; set; }

                [JsonPropertyName( "bb" )]
                public uint BB { get; set; }

                [JsonPropertyName( "duringrb" )]
                public bool DuringRB { get; set; }

                [JsonPropertyName( "duringbb" )]
                public bool DuringBB { get; set; }
                #endregion
        }
}
