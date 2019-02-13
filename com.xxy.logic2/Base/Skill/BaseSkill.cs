using System;
using System.Collections.Generic;
using com.xxy.entity.model;
using com.xxy.logic.Base.Card;

namespace com.xxy.logic.Base.Skill
{
    public class BaseSkill
    {
        private int id;
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
        public int needMp = 0;
        public BaseSkill(int id, string name, string imgUri, int useLevel, int level, int maxLevel, bool canUp,int needMp,
            Dictionary<string, float> upMaterialNeed,
            string description,
            UseSkill[] onUses)
        {
            this.id = id;
            this.name = name;
            this.imgUri = imgUri;
            this.useLevel = useLevel;
            this.needMp = needMp;
            this.level = level;
            this.maxLevel = maxLevel;
            this.canUp = canUp;
            this.upMaterialNeed = upMaterialNeed;
            this.description = description;
            if (onUses != null)
            {
                foreach (UseSkill useCard in onUses)
                {
                    this._useSkill.Add(useCard);
                }
            }
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Level { get => level; set => level = value; }
        CardSKillBattleType BattleType { get => battleType; set => battleType = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public string ImgUri { get => imgUri; set => imgUri = value; }
        public string Description { get => description; set => description = value; }


        /// <summary>
        /// 每张卡牌有
        /// </summary>
        private List<UseSkill> _useSkill = new List<UseSkill>();
        public virtual void useSkill(object sender,UseSkillEventArgs e)
        {
            if (this.IsAvailable)
            {
                if (this._useSkill!=null&&this._useSkill.Count>0)
                {
                    foreach (var item in this._useSkill)
                    {
                        ReturnDTO success = item(sender, e);
                        if (success.hasError)
                        {
                            break;
                        }
                    }
                }
                Console.WriteLine("使用了" + this.name + ",描述：" + this.description);
            }
            else
            {
                Console.WriteLine("error:卡牌不可使用");
                // throw new BaseError("","");
            }
        }

    }
}