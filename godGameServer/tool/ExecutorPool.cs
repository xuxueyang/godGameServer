﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace godGameServer.tool
{
    /// <summary>
    /// 单线程事件委托
    /// </summary>
    public delegate void ExecutorDelegate();
    /// <summary>
    /// 单线程处理对象，将所有事物处理调用 同步此处调用
    /// </summary>
    public class ExecutorPool
    {
        private static ExecutorPool instance;
        /// <summary>
        /// 线程同步锁
        /// </summary>
        Mutex tex = new Mutex();
        public static ExecutorPool Instance
        {
            get
            {
                if (instance == null)
                    instance = new ExecutorPool();
                return instance;
            }
        }
        private ExecutorPool()
        {

        }
        public void execute(ExecutorDelegate d)
        {
            lock (this)
            {
                tex.WaitOne();
                d();
                tex.ReleaseMutex();
            }
        }
    }
}
