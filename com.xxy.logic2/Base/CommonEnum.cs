namespace com.xxy.logic.Base
{
    /// <summary>
    /// 回合阶段都type
    /// </summary>
    public enum BattleTimeType
    {
        _PRE_START,
        START,//开始阶段
        PRE,//准备阶段
        BATTLE,//战斗阶段
        NEXT,//预结束阶段
        OVER,//结束阶段
        _GAME_OVER // 游戏结束
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

    public enum RoleType
    {
        NPC,//npc的角色，电脑控制
        PLAYER//玩家
    }
    public enum RoomType
    {
        ONE_NPC_ROOM,//自己一个人和npc的战斗房间
        DEMO_NPC,//npc之间的对战
        TWO_PLAYER//两个玩家之间的对战
    }
    public enum BuffType
    {
        ONE_EFFECT,//一次性作用的
        EACH_TIME_EFFECT,//每回合作用的
    }
}