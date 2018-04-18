using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.NetFrame;
using godGameServer.dao.model;

namespace godGameServer.biz
{
    public interface IRoleBiz
    {

        /// <summary>
        /// 创建召唤师
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        string Create(UserToken token, string name);

        /// <summary>
        /// 获取连接对应的用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        RoleModel GetModel(UserToken token);

        /// <summary>
        /// 通过id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleModel GetModel(long id);

        /// <summary>
        /// 用户上线
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        RoleModel Online(UserToken token);

        /// <summary>
        /// 用户下线
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        void offline(UserToken token);

        /// <summary>
        /// 通过好友id获取连接（可以用于私聊）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserToken getToken(long id);
        /// <summary>
        /// 判断是否存在该用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        bool isHasSameNameRole(string name, int area);
        RoleModel getModelByName(int area, string name);

    }
}
