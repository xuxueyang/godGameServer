﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace godGameServer.tool
{
   public class ScheduleUtil
    {
        private static ScheduleUtil util;
        Timer timer;
        private ConcurrentInteger index = new ConcurrentInteger();
        private Dictionary<int, TimeTaskModel> mission = new Dictionary<int, TimeTaskModel>();
        //移除列表
        private List<int> removelist = new List<int>();



        public static ScheduleUtil Instance()
        {
            if (util == null)
            {
                util = new ScheduleUtil();
            }
            return util;
        }

        private ScheduleUtil()
        {

            timer = new Timer(200);
            timer.Elapsed += callback;
            timer.Start();
        }
        public void callback(object sender, ElapsedEventArgs e)
        {
            lock (mission)
            {
                lock (removelist)
                {
                    foreach (var item in removelist)
                    {
                        mission.Remove(item);
                    }
                    removelist.Clear();
                    foreach (var item in mission.Values)
                    {
                        if (item.time <= DateTime.Now.Ticks)
                        {
                            item.run();
                            removelist.Add(item.id);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 任务调用毫秒
        /// </summary>
        /// <param name="task"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public int schedule(TimeEvent task, long delay)
        {
            return schedulemms(task, delay * 1000 * 1000);
        }
        /// <summary>
        /// 任务调用微妙
        /// </summary>
        /// <param name="task"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private int schedulemms(TimeEvent task, long delay)
        {
            int id = index.AddAndGet();
            TimeTaskModel model = new TimeTaskModel(id, task, delay + DateTime.Now.Ticks);
            mission.Add(id, model);
            return id;
        }

        public void removeMission(int id)
        {
            lock (removelist)
            {
                removelist.Add(id);
            }

        }

        public int schedule(TimeEvent task, DateTime time)
        {
            long t = time.Ticks - DateTime.Now.Ticks;
            t = Math.Abs(t);
            return schedulemms(task, t);
        }
        public int timeSchedule(TimeEvent task, long time)
        {
            long t = time - DateTime.Now.Ticks;
            t = Math.Abs(t);
            return schedulemms(task, t);
        }


    }
}
