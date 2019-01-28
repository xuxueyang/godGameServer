using com.xxy.entity.Base.GameRole;
using com.xxy.entity.model;
using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Errors;
using com.xxy.Protocol;
using System;
using System.Collections.Generic;

namespace com.xxy.logic.Base.Skill.demo
{
    public class DemoSkillFactory
    {
        public static DemoSkillFactory Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DemoSkillFactory();
                return _Instance;
            }
        }
        private static DemoSkillFactory _Instance;
        private DemoSkillFactory() { }
        /// <summary>
        /// 直伤卡牌
        /// </summary>
        /// <returns></returns>
        public BaseSkill getDamangeSkill(int number)
        {
            //还要消耗栏
            UseSkill cardEffect1 = (sender, args) =>
            {
                BaseRoleAction baseRole = (BaseRoleAction)sender;
                if (baseRole.GetMp() >= number)
                {
                    baseRole.SetMp(baseRole.GetMp() - number);
                    return new ReturnDTO(RETURN_CODE.SUCCESS);
                }
                else
                {   //打断
                    Console.WriteLine("法力值不足");
                    return new ReturnDTO(RETURN_CODE.BATTLE_HAS_NO_MP);
                }    
            };
            UseSkill cardEffect2 = (sender, args) =>
            {
                List<BaseRoleAction> baseRoles = ((UseSkillEventArgs)args).targets;
                var baseRole = baseRoles[0];
                //TODO 让target受到伤害。反射判断target有没有受伤害都脚本
                //                baseRole.attacked();的动画
                baseRole.SetHp(baseRole.GetHp()-number);
                return new ReturnDTO(RETURN_CODE.SUCCESS);

            };
            BaseSkill card = new BaseSkill("1","炫酷火球","",1,1,1,false,null,
                "你不觉得火球作为demo很炫酷吗", new UseSkill[]{cardEffect1, cardEffect2});
            card.IsAvailable = true;
            return card;               
        }
        /// <summary>
        /// 回复卡牌
        /// </summary>
        /// <returns></returns>
        public BaseSkill getRecoverSkill(int number)
        {
            //还要消耗栏
            UseSkill cardEffect1 = (sender, args) =>
            {
                BaseRoleAction baseRole = (BaseRoleAction)sender;
                if (baseRole.GetMp() >= number)
                {
                    baseRole.SetMp(baseRole.GetMp() - number);
                    return new ReturnDTO(RETURN_CODE.SUCCESS);
                }
                else
                {   //打断
                    Console.WriteLine("法力值不足");
                    return new ReturnDTO(RETURN_CODE.BATTLE_HAS_NO_MP);
                }
            };
            UseSkill cardEffect2 = (sender, args) =>
            {
                BaseRoleAction baseRole = ((BaseRoleAction)sender);
                //TODO 让target受到伤害。反射判断target有没有受伤害都脚本
//                baseRole.attacked();的动画
                baseRole.SetHp(baseRole.GetHp()+number);
                return new ReturnDTO(RETURN_CODE.SUCCESS);
            };
            BaseSkill card = new BaseSkill("1", "回复术", "",1,1,1,false,null,
                "没有什么是钱解决不了的问题", new UseSkill[]{ cardEffect1 , cardEffect2});
            card.IsAvailable = true;
            return card;   
        }
    }
}