using com.xxy.entity.Base.GameRole;
using com.xxy.entity.model;
using com.xxy.Protocol;
using System.Collections.Generic;

namespace com.xxy.logic.Base.Card.demo
{
    public class DemoCardFactory
    {
        public static DemoCardFactory Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DemoCardFactory();
                return _Instance;
            }
        }
        private static DemoCardFactory _Instance;
        private DemoCardFactory() { }
        /// <summary>
        /// 直伤卡牌
        /// </summary>
        /// <returns></returns>
        public BaseCard getDamangeCard(int number)
        {
            UseCard cardEffect = (sender, args) =>
            {
                List<BaseRoleAction> baseRoles = ((UseCardEventArgs) args).targets;
                var baseRole =  baseRoles[0];
                //TODO 让target受到伤害。反射判断target有没有受伤害都脚本
                //                baseRole.attacked();的动画
                baseRole.SetHp(baseRole.GetHp()-number);
                return new ReturnDTO(RETURN_CODE.SUCCESS);
            };
            PermanentCard card = new PermanentCard("1","炫酷火球","",1,1,1,false,null,
                "你不觉得火球作为demo很炫酷吗", new UseCard[]{cardEffect});

            return card;
        }
        /// <summary>
        /// 回复卡牌
        /// </summary>
        /// <returns></returns>
        public BaseCard getRecoverCard(int number)
        {
            UseCard cardEffect = (sender, args) =>
            {
                BaseRoleAction baseRole = (BaseRoleAction)sender;
                //TODO 让target受到伤害。反射判断target有没有受伤害都脚本
//                baseRole.attacked();的动画
                baseRole.SetHp(baseRole.GetHp()+number);
                return new ReturnDTO(RETURN_CODE.SUCCESS);
            };
            PermanentCard card = new PermanentCard("2","回复术","",1,1,1,false,null,
                "没有什么是钱解决不了的问题", new UseCard[]{cardEffect});

            return card;   
        }
        
    }
}