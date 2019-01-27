using System;
using System.Collections.Generic;
using com.xxy.logic.Base.Card;

namespace com.xxy.logic.Base.Skill
{
    public class BaseSkill
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
        /// <summary>
        /// 使用都回合阶段
        /// </summary>
        private TimeType type;
        public BaseSkill(string id, string name, string imgUri, int useLevel, int level, int maxLevel, bool canUp,
            Dictionary<string, float> upMaterialNeed,
            string description,
            UseSkill[] onUses)
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
                foreach (UseSkill useCard in onUses)
                {
                    this._useSkill += useCard;
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
        private UseSkill _useSkill;
        public virtual void useSkill(object sender,UseSkillEventArgs e)
        {
            if (this.IsAvailable)
            {
                this._useSkill(sender, e);
            }
            else
            {
                // throw new BaseError("","");
            }
        }

    }
}