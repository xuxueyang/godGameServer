using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.entity.card
{
    class ConsumableCard : BaseCard
    {
        private int useCount = 0;
        public int UseCount { get => useCount; set => useCount = value; }

        public ConsumableCard(UseCard[] onUses) : base(onUses)
        {
        }


        public override void useCard(object sender, EventArgs e)
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
