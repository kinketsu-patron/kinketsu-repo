/**
 * =============================================================
 * @file        Serial_Com.h
 * @author      kinketsu patron (https://kinketsu-patron.com)
 * @brief       シリアル通信設定ヘッダファイル
 * @version     1.0
 * @date        2024-06-08
 * =============================================================
 */

#ifndef __SERIAL_COM_H__
#define __SERIAL_COM_H__

// =======================================================
// ヘッダインクルード
// =======================================================
#include <Arduino.h>
#include <Arduino_JSON.h>
#include "vtype.h"
#include "struct.h"

// =======================================================
// 関数宣言
// =======================================================
extern void Serial_Init( void );
extern void Serial_Send( GAME_INFO *p_GameInfo );

#endif __SERIAL_COM_H__