namespace com.xxy.logic.Base
{
    /// <summary>
    /// 回合阶段都type
    /// </summary>
    public enum TimeType
    {
        
    }
    /// <summary>
    /// 卡牌类型，分为战斗中和非战斗中
    /// </summary>
    public enum CardSKillBattleType: byte
    {
        BattleIn = 1,
        BattleNo = 2, // 非战斗中使用
        BattleAll = 3 // 均可
    }
}