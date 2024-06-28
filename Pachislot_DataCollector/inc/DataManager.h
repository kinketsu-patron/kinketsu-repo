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
extern uint32    Data_GetGame( void );
extern uint32    Data_GetTotalGame( void );
extern uint32    Data_GetIN( void );
extern uint32    Data_GetOUT( void );
extern uint32    Data_GetRB( void );
extern uint32    Data_GetBB( void );
extern bool      Data_GetDuringRB( void );
extern bool      Data_GetDuringBB( void );
extern bool      Data_GetDuringBonus( void );
extern void      Data_SetGame( uint32 p_Game );
extern void      Data_SetTotalGame( uint32 p_TotalGame );
extern void      Data_SetIN( uint32 p_IN );
extern void      Data_SetOUT( uint32 p_OUT );
extern void      Data_SetRB( uint32 p_RB );
extern void      Data_SetBB( uint32 p_BB );
extern void      Data_SetDuringRB( bool p_DuringRB );
extern void      Data_SetDuringBB( bool p_DuringBB );

#endif __DATA_H__