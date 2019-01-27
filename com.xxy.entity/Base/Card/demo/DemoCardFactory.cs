using com.xxy.entity.Base.GameRole;

namespace com.xxy.logic.Base.Card.demo
{
    public class DemoCardFactory
    {
        /// <summary>
        /// 直伤卡牌
        /// </summary>
        /// <returns></returns>
        public BaseCard getDamangeCard(int number)
        {
            UseCard cardEffect = (sender, args) =>
            {
                BaseRole baseRole = ((UseCardEventArgs) args).target;
                //TODO 让target受到伤害。反射判断target有没有受伤害都脚本
//                baseRole.attacked();的动画
                baseRole.SetHp(baseRole.GetHp()-number);
                
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
                BaseRole baseRole = ((UseCardEventArgs) args).target;
                //TODO 让target受到伤害。反射判断target有没有受伤害都脚本
//                baseRole.attacked();的动画
                baseRole.SetHp(baseRole.GetHp()+number);
                
            };
            PermanentCard card = new PermanentCard("1","炫酷火球","",1,1,1,false,null,
                "你不觉得火球作为demo很炫酷吗", new UseCard[]{cardEffect});

            return card;   
        }
        
    }
}