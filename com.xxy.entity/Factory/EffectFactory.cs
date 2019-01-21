using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.logic.Factory
{
    public class EffectFactory
    {
        /// <summary>
        ///  不同的特效等有着自己的code，通过code，初始化卡牌等等
        /// </summary>
        private Dictionary<string, Object> keyValues;
        private static EffectFactory _effectFactory;
        private EffectFactory() { }
        public EffectFactory GetEffectManager()
        {
            if (_effectFactory == null)
                _effectFactory = new EffectFactory();
            return _effectFactory;
        }
        
    }
}
