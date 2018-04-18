using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.tool
{
    public delegate void TimeEvent();
    public class TimeTaskModel
    {
        /// <summary>
        /// 任务逻辑
        /// </summary>
        private TimeEvent execut;
        /// <summary>
        /// 任务执行的时间
        /// </summary>
        public long time;
        /// <summary>
        /// 任务ID
        /// </summary>
        public int id;
        public TimeTaskModel(int id, TimeEvent execut, long time)
        {
            this.id = id;
            this.execut = execut;
            this.time = time;
        }
        public void run()
        {
            execut();
        }
    }
}
