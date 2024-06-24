/**
 * =============================================================
 * @file        Interrupt.cpp
 * @author      kinketsu patron (https://kinketsu-patron.com)
 * @brief       Setting interrupt functions
 * @version     1.0
 * @date        2024-06-10
 * =============================================================
 */

// =======================================================
// ヘッダファイルインクルード
// =======================================================
#include "inc/Interrupt.h"

// =======================================================
// ローカル変数
// =======================================================
static ulong64 mIN_PrevTime;
static ulong64 mOUT_PrevTime;
static ulong64 mRB_PrevTime;
static ulong64 mBB_PrevTime;

// =======================================================
// ローカル関数
// =======================================================
static void in_intr_occur ( void );
static void out_intr_occur( void );
static void rb_intr_occur ( void );
static void bb_intr_occur ( void );

/**
 * =======================================================
 * @fn         Intr_Init
 * @brief      割り込み関係を初期化する
 * @date       2024-06-11
 * =======================================================
 */
void Intr_Init( void )
{
        // 3番～6番のピンを外部割込みに設定する
        // 割り込み発生は立ち下がりエッジ(FALLING)発生時とする
        attachInterrupt( digitalPinToInterrupt( IN_PIN ),  in_intr_occur,  FALLING );
        attachInterrupt( digitalPinToInterrupt( OUT_PIN ), out_intr_occur, FALLING );
        attachInterrupt( digitalPinToInterrupt( RB_PIN ),  rb_intr_occur,  FALLING );
        attachInterrupt( digitalPinToInterrupt( BB_PIN ),  bb_intr_occur,  FALLING );
        mIN_PrevTime  = 0U;
        mOUT_PrevTime = 0U;
        mRB_PrevTime  = 0U;
        mBB_PrevTime  = 0U;
}

/**
 * =======================================================
 * @fn          in_intr_occur
 * @brief       INの立ち下がりエッジ発生時にINを+1する
 * @date        2024-06-10
 * =======================================================
 */
static void in_intr_occur( void )
{
        ulong64 l_interval;
        uint32 l_curr_in;

        l_interval = millis() - mIN_PrevTime;           // 前回の割込みからの経過時間を計算する

        if ( l_interval >= INTR_WAIT )                  // 割込み待ち時間がINTR_WAIT[ms]を超えていたら
        {
                noInterrupts( );                        // 他の割り込みを禁止する

                l_curr_in = Get_IN_Coin( );             // IN枚数を取得する
                l_curr_in++;                            // 現在のIN枚数に+1する
                Set_IN_Coin( l_curr_in );               // IN枚数を更新する
                Serial_Write( "IN_COIN:", l_curr_in );  // 更新後のIN枚数をシリアル通信でPCへ送る

                mIN_PrevTime = millis( );               // 割込み実施時間を更新する

                interrupts( );                          // 他の割り込みを許可する
        }
}

/**
 * =======================================================
 * @fn          out_intr_occur
 * @brief       OUTの立ち下がりエッジ発生時にOUTを+1する
 * @date        2024-06-10
 * =======================================================
 */
static void out_intr_occur( void )
{
        ulong64 l_interval;
        uint32 l_curr_out;

        l_interval = millis() - mOUT_PrevTime;          // 前回の割込みからの経過時間を計算する

        if ( l_interval >= INTR_WAIT )                  // 割込み待ち時間がINTR_WAIT[ms]を超えていたら
        {
                noInterrupts( );                        // 他の割り込みを禁止する

                l_curr_out = Get_OUT_Coin( );           // OUT枚数を取得する
                l_curr_out++;                           // 現在のOUT枚数に+1する
                Set_OUT_Coin( l_curr_out );             // OUT枚数を更新する
                Serial_Write( "OUT_COIN:", l_curr_out );// 更新後のOUT枚数をシリアル通信でPCへ送る

                mOUT_PrevTime = millis( );              // 割込み実施時間を更新する

                interrupts( );                          // 他の割り込みを許可する
        }
}

/**
 * =======================================================
 * @fn          rb_intr_occur
 * @brief       RBの立ち下がりエッジ発生時にRBを+1する
 * @date        2024-06-10
 * =======================================================
 */
static void rb_intr_occur( void )
{
        ulong64 l_interval;
        uint32 l_curr_rb;

        l_interval = millis() - mRB_PrevTime;           // 前回の割込みからの経過時間を計算する

        if ( l_interval >= INTR_WAIT )                  // 割込み待ち時間がINTR_WAIT[ms]を超えていたら
        {
                noInterrupts( );                        // 他の割り込みを禁止する

                l_curr_rb = Get_RB( );                  // RB回数を取得する
                l_curr_rb++;                            // 現在のRB回数に+1する
                Set_RB( l_curr_rb );                    // RB回数を更新する
                Serial_Write( "RB:", l_curr_rb );       // 更新後のRB回数をシリアル通信でPCへ送る

                mRB_PrevTime = millis( );               // 割込み実施時間を更新する

                interrupts( );                          // 他の割り込みを許可する
        }
}

/**
 * =======================================================
 * @fn          bb_intr_occur
 * @brief       BBの立ち下がりエッジ発生時にBBを+1する
 * @date        2024-06-10
 * =======================================================
 */
static void bb_intr_occur( void )
{
        ulong64 l_interval;
        uint32 l_curr_bb;

        l_interval = millis() - mBB_PrevTime;           // 前回の割込みからの経過時間を計算する

        if ( l_interval >= INTR_WAIT )                  // 割込み待ち時間がINTR_WAIT[ms]を超えていたら
        {
                noInterrupts( );                        // 他の割り込みを禁止する

                l_curr_bb = Get_BB( );                  // BB回数を取得する
                l_curr_bb++;                            // 現在のBB回数に+1する
                Set_BB( l_curr_bb );                    // BB回数を更新する
                Serial_Write( "BB:", l_curr_bb );       // 更新後のBB回数をシリアル通信でPCへ送る

                mBB_PrevTime = millis( );               // 割込み実施時間を更新する

                interrupts( );                          // 他の割り込みを許可する
        }
}