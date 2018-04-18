using System.Collections.Generic;

namespace com.xxy.NetFrame
{
    class UserTokenPool
    {
        private Stack<UserToken> pool;
        public UserTokenPool(int max)
        {
            pool = new Stack<UserToken>(max);
        }

        public UserToken pop()
        {
            return pool.Pop();
        }
        public void push(UserToken token)
        {
            if (token != null)
            {
                pool.Push(token);
            }
        }
        public int Size
        {
            get
            {
                return pool.Count;
            }
        }

    }
}
