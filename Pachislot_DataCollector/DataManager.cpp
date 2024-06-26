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
static volatile uint32 mGame;
static volatile uint32 mTotalGame;
static volatile uint32 mIN;
static volatile uint32 mOUT;
static volatile uint32 mRB;
static volatile uint32 mBB;
static volatile bool mDuringRB;
static volatile bool mDuringBB;


/**
 * =======================================================
 * @fn          Data_Init
 * @brief       データ管理の初期化を行う
 * @date        2024-06-10
 * =======================================================
 */
void Data_Init( void )
{
        mGame       = 0U;
        mTotalGame  = 0U;
        mIN         = 0U;
        mOUT        = 0U;
        mRB         = 0U;
        mBB         = 0U;
        mDuringRB   = false;
        mDuringBB   = false;
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

        l_DataInfo.Game      = mGame;
        l_DataInfo.TotalGame = mTotalGame;
        l_DataInfo.IN        = mIN;
        l_DataInfo.OUT       = mOUT;
        l_DataInfo.Diff      = ( sint32 )mOUT - ( sint32 )mIN;
        l_DataInfo.RB        = mRB;
        l_DataInfo.BB        = mBB;
        l_DataInfo.DuringRB  = mDuringRB;
        l_DataInfo.DuringBB  = mDuringBB;

        return l_DataInfo;
}

/**
 * =======================================================
 * @fn          Data_GetGame
 * @brief       ゲーム回数を取得する
 * @date        2024-06-26
 * =======================================================
 */
uint32 Data_GetGame( void ) { return mGame; }

/**
 * =======================================================
 * @fn          Data_GetTotalGame
 * @brief       累計ゲーム回数を取得する
 * @date        2024-06-26
 * =======================================================
 */
uint32 Data_GetTotalGame( void ) { return mTotalGame; }

/**
 * =======================================================
 * @fn          Data_GetIN
 * @brief       入力枚数を取得する
 * @date        2024-06-10
 * =======================================================
 */
uint32 Data_GetIN ( void ) { return mIN; }

/**
 * =======================================================
 * @fn          Data_GetOUT
 * @brief       出力枚数を取得する
 * @date        2024-06-10
 * =======================================================
 */
uint32 Data_GetOUT( void ) { return mOUT; }

/**
 * =======================================================
 * @fn          Data_GetRB
 * @brief       レギュラーボーナス回数を取得する
 * @date        2024-06-10
 * =======================================================
 */
uint32 Data_GetRB ( void ) { return mRB; }

/**
 * =======================================================
 * @fn          Data_GetBB
 * @brief       ビッグボーナス回数を取得する
 * @date        2024-06-10
 * =======================================================
 */
uint32 Data_GetBB ( void ) { return mBB; }

/**
 * =======================================================
 * @fn          Data_GetDuringRB
 * @brief       レギュラーボーナス中フラグを取得する
 * @date        2024-06-26
 * =======================================================
 */
bool Data_GetDuringRB ( void ) { return mDuringRB; }

/**
 * =======================================================
 * @fn          Data_GetDuringBB
 * @brief       ビッグボーナス中フラグを取得する
 * @date        2024-06-26
 * =======================================================
 */
bool Data_GetDuringBB ( void ) { return mDuringBB; }

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
        
        if ( mDuringRB == true || mDuringBB == true )
        {
                l_IsBonus = true;
        }
        else
        {
                l_IsBonus = false;
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
void Data_SetGame( uint32 pGame )
{
        if ( pGame >= 0U )
        {
                mGame = pGame;
        }
}

/**
 * =======================================================
 * @fn          Data_SetTotalGame
 * @brief       累計ゲーム回数を設定する
 * @date        2024-06-26
 * =======================================================
 */
void Data_SetTotalGame( uint32 pTotalGame )
{
        if ( pTotalGame >= 0U )
        {
                mTotalGame = pTotalGame;
        }
}

/**
 * =======================================================
 * @fn          Data_SetIN
 * @brief       入力枚数を設定する
 * @date        2024-06-10
 * =======================================================
 */
void Data_SetIN( uint32 pIN )
{
        if ( pIN >= 0U )
        {
                mIN = pIN;
        }
}

/**
 * =======================================================
 * @fn          Data_SetOUT
 * @brief       出力枚数を設定する
 * @date        2024-06-10
 * =======================================================
 */
void Data_SetOUT( uint32 pOUT )
{
        if ( pOUT >= 0U )
        {
                mOUT = pOUT;
        }
}

/**
 * =======================================================
 * @fn          Data_SetRB
 * @brief       レギュラーボーナス回数を設定する
 * @date        2024-06-10
 * =======================================================
 */
void Data_SetRB( uint32 pRB )
{
        if ( pRB >= 0U )
        {
                mRB = pRB;
        }
}

/**
 * =======================================================
 * @fn          Data_SetBB
 * @brief       ビッグボーナス回数を設定する
 * @date        2024-06-10
 * =======================================================
 */
void Data_SetBB( uint32 pBB )
{
        if ( pBB >= 0U )
        {
                mBB = pBB;
        }
}

/**
 * =======================================================
 * @fn          Data_SetDuringRB
 * @brief       レギュラーボーナス中フラグを設定する
 * @date        2024-06-26
 * =======================================================
 */
void Data_SetDuringRB( bool pDuringRB )
{
        mDuringRB = pDuringRB;
}

/**
 * =======================================================
 * @fn          Data_SetDuringBB
 * @brief       ビッグボーナス中フラグを設定する
 * @date        2024-06-26
 * =======================================================
 */
void Data_SetDuringBB( bool pDuringBB )
{
        mDuringBB = pDuringBB;
}