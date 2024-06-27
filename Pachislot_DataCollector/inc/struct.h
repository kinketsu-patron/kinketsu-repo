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
                uint32 Game;
                uint32 TotalGame;
                uint32 IN;
                uint32 OUT;
                sint32 Diff;
                uint32 RB;
                uint32 BB;
                bool   DuringRB;
                bool   DuringBB;
} GAME_INFO;

#endif __STRUCT_H__