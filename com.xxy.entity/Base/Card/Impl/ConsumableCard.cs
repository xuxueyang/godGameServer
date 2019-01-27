using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.logic.Base.Card
{
    class ConsumableCard : BaseCard
    {
        private int useCount = 0;
 

        public int UseCount { get => useCount; set => useCount = value; }

        public ConsumableCard(int useCount, string id, string name, string imgUri, int useLevel, int level, int maxLevel, bool canUp,
            Dictionary<string, float> upMaterialNeed,string message, UseCard[] onUses) 
            : base(id,name,imgUri,useLevel,level,maxLevel,canUp,upMaterialNeed, message,onUses)
        {
            this.useCount = useCount;
        }



        public override void useCard(object sender, UseCardEventArgs e)
        {
            // TODO 判断有没有使用次数
            if (useCount > 0)
            {
                base.useCard(sender, e);
                useCount--;
                if (useCount < 1)
                {
                    //说明不可用
                    this.IsAvailable = false;
                }
            }
            
        }
    }
}
