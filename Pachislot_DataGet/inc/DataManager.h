/**
 * =============================================================
 * @file        DataManager.h
 * @author      kinketsu patron (https://kinketsu-patron.com)
 * @brief       データ入出力管理ヘッダファイル
 * @version     1.0
 * @date        2024-06-08
 * =============================================================
 */

#ifndef         __DATA_H__
#define         __DATA_H__

// =======================================================
// ヘッダインクルード
// =======================================================
#include "vtype.h"

// =======================================================
// 関数宣言
// =======================================================
extern void Data_Init( void );
extern uint32 Get_IN_Coin  ( void );
extern uint32 Get_OUT_Coin ( void );
extern uint32 Get_RB  ( void );
extern uint32 Get_BB  ( void );
extern void Set_IN_Coin ( uint32 pIN_Coin );
extern void Set_OUT_Coin( uint32 pOUT_Coin );
extern void Set_RB ( uint32 pRB );
extern void Set_BB ( uint32 pBB );

#endif          __DATA_H__