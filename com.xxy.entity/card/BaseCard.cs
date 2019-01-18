using com.xxy.entity.errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.entity.card
{
    /// <summary>
    /// 卡牌类型，分为战斗中和非战斗中
    /// </summary>
    public enum CardBattleType: byte
    {
        BattleIn = 1,
        BattleNo = 2, // 非战斗中使用
        BattleAll = 3 // 均可
    }
    /// <summary>
    /// 使用卡牌的实现效果
    /// </summary>
    public delegate void UseCard(object sender, EventArgs e);
    /// <summary>
    /// 基本的卡牌类
    /// </summary>
    abstract class BaseCard
    {
        private string id;
        private string name;
        private CardBattleType battleType;
        /// <summary>
        /// 卡牌等级
        /// </summary>
        private int level;
        /// <summary>
        /// 人物使用卡牌的等级
        /// </summary>
        private int useLevel;
        /// <summary>
        /// 判断卡牌可不可用
        /// </summary>
        private bool isAvailable;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Level { get => level; set => level = value; }
        CardBattleType BattleType { get => battleType; set => battleType = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }


        /// <summary>
        /// 每张卡牌有
        /// </summary>
        private UseCard _useCard;
        public virtual void useCard(object sender,EventArgs e)
        {
            if (this.IsAvailable)
            {
                this._useCard(sender, e);
            }
            else
            {
                // throw new BaseError("","");
            }
        }
        public BaseCard(UseCard[] onUses)
        {
            foreach(UseCard useCard in onUses)
            {
                this._useCard += useCard;
            }
        }
    }
}
