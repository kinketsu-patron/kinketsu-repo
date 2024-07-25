/**
 * =============================================================
 * @file        Pachislot_DataCollector.ino
 * @author      kinketsu patron (https://kinketsu-patron.com)
 * @brief       メインファイル
 * @version     1.0
 * @date        2024-06-25
 * =============================================================
 */

// =======================================================
// ヘッダインクルード
// =======================================================
#include "inc/Port.h"
#include "inc/Interrupt.h"
#include "inc/Serial_Com.h"
#include "inc/DataManager.h"

// =======================================================
// ローカル関数
// =======================================================
static void update_game( void );
static void update_in( void );
static void update_out( void );
static void begin_rb( void );
static void end_rb( void );
static void begin_bb( void );
static void end_bb( void );

// =======================================================
// 割り込み発生時のコールバック関数
// =======================================================
static INTR_CALLBACK _IntrPtr[] = {
        update_game,
        update_in,
        update_out,
        begin_rb,
        end_rb,
        begin_bb,
        end_bb
};

/**
 * =======================================================
 * @fn         setup
 * @brief      初期化を行う
 * @date        2024-06-08
 * =======================================================
 */
void setup( void )
{
        Port_Init( );                 // ポートを初期化する
        Intr_Init( _IntrPtr );        // 割り込み機能を初期化する
        Serial_Init( );               // シリアル通信を初期化する
        Data_Init( );                 // データ管理を初期化する
}

/**
 * =======================================================
 * @fn         loop
 * @brief      繰り返し処理を行う
 * @date        2024-06-26
 * =======================================================
 */
void loop( void )
{
        // 何もしない
}

/**
 * =======================================================
 * @fn         update_game
 * @brief      ゲーム数を更新する
 * @date        2024-06-26
 * =======================================================
 */
static void update_game( void )
{
        ulong32 l_CurrentGame;

        if ( Data_GetDuringBonus( ) == false )        // ボーナス中はゲーム数のカウントを止める
        {
                noInterrupts( );                               // 他の割り込みを禁止する
                l_CurrentGame = Data_GetGame( );               // 現在のゲーム回数を取得する
                l_CurrentGame++;                               // 現在のゲーム回数に+1する
                Data_SetGame( l_CurrentGame );                 // ゲーム回数を更新する
                l_CurrentGame = Data_GetTotalGame( );          // 現在の累計ゲーム回数を取得する
                l_CurrentGame++;                               // 現在の累計ゲーム回数に+1する
                Data_SetTotalGame( l_CurrentGame );            // 累計ゲーム回数を更新する
                Serial_Send( &( Data_GetAllData( ) ) );        // すべてのゲーム情報をPCへ送信する
                interrupts( );                                 // 他の割り込みを許可する
        }
}

/**
 * =======================================================
 * @fn         update_in
 * @brief      INを更新する
 * @date        2024-06-25
 * =======================================================
 */
static void update_in( void )
{
        ulong32 l_CurrentIN;

        noInterrupts( );                               // 他の割り込みを禁止する
        l_CurrentIN = Data_GetIN( );                   // 現在のIN枚数を取得する
        l_CurrentIN++;                                 // 現在のIN枚数に+1する
        Data_SetIN( l_CurrentIN );                     // IN枚数を更新する
        Serial_Send( &( Data_GetAllData( ) ) );        // すべてのゲーム情報をPCへ送信する
        interrupts( );                                 // 他の割り込みを許可する
}

/**
 * =======================================================
 * @fn         update_out
 * @brief      OUTを更新する
 * @date        2024-06-25
 * =======================================================
 */
static void update_out( void )
{
        ulong32 l_CurrentOUT;

        noInterrupts( );                               // 他の割り込みを禁止する
        l_CurrentOUT = Data_GetOUT( );                 // 現在のOUT枚数を取得する
        l_CurrentOUT++;                                // 現在のOUT枚数に+1する
        Data_SetOUT( l_CurrentOUT );                   // OUT枚数を更新する
        Serial_Send( &( Data_GetAllData( ) ) );        // すべてのゲーム情報をPCへ送信する
        interrupts( );                                 // 他の割り込みを許可する
}

/**
 * =======================================================
 * @fn         begin_rb
 * @brief      RBが開始される
 * @date        2024-06-26
 * =======================================================
 */
static void begin_rb( void )
{
        ulong32 l_CurrentRB;

        noInterrupts( );                               // 他の割り込みを禁止する
        l_CurrentRB = Data_GetRB( );                   // 現在のRB回数を取得する
        l_CurrentRB++;                                 // 現在のRB回数に+1する
        Data_SetRB( l_CurrentRB );                     // RB回数を更新する
        Data_SetDuringRB( true );                      // RB中フラグを立てる
        Serial_Send( &( Data_GetAllData( ) ) );        // すべてのゲーム情報をPCへ送信する
        interrupts( );                                 // 他の割り込みを許可する
}

/**
 * =======================================================
 * @fn         end_rb
 * @brief      RBが終了される
 * @date        2024-06-26
 * =======================================================
 */
static void end_rb( void )
{
        noInterrupts( );                               // 他の割り込みを禁止する
        Data_SetDuringRB( false );                     // RB中フラグを下ろす
        Data_SetGame( 0U );                            // ゲーム数を0にリセットする
        Serial_Send( &( Data_GetAllData( ) ) );        // すべてのゲーム情報をPCへ送信する
        interrupts( );                                 // 他の割り込みを許可する
}

/**
 * =======================================================
 * @fn         begin_bb
 * @brief      BBが開始される
 * @date        2024-06-26
 * =======================================================
 */
static void begin_bb( void )
{
        ulong32 l_CurrentBB;

        noInterrupts( );                               // 他の割り込みを禁止する
        l_CurrentBB = Data_GetBB( );                   // 現在のBB回数を取得する
        l_CurrentBB++;                                 // 現在のBB回数に+1する
        Data_SetBB( l_CurrentBB );                     // BB回数を更新する
        Data_SetDuringBB( true );                      // BB中フラグを立てる
        Serial_Send( &( Data_GetAllData( ) ) );        // すべてのゲーム情報をPCへ送信する
        interrupts( );                                 // 他の割り込みを許可する
}

/**
 * =======================================================
 * @fn         end_bb
 * @brief      BBが終了される
 * @date        2024-06-26
 * =======================================================
 */
static void end_bb( void )
{
        noInterrupts( );                               // 他の割り込みを禁止する
        Data_SetDuringBB( false );                     // BB中フラグを下ろす
        Data_SetGame( 0U );                            // ゲーム数を0にリセットする
        Serial_Send( &( Data_GetAllData( ) ) );        // すべてのゲーム情報をPCへ送信する
        interrupts( );                                 // 他の割り込みを許可する
}