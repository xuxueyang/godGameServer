using com.xxy.NetFrame.tool;

namespace com.xxy.NetFrame.auto
{
    public class MessageEncoding
    {
        /// <summary>
        /// 消息体序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] encode(object value)
        {
            SocketModel model = value as SocketModel;
            ByteArray ba = new ByteArray();//工具类，将数据转为二进制数组
            ba.write(model.type);
            ba.write(model.area);
            ba.write(model.command);
            if (model.message != null)
            {
                ba.write(SerialUtil.encode(model.message));
            }
            byte[] result = ba.getBuff();
            ba.Close();
            return result;
        }
        /// <summary>
        /// 消息体反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object decode(byte[] value)
        {
            ByteArray ba = new ByteArray(value);
            SocketModel model = new SocketModel();
            byte type;
            int area;
            int command;
            ba.read(out type);
            ba.read(out area);
            ba.read(out command);
            model.type = type;
            model.area = area;
            model.command = command;
            if (ba.Readnable)
            {
                byte[] message;
                ba.read(out message, ba.Length - ba.Position);
                model.message = SerialUtil.decode(message);
            }
            ba.Close();
            return model;
        }
    }
}
