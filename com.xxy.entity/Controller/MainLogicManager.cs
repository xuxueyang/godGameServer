using com.xxy.entity.Base.Mapper;
using com.xxy.entity.Util;
using com.xxy.logic.Controller.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.logic.Controller
{
    /// <summary>
    /// 逻辑的主控制器
    /// </summary>
    public class MainLogicManager
    {
        private MapperManager mapperManager = new MapperManager();

        /// <summary>
        /// 创建一个副本地图
        /// </summary>
        private void createPveMap()
        {
            //加载地图信息,记录地图UI TODO 需要地图信息
            Mapper mapper = new Mapper();
            mapperManager.addMapper(CommonUtil.getUUID(), mapper);
            // 初始化人物信息，包含技能、卡牌、等等   需要人物信息和技能搭配配置 和装备信息

        }
        private static MainLogicManager _mainLogicManager;
        private MainLogicManager() { }
        public MainLogicManager GetMainLogicManager()
        {
            if (_mainLogicManager == null)
                _mainLogicManager = new MainLogicManager();
            return _mainLogicManager;
        }
    }
}
