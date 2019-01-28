using com.xxy.entity.Base.GameRole;
using com.xxy.logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.entity.Base.Buff
{
    public class BaseBuff
    {
        public BuffType buffType;
        /// <summary>
        /// 作用于同一个物体身上
        /// </summary>
        /// <param name="baseRole"></param>
        /// <param name="baseRoleAction"></param>
        public virtual void effect(object baseRole, BaseRoleAction baseRoleAction)
        {
            //(BaseRoleData)baseRole,用到的时候再转换
        }

    }
}
