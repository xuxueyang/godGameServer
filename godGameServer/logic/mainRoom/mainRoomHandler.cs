using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using godGameServer.tool;
using godGameServer.biz;
using godGameServer.logic.mainRoom.moduleClass;
using com.xxy.Protocol;
using com.xxy.Protocol.DTO.MainRoomDTO;
using com.xxy.Protocol.DTO;

namespace godGameServer.logic.mainRoom
{
    public class mainRoomHandler : HandlerInterface
    {
        IRoleBiz roleBiz = BizFactory.roleBiz;
        LoginAndRegAndGet loginAndRegAndGetHandler = new LoginAndRegAndGet();
        FriendManager friendManager = new FriendManager();
        ChatManager chatManager = new ChatManager();

        public void ClientClose(UserToken token, string error)
        {

            ExecutorPool.Instance.execute(
            delegate ()
            {
                roleBiz.offline(token);
            }
            );

        }

        public void ClientConnect(UserToken token)
        {
            //用协议来处理登陆。
        }

        public void MessageReceive(UserToken token, SocketModel message)
        {

            switch(message.command)
            {
                ///初始时登陆设定
                case MainRoomProtocol.GET_ROLE_INFO_CREQ:
                    //客户端最初登陆时获取角色信息。如果为空，客户端是需要申请登陆的
                    loginAndRegAndGetHandler.getRoleModel(token);
                    break;
                case MainRoomProtocol.REG_ROLE_CREQ:
                    loginAndRegAndGetHandler.regRole(token, message.getMessage<MainRoomLoginAndRegInfoDTO>());
                    break;
                case MainRoomProtocol.LOGIN_ROLE_CREQ:
                    loginAndRegAndGetHandler.logRole(token,message.getMessage<MainRoomLoginAndRegInfoDTO>());
                    break;

                //好友设定
                case MainRoomProtocol.FRIEND_ADD_CREQ:
                    friendManager.addFriend(token, message.getMessage<FriendManagerDTO>());
                    break;
                case MainRoomProtocol.FRIEND_DELETE_CREQ:
                    friendManager.deleteFriend(token, message.getMessage<FriendManagerDTO>());
                    break;
                //聊天模块
                case MainRoomProtocol.CHAT_BRO_CREQ:
                    chatManager.broAllPeople(token,message.getMessage<ChatInfoDTO>());
                    break;
                case MainRoomProtocol.CHAT_SOME_CREQ:
                    chatManager.broSomePeople(token, message.getMessage<ChatInfoDTO>());
                    break;
                
                
            }
        }
    }
}
