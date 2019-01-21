using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.logic.Base.Card
{
    public class PermanentCard : BaseCard
    {
        public PermanentCard(string id, string name, string imgUri, int useLevel, int level, int maxLevel, bool canUp,
            Dictionary<string, float> upMaterialNeed,string message, UseCard[] onUses)
            : base(id, name, imgUri, useLevel, 1, 1, false, upMaterialNeed, message, null)
        {

        }

    }
}
