using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.logic.Effect
{
    public interface RecoverInterface
    {
        void RecoverSelf(float num);
    }
    public class RecoverEventArgs : EventArgs
    {
        /// <summary>
        /// 回复的目标
        /// </summary>
        public RecoverInterface target;
        /// <summary>
        /// 回复的数值
        /// </summary>
        public float num;
        public RecoverEventArgs(RecoverInterface target,float num)
        {
            this.target = target;
            this.num = num;
        }
    }
    /// <summary>
    /// 提供回复类的函数(如果恢复值为复数，就是伤害了）
    /// </summary>
    public class Recover
    {
        public static void RecoverSelf(Object sender, RecoverEventArgs e)
        {
            e.target.RecoverSelf(e.num);
        }
        public static void RecoverSelf(Object sender, EventArgs _e)
        {
            RecoverEventArgs e = (RecoverEventArgs)_e;
            e.target.RecoverSelf(e.num);
        }
    }
}
