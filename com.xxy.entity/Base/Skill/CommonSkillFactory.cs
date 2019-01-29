using com.xxy.entity.Base.GameRole;
using com.xxy.entity.model;
using com.xxy.logic.Base.Card;
using com.xxy.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.xxy.entity.Base.Skill
{
    /// <summary>
    /// 得到一些常用判断组件
    /// </summary>
    public class CommonSkillFactory
    {
        private CommonSkillFactory() { }
        public static UseSkill getSkillWhenNoSkill()
        {
            UseSkill boming = (sender, args) =>
            {
                BaseRoleAction source = (BaseRoleAction)sender;
                List<BaseRoleAction> baseRoles = ((UseSkillEventArgs)args).targets;
                var baseRole = baseRoles[0];
                //TODO 让target受到伤害。反射判断target有没有受伤害都脚本
                //                baseRole.attacked();的动画
                var hp = (int)Math.Round(source.GetMaxHp() * 0.1,0);
                baseRole.SetHp(baseRole.GetHp() - hp);
                source.SetHp(source.GetHp() - hp);
                return new ReturnDTO(RETURN_CODE.SUCCESS);
            };
            return boming;
        }
        public static UseSkill checkMpIsEnough(int mp)
        {
            //还要消耗栏
            UseSkill cardEffect1 = (sender, args) =>
            {
                BaseRoleAction baseRole = (BaseRoleAction)sender;
                if (baseRole.GetMp() >= mp)
                {
                    baseRole.SetMp(baseRole.GetMp() - mp);
                    return new ReturnDTO(RETURN_CODE.SUCCESS);
                }
                else
                {   //打断
                    Console.WriteLine("法力值不足");
                    return new ReturnDTO(RETURN_CODE.BATTLE_HAS_NO_MP);
                }
            };
            return cardEffect1;
        }
    }
}
