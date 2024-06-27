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
        Serial.begin( 9600 );        // PCとのシリアル通信ボーレートを9600bpsに設定
}

/**
 * =======================================================
 * @fn          Serial_Send
 * @brief       シリアル通信でPCにメッセージを送信する
 * @param pGameInfo  遊戯情報
 * @date        2024-06-12
 * =======================================================
 */
void Serial_Send( GAME_INFO *pGameInfo )
{
        JSONVar l_Jobj;
        String  l_SendStrMsg;

        l_Jobj[ "game" ]      = pGameInfo->Game;
        l_Jobj[ "totalgame" ] = pGameInfo->TotalGame;
        l_Jobj[ "in" ]        = pGameInfo->IN;
        l_Jobj[ "out" ]       = pGameInfo->OUT;
        l_Jobj[ "diff" ]      = pGameInfo->Diff;
        l_Jobj[ "rb" ]        = pGameInfo->RB;
        l_Jobj[ "bb" ]        = pGameInfo->BB;
        l_Jobj[ "duringrb" ]  = pGameInfo->DuringRB;
        l_Jobj[ "duringbb" ]  = pGameInfo->DuringBB;

        l_SendStrMsg = JSON.stringify( l_Jobj );        // JSON形式をString型に変換する
        Serial.println( l_SendStrMsg );                 // String型でメッセージを送る
}