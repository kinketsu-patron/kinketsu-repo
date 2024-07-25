/**
 * =============================================================
 * @file        struct.h
 * @author      kinketsu patron (https://kinketsu-patron.com)
 * @brief       構造体定義ファイル
 * @version     1.0
 * @date        2024-06-26
 * =============================================================
 */

#ifndef __STRUCT_H__
#define __STRUCT_H__

#include "vtype.h"

// =======================================================
// 構造体定義
// =======================================================
typedef struct
{
                ulong32 Game;
                ulong32 TotalGame;
                ulong32 IN;
                ulong32 OUT;
                slong32 Diff;
                ulong32 RB;
                ulong32 BB;
                bool   DuringRB;
                bool   DuringBB;
} GAME_INFO;

#endif __STRUCT_H__