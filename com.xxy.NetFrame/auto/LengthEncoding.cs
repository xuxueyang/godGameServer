using System;
using System.Collections.Generic;
using System.IO;

namespace com.xxy.NetFrame.auto
{
    public class LengthEncoding
    {
        /// <summary>
        /// 粘包长度编码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static byte[] encode(byte[] buff)
        {
            MemoryStream ms = new MemoryStream();//创建内存流对象
            BinaryWriter sw = new BinaryWriter(ms);//写入二进制对象流
            sw.Write(buff.Length);//int4个字节
            sw.Write(buff);
            byte[] result = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int)ms.Length);
            sw.Close();
            ms.Close();
            return result;
        }
        /// <summary>
        /// 粘包长度解码
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        public static byte[] decode(ref List<byte> cache)
        {
            if (cache.Count < 4) return null;
            MemoryStream ms = new MemoryStream(cache.ToArray());
            BinaryReader br = new BinaryReader(ms);
            int lenght = br.ReadInt32();//从缓存中读取int的消息长度
            if (lenght > ms.Length - ms.Position)
            {
                return null;
            }
            byte[] result = br.ReadBytes(lenght);
            cache.Clear();
            cache.AddRange(br.ReadBytes((int)(ms.Length - ms.Position)));
            br.Close();
            ms.Close();
            return result;
        }
    }
}
