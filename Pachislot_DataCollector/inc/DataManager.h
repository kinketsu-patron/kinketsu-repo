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
extern uint32 Data_GetIN  ( void );
extern uint32 Data_GetOUT ( void );
extern uint32 Data_GetRB  ( void );
extern uint32 Data_GetBB  ( void );
extern void Data_SetIN ( uint32 pIN_Coin );
extern void Data_SetOUT( uint32 pOUT_Coin );
extern void Data_SetRB ( uint32 pRB );
extern void Data_SetBB ( uint32 pBB );

#endif          __DATA_H__