using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace com.xxy.NetFrame.tool
{
    /// <summary>
    /// 序列化工具
    /// </summary>
    public class SerialUtil
    {
        /// <summary>
        /// 对象序列化
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] encode(object message)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();//二进制序列化对象
            //将obj对象序列化成二进制数据，写入到内存流
            bf.Serialize(ms, message);
            byte[] result = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int)ms.Length);
            ms.Close();
            return result;
        }
        /// <summary>
        /// 对象反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object decode(byte[] value)
        {
            MemoryStream ms = new MemoryStream(value);
            BinaryFormatter bf = new BinaryFormatter();
            //反序列化
            object message = bf.Deserialize(ms);
            ms.Close();
            return message;
        }
    }
}
