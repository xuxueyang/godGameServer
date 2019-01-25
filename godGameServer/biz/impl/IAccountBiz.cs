﻿using com.xxy.entity.model;
using com.xxy.NetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.biz
{
    interface IAccountBiz
    {
        /// <summary>
        /// 用户创建
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <return>返回协议中的创建结果 0成功 1账号重复 2账号不合法 3密码不合法</return> 
        ReturnDTO create(UserToken token, string account, string password);

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns>登陆结果 0成功  -1账号不存在 -2账号在线 -3 密码错误 -4 输入不合法</returns>
        ReturnDTO login(UserToken token, string account, string password);

        /// <summary>
        /// 客户端断开连接（下线）
        /// </summary>
        /// <param name="token"></param>
        void close(UserToken token);

        /// <summary>
        /// 获取账号ID
        /// </summary>
        /// <param name="token"></param>
        /// <returns>返回用户的登陆账号ID</returns>
        long getId(UserToken token);

    }
}
