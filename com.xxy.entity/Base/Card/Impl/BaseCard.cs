using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.logic.Base.Card
{


    /// <summary>
    /// 基本的卡牌类
    /// </summary>
    public abstract class BaseCard
    {
        private string id;
        private string name;
        private CardSKillBattleType battleType;
        /// <summary>
        /// 卡牌等级
        /// </summary>
        private int level;
        /// <summary>
        /// 卡牌能不能升级
        /// </summary>
        private bool canUp;
        /// <summary>
        /// 升级一次需要的材料（ID）和对应的数目
        /// </summary>
        public Dictionary<string, float> upMaterialNeed;
        /// <summary>
        /// 卡牌最大等级
        /// </summary>
        private int maxLevel;
        /// <summary>
        /// 卡牌描述
        /// </summary>
        private string description;
        /// <summary>
        /// 人物使用卡牌的等级
        /// </summary>
        private int useLevel;
        /// <summary>
        /// 判断卡牌可不可用
        /// </summary>
        private bool isAvailable;
        private string imgUri;

        public BaseCard(string id, string name, string imgUri, int useLevel, int level, int maxLevel, bool canUp,
            Dictionary<string, float> upMaterialNeed,
            string description,
            UseCard[] onUses)
        {
            this.id = id;
            this.name = name;
            this.imgUri = imgUri;
            this.useLevel = useLevel;
            this.level = level;
            this.maxLevel = maxLevel;
            this.canUp = canUp;
            this.upMaterialNeed = upMaterialNeed;
            this.description = description;
            if (onUses != null)
            {
                foreach (UseCard useCard in onUses)
                {
                    this._useCard.Add(useCard);
                }
            }
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Level { get => level; set => level = value; }
        CardSKillBattleType BattleType { get => battleType; set => battleType = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public string ImgUri { get => imgUri; set => imgUri = value; }
        public string Description { get => description; set => description = value; }


        /// <summary>
        /// 每张卡牌有
        /// </summary>
        private List<UseCard> _useCard = new List<UseCard>();
        public virtual void useCard(object sender,UseCardEventArgs e)
        {
            if (this.IsAvailable)
            {
                foreach (var item in _useCard)
                {
                    item(sender, e);
                    Console.WriteLine("使用了" + this.name + ",描述：" + this.description);
                }
            }
            else
            {
                // throw new BaseError("","");
            }
        }

    }
}
