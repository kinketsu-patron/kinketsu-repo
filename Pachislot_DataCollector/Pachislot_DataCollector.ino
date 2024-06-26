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
static void update_in( void );
static void update_out( void );
static void update_rb( void );
static void update_bb( void );
static void update_game( void );

// =======================================================
// 割り込み発生時のコールバック関数
// =======================================================
static INTR_CALLBACK _IntrPtr[] =
{
        update_in,
        update_out,
        update_rb,
        update_bb,
        update_game
};

/**
 * =======================================================
 * @fn         setup
 * @brief      初期化を行う
 * @date        2024-06-08
 * =======================================================
 */
void    setup( void )
{
        Port_Init( );           // ポートを初期化する
        Intr_Init( _IntrPtr );  // 割り込み機能を初期化する
        Serial_Init( );         // シリアル通信を初期化する
        Data_Init( );           // データ管理を初期化する
}

/**
 * =======================================================
 * @fn         loop
 * @brief      繰り返し処理を行う
 * @date        2024-06-08
 * =======================================================
 */
void    loop( void )
{
        // メインループでは何もしない
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
        uint32 l_curr_in;
        
        noInterrupts( );                        // 他の割り込みを禁止する
        l_curr_in = Data_GetIN( );              // 現在のIN枚数を取得する
        l_curr_in++;                            // 現在のIN枚数に+1する
        Data_SetIN( l_curr_in );                // IN枚数を更新する
        
        interrupts( );                          // 他の割り込みを許可する
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
        noInterrupts( );                        // 他の割り込みを禁止する
        interrupts( );                          // 他の割り込みを許可する
}

/**
 * =======================================================
 * @fn         update_rb
 * @brief      RBを更新する
 * @date        2024-06-25
 * =======================================================
 */
static void update_rb( void )
{
        noInterrupts( );                        // 他の割り込みを禁止する
        interrupts( );                          // 他の割り込みを許可する
}

/**
 * =======================================================
 * @fn         update_bb
 * @brief      BBを更新する
 * @date        2024-06-25
 * =======================================================
 */
static void update_bb( void )
{
        noInterrupts( );                        // 他の割り込みを禁止する

        // l_curr_bb = Get_BB( );                  // BB回数を取得する
        // l_curr_bb++;                            // 現在のBB回数に+1する
        // Set_BB( l_curr_bb );                    // BB回数を更新する
        // Serial_Write( "BB:", l_curr_bb );       // 更新後のBB回数をシリアル通信でPCへ送る

        interrupts( );                          // 他の割り込みを許可する
}

/**
 * =======================================================
 * @fn         update_game
 * @brief      ゲーム数を更新する
 * @date        2024-06-25
 * =======================================================
 */
static void update_game( void )
{
        noInterrupts( );                        // 他の割り込みを禁止する
        interrupts( );                          // 他の割り込みを許可する
}