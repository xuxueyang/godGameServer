using com.xxy.entity.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace godGameServer.dao.model
{
    public class RoleModel
    {
        public int area;//所属大区编号
        public long id;
        public string name;
        public long accountId;//角色所属的账号ID
        public GameModel gameModel;//含有该游戏的属性类       
        public HashSet<string> friendSet;

        public RoleModel(string name, long id, long accountId)
        {
            initClass(name, id, accountId,null);
        }

        public RoleModel(string name, long id, long accountId,GameModel gameModel)
        {
            initClass(name, id, accountId, gameModel);
        }
        private void initClass(string name, long id, long accountId, GameModel gameModel)
        {
            this.name = name;
            this.id = id;
            this.accountId = accountId;
            friendSet = new HashSet<string>();
            if (gameModel == null)
                this.gameModel = new GameModel(accountId, id);
        }
    }
}
