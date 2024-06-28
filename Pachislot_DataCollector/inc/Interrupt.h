/**
 * =============================================================
 * @file        Interrupt.h
 * @author      kinketsu patron (https://kinketsu-patron.com)
 * @brief       割り込み設定ヘッダファイル
 * @version     1.0
 * @date        2024-06-08
 * =============================================================
 */

#ifndef __INTERRUPT_H__
#define __INTERRUPT_H__

// =======================================================
// ヘッダインクルード
// =======================================================
#include <Arduino.h>
#include "vtype.h"
#include "PinDefine.h"
#include "DataManager.h"
#include "Serial_Com.h"

#define INTR_WAIT      80U         // 80ms間は次の外部割込みをマスク
#define GAMECOUNT_WAIT 500U        // 500ms間待てば3枚掛けの3パルス分は無視できる

// =======================================================
// 関数ポインタ定義
// =======================================================
typedef void ( *INTR_CALLBACK )( void );

// =======================================================
// 関数宣言
// =======================================================
extern void Intr_Init( INTR_CALLBACK *p_Func );

#endif __INTERRUPT_H__