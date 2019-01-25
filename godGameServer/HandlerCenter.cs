
using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using godGameServer.logic.login;
using godGameServer.logic.mainRoom;
using godGameServer.logic.JJCMatch;
using godGameServer.logic.BattleRoom;
using com.xxy.Protocol;

namespace godGameServer
{
    public class HandlerCenter : AbsHandlerCenter
    {
        HandlerInterface login;
        HandlerInterface mainRoom;
        HandlerInterface jjcmatch;
        HandlerInterface battleRoom;
        public HandlerCenter()
        {
            login = new loginHandler();
            mainRoom = new mainRoomHandler();
            jjcmatch = new JJCMatchHandler();
            battleRoom = new BattleRoomHandler();
        }

        public override void ClientClose(UserToken token, string error)
        {
            battleRoom.ClientClose(token, error);
            jjcmatch.ClientClose(token, error);
            mainRoom.ClientClose(token, error);
            login.ClientClose(token, error);
        }

        public override void ClientConnect(UserToken token)
        {
            battleRoom.ClientConnect(token);
            mainRoom.ClientConnect(token);
            login.ClientConnect(token);
        }

        public override void MessageReceive(UserToken token, object message)
        {
            SocketModel model = message as SocketModel;
            switch (model.type)
            {
                case TypeProtocol.TYPE_LOGIN:
                    login.MessageReceive(token, model);
                    break;
                case TypeProtocol.TYPE_MAIN_ROOM:
                    mainRoom.MessageReceive(token, model);
                    break;
                case TypeProtocol.TYPE_MATCH_JJC:
                    jjcmatch.MessageReceive(token, model);
                    break;
                case TypeProtocol.TYPE_BATTLE_ROOM:
                    battleRoom.MessageReceive(token, model);
                    break;
                default:
                    //未知模块，无视
                    break;
            }
        }
    }
}
