using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace godGameServer.tool
{
    class ConcurrentInteger
    {
        int value;
        Mutex tex = new Mutex();
        public ConcurrentInteger()
        {

        }
        public ConcurrentInteger(int value)
        {
            this.value = value;

        }
        public int AddAndGet()
        {
            lock (this)
            {
                //纯lock似乎无法保证安全？？？
                tex.WaitOne();
                ++value;
                tex.ReleaseMutex();
                return value;
            }
        }
        public int ReduceAndGet()
        {
            lock (this)
            {
                //纯lock似乎无法保证安全？？？
                tex.WaitOne();
                --value;
                tex.ReleaseMutex();
                return value;
            }
        }
        public void reset()
        {
            lock (this)
            {
                tex.WaitOne();
                value = 0;
                tex.ReleaseMutex();
            }
        }
        public int get()
        {
            return value;
        }
    }
}
