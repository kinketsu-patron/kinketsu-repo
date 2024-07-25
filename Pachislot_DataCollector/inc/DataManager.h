/**
 * =============================================================
 * @file        DataManager.h
 * @author      kinketsu patron (https://kinketsu-patron.com)
 * @brief       データ入出力管理ヘッダファイル
 * @version     1.0
 * @date        2024-06-08
 * =============================================================
 */

#ifndef __DATA_H__
#define __DATA_H__

// =======================================================
// ヘッダインクルード
// =======================================================
#include "vtype.h"
#include "struct.h"

// =======================================================
// 関数宣言
// =======================================================
extern void      Data_Init( void );
extern GAME_INFO Data_GetAllData( void );
extern ulong32    Data_GetGame( void );
extern ulong32    Data_GetTotalGame( void );
extern ulong32    Data_GetIN( void );
extern ulong32    Data_GetOUT( void );
extern ulong32    Data_GetRB( void );
extern ulong32    Data_GetBB( void );
extern bool      Data_GetDuringRB( void );
extern bool      Data_GetDuringBB( void );
extern bool      Data_GetDuringBonus( void );
extern void      Data_SetGame( ulong32 p_Game );
extern void      Data_SetTotalGame( ulong32 p_TotalGame );
extern void      Data_SetIN( ulong32 p_IN );
extern void      Data_SetOUT( ulong32 p_OUT );
extern void      Data_SetRB( ulong32 p_RB );
extern void      Data_SetBB( ulong32 p_BB );
extern void      Data_SetDuringRB( bool p_DuringRB );
extern void      Data_SetDuringBB( bool p_DuringBB );

#endif __DATA_H__