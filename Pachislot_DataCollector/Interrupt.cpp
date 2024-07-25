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
static INTR_CALLBACK *m_Func;           // 関数ポインタ配列

// =======================================================
// ローカル関数
// =======================================================
static bool allow_intrrput( ulong32 p_WaitTime, ulong32 *p_PrevTime );
static void in_intr_occur( void );
static void out_intr_occur( void );
static void rb_intr_occur( void );
static void bb_intr_occur( void );

/**
 * =======================================================
 * @fn         Intr_Init
 * @brief      割り込み関係を初期化する
 * @date       2024-06-11
 * =======================================================
 */
void Intr_Init( INTR_CALLBACK *p_Func )
{
        m_Func = p_Func;        // 関数ポインタ配列のアドレスを受け取る

        // 3番～6番のピンを外部割込みに設定する
        // 割り込み発生は立ち下がりエッジ(FALLING)発生時とする
        attachInterrupt( digitalPinToInterrupt( IN_PIN ), in_intr_occur, FALLING );
        attachInterrupt( digitalPinToInterrupt( OUT_PIN ), out_intr_occur, FALLING );
        attachInterrupt( digitalPinToInterrupt( RB_PIN ), rb_intr_occur, CHANGE );
        attachInterrupt( digitalPinToInterrupt( BB_PIN ), bb_intr_occur, CHANGE );
}

/**
 * =======================================================
 * @fn          has_passed
 * @brief       前回の割り込みからの80ms経過判定
 * @date        2024-06-25
 * =======================================================
 */
static bool allow_intrrput( ulong32 p_WaitTime, ulong32 *p_PrevTime )
{
        ulong32 l_Interval;
        bool    l_Allow;

        l_Interval = millis( ) - *p_PrevTime;   // 前回の割込みからの経過時間を計算する

        if ( l_Interval >= p_WaitTime )         // 割込み待ち時間がINTR_WAIT[ms]を超えていたら
        {
                l_Allow     = true;
                *p_PrevTime = millis( );        // 前回時間を更新しておく
        }
        else
        {
                l_Allow = false;
        }

        return l_Allow;
}

/**
 * =======================================================
 * @fn          in_intr_occur
 * @brief       INの外部割込み
 * @date        2024-06-10
 * =======================================================
 */
static void in_intr_occur( void )
{
        static ulong32 l_INPrevtime   = 0U;        // 前回時間を初期化する
        static ulong32 l_GamePrevtime = 0U;

        if ( allow_intrrput( INTR_WAIT, &l_INPrevtime ) == true )        // 前回の割り込みから時間が十分経過していたら
        {
                m_Func[ 1 ]( );        // Func[1]：update_in()をコールバックする
        }

        if ( allow_intrrput( GAMECOUNT_WAIT, &l_GamePrevtime ) == true )        // 前回の割り込みから時間が十分経過していたら
        {
                m_Func[ 0 ]( );        // Func[0]：update_game()をコールバックする
        }
}

/**
 * =======================================================
 * @fn          out_intr_occur
 * @brief       OUTの外部割込み
 * @date        2024-06-10
 * =======================================================
 */
static void out_intr_occur( void )
{
        static ulong32 l_OUTPrevtime = 0U;

        if ( allow_intrrput( INTR_WAIT, &l_OUTPrevtime ) == true )
        {
                m_Func[ 2 ]( );        // Func[2]：update_out()をコールバックする
        }
}

/**
 * =======================================================
 * @fn          rb_intr_occur
 * @brief       RBの両エッジ外部割込み
 * @date        2024-06-26
 * =======================================================
 */
static void rb_intr_occur( void )
{
        static ulong32 l_RBPrevtime = 0U;

        if ( allow_intrrput( INTR_WAIT, &l_RBPrevtime ) == true )
        {
                if ( digitalRead( RB_PIN ) == LOW )
                {
                        m_Func[ 3 ]( );        // Func[3]：begin_rb()をコールバックする
                }
                else
                {
                        m_Func[ 4 ]( );        // Func[4]：end_rb()をコールバックする
                }
        }
}

/**
 * =======================================================
 * @fn          bb_intr_occur
 * @brief       BBの両エッジ外部割込み
 * @date        2024-06-26
 * =======================================================
 */
static void bb_intr_occur( void )
{
        static ulong32 l_BBPrevtime = 0U;

        if ( allow_intrrput( INTR_WAIT, &l_BBPrevtime ) == true )
        {
                if ( digitalRead( BB_PIN ) == LOW )
                {
                        m_Func[ 5 ]( );        // Func[5]：begin_bb()をコールバックする
                }
                else
                {
                        m_Func[ 6 ]( );        // Func[6]：end_bb()をコールバックする
                }
        }
}