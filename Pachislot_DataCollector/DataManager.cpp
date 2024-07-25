/**
 * =============================================================
 * @file        DataManager.cpp
 * @author      kinketsu patron (https://kinketsu-patron.com)
 * @brief       データ入出力管理
 * @version     1.0
 * @date        2024-06-08
 * =============================================================
 */

// =======================================================
// ヘッダインクルード
// =======================================================
#include "inc/DataManager.h"

// =======================================================
// 静的変数
// =======================================================
static volatile ulong32 m_Game;
static volatile ulong32 m_TotalGame;
static volatile ulong32 m_IN;
static volatile ulong32 m_OUT;
static volatile ulong32 m_RB;
static volatile ulong32 m_BB;
static volatile bool   m_DuringRB;
static volatile bool   m_DuringBB;


/**
 * =======================================================
 * @fn          Data_Init
 * @brief       データ管理の初期化を行う
 * @date        2024-06-10
 * =======================================================
 */
void Data_Init( void )
{
        m_Game      = 0U;
        m_TotalGame = 0U;
        m_IN        = 0U;
        m_OUT       = 0U;
        m_RB        = 0U;
        m_BB        = 0U;
        m_DuringRB  = false;
        m_DuringBB  = false;
}

/**
 * =======================================================
 * @fn          Data_GetAllData
 * @brief       全てのデータを取得する
 * @date        2024-06-26
 * =======================================================
 */
GAME_INFO Data_GetAllData( void )
{
        GAME_INFO l_DataInfo;

        l_DataInfo.Game      = m_Game;
        l_DataInfo.TotalGame = m_TotalGame;
        l_DataInfo.IN        = m_IN;
        l_DataInfo.OUT       = m_OUT;
        l_DataInfo.Diff      = ( slong32 )m_OUT - ( slong32 )m_IN;
        l_DataInfo.RB        = m_RB;
        l_DataInfo.BB        = m_BB;
        l_DataInfo.DuringRB  = m_DuringRB;
        l_DataInfo.DuringBB  = m_DuringBB;

        return l_DataInfo;
}

/**
 * =======================================================
 * @fn          Data_GetGame
 * @brief       ゲーム回数を取得する
 * @date        2024-06-26
 * =======================================================
 */
ulong32 Data_GetGame( void )
{
        return m_Game;
}

/**
 * =======================================================
 * @fn          Data_GetTotalGame
 * @brief       累計ゲーム回数を取得する
 * @date        2024-06-26
 * =======================================================
 */
ulong32 Data_GetTotalGame( void )
{
        return m_TotalGame;
}

/**
 * =======================================================
 * @fn          Data_GetIN
 * @brief       入力枚数を取得する
 * @date        2024-06-10
 * =======================================================
 */
ulong32 Data_GetIN( void )
{
        return m_IN;
}

/**
 * =======================================================
 * @fn          Data_GetOUT
 * @brief       出力枚数を取得する
 * @date        2024-06-10
 * =======================================================
 */
ulong32 Data_GetOUT( void )
{
        return m_OUT;
}

/**
 * =======================================================
 * @fn          Data_GetRB
 * @brief       レギュラーボーナス回数を取得する
 * @date        2024-06-10
 * =======================================================
 */
ulong32 Data_GetRB( void )
{
        return m_RB;
}

/**
 * =======================================================
 * @fn          Data_GetBB
 * @brief       ビッグボーナス回数を取得する
 * @date        2024-06-10
 * =======================================================
 */
ulong32 Data_GetBB( void )
{
        return m_BB;
}

/**
 * =======================================================
 * @fn          Data_GetDuringRB
 * @brief       レギュラーボーナス中フラグを取得する
 * @date        2024-06-26
 * =======================================================
 */
bool Data_GetDuringRB( void )
{
        return m_DuringRB;
}

/**
 * =======================================================
 * @fn          Data_GetDuringBB
 * @brief       ビッグボーナス中フラグを取得する
 * @date        2024-06-26
 * =======================================================
 */
bool Data_GetDuringBB( void )
{
        return m_DuringBB;
}

/**
 * =======================================================
 * @fn          Data_GetDuringBonus
 * @brief       ボーナス中フラグを取得する
 * @date        2024-06-26
 * =======================================================
 */
bool Data_GetDuringBonus( void )
{
        bool l_IsBonus;

        if ( m_DuringRB == true || m_DuringBB == true )        // BB中またはRB中であれば
        {
                l_IsBonus = true;        // ボーナス中フラグを立てる
        }
        else        // ボーナス中でなければ
        {
                l_IsBonus = false;        // ボーナス中フラグを下ろす
        }

        return l_IsBonus;
}

/**
 * =======================================================
 * @fn          Data_SetGame
 * @brief       ゲーム回数を設定する
 * @date        2024-06-26
 * =======================================================
 */
void Data_SetGame( ulong32 p_Game )
{
        if ( p_Game >= 0U )
        {
                m_Game = p_Game;
        }
}

/**
 * =======================================================
 * @fn          Data_SetTotalGame
 * @brief       累計ゲーム回数を設定する
 * @date        2024-06-26
 * =======================================================
 */
void Data_SetTotalGame( ulong32 p_TotalGame )
{
        if ( p_TotalGame >= 0U )
        {
                m_TotalGame = p_TotalGame;
        }
}

/**
 * =======================================================
 * @fn          Data_SetIN
 * @brief       入力枚数を設定する
 * @date        2024-06-10
 * =======================================================
 */
void Data_SetIN( ulong32 p_IN )
{
        if ( p_IN >= 0U )
        {
                m_IN = p_IN;
        }
}

/**
 * =======================================================
 * @fn          Data_SetOUT
 * @brief       出力枚数を設定する
 * @date        2024-06-10
 * =======================================================
 */
void Data_SetOUT( ulong32 p_OUT )
{
        if ( p_OUT >= 0U )
        {
                m_OUT = p_OUT;
        }
}

/**
 * =======================================================
 * @fn          Data_SetRB
 * @brief       レギュラーボーナス回数を設定する
 * @date        2024-06-10
 * =======================================================
 */
void Data_SetRB( ulong32 p_RB )
{
        if ( p_RB >= 0U )
        {
                m_RB = p_RB;
        }
}

/**
 * =======================================================
 * @fn          Data_SetBB
 * @brief       ビッグボーナス回数を設定する
 * @date        2024-06-10
 * =======================================================
 */
void Data_SetBB( ulong32 p_BB )
{
        if ( p_BB >= 0U )
        {
                m_BB = p_BB;
        }
}

/**
 * =======================================================
 * @fn          Data_SetDuringRB
 * @brief       レギュラーボーナス中フラグを設定する
 * @date        2024-06-26
 * =======================================================
 */
void Data_SetDuringRB( bool p_DuringRB )
{
        m_DuringRB = p_DuringRB;
}

/**
 * =======================================================
 * @fn          Data_SetDuringBB
 * @brief       ビッグボーナス中フラグを設定する
 * @date        2024-06-26
 * =======================================================
 */
void Data_SetDuringBB( bool p_DuringBB )
{
        m_DuringBB = p_DuringBB;
}