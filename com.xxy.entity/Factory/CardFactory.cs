using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Errors;
using com.xxy.logic.Effect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.logic.Factory
{
    public class CardFactory
    {
        /// <summary>
        /// 得到消耗回复类的卡牌
        /// </summary>
        /// <returns></returns>
        public BaseCard getConsumableRecoverCardTemplate()
        {
            ConsumableCard consumable = new ConsumableCard(1, "id", "name", "imgUri", 0, 0, 0, true, null, "message", new UseCard[] { Recover.RecoverSelf });
            return consumable;
        }

        public BaseCard getCardTemplateByCode(String code)
        {
            switch (code)
            {
                case "getConsumableRecoverCardTemplate":
                    return getConsumableRecoverCardTemplate();
                default:
                    // return new BaseCard("", "", "", 1, 1, 1, false, null, "", null);
                    throw new BaseError(Code.NotCardTemplate, "没有发现初始化的卡牌！");
            }

        }


        private static CardFactory _cardFactory;
        private CardFactory() { }
        public CardFactory GetEffectManager()
        {
            if (_cardFactory == null)
                _cardFactory = new CardFactory();
            return _cardFactory;
        }
    }
}
