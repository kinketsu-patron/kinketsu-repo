/**
 * =============================================================
 * @file        Serial_Com.cpp
 * @author      kinketsu patron (https://kinketsu-patron.com)
 * @brief       シリアル通信設定
 * @version     1.0
 * @date        2024-06-08
 * =============================================================
 */

// =======================================================
// ヘッダインクルード
// =======================================================
#include "inc/Serial_Com.h"

/**
 * =======================================================
 * @fn         Serial_Init
 * @brief      シリアル通信の初期設定を行う
 * @date       2024-06-08
 * =======================================================
 */
void Serial_Init( void )
{
        Serial.begin( 115200 );        // PCとのシリアル通信ボーレートを115200bpsに設定
}

/**
 * =======================================================
 * @fn          Serial_Send
 * @brief       シリアル通信でPCにメッセージを送信する
 * @param p_GameInfo  遊戯情報
 * @date        2024-06-12
 * =======================================================
 */
void Serial_Send( GAME_INFO *p_GameInfo )
{
        JSONVar l_Jobj;
        String  l_SendStrMsg;

        l_Jobj[ "game" ]      = p_GameInfo->Game;
        l_Jobj[ "totalgame" ] = p_GameInfo->TotalGame;
        l_Jobj[ "in" ]        = p_GameInfo->IN;
        l_Jobj[ "out" ]       = p_GameInfo->OUT;
        l_Jobj[ "diff" ]      = p_GameInfo->Diff;
        l_Jobj[ "rb" ]        = p_GameInfo->RB;
        l_Jobj[ "bb" ]        = p_GameInfo->BB;
        l_Jobj[ "duringrb" ]  = p_GameInfo->DuringRB;
        l_Jobj[ "duringbb" ]  = p_GameInfo->DuringBB;

        l_SendStrMsg = JSON.stringify( l_Jobj );        // JSON形式をString型に変換する
        Serial.println( l_SendStrMsg );                 // String型でメッセージを送る
}