using com.xxy.entity.Base.GameRole;
using com.xxy.logic.Base.Card;

namespace com.xxy.logic.Base.Skill.demo
{
    public class DemoSkillFactory
    {
        /// <summary>
        /// 直伤卡牌
        /// </summary>
        /// <returns></returns>
        public BaseSkill getDamangeCard(int number)
        {
            UseSkill cardEffect = (sender, args) =>
            {
                BaseRole baseRole = ((UseSkillEventArgs) args).target;
                //TODO 让target受到伤害。反射判断target有没有受伤害都脚本
//                baseRole.attacked();的动画
                baseRole.SetHp(baseRole.GetHp()-number);
                
            };
            BaseSkill card = new BaseSkill("1","炫酷火球","",1,1,1,false,null,
                "你不觉得火球作为demo很炫酷吗", new UseSkill[]{cardEffect});

            return card;               
        }
        /// <summary>
        /// 回复卡牌
        /// </summary>
        /// <returns></returns>
        public BaseSkill getRecoverCard(int number)
        {
            UseSkill cardEffect = (sender, args) =>
            {
                BaseRole baseRole = ((UseSkillEventArgs) args).target;
                //TODO 让target受到伤害。反射判断target有没有受伤害都脚本
//                baseRole.attacked();的动画
                baseRole.SetHp(baseRole.GetHp()+number);
                
            };
            BaseSkill card = new BaseSkill("1","炫酷火球","",1,1,1,false,null,
                "你不觉得火球作为demo很炫酷吗", new UseSkill[]{cardEffect});

            return card;   
        }
    }
}