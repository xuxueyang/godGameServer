using com.xxy.entity.Base.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.logic.Controller.Manager
{
    /// <summary>
    /// 场景管理
    /// </summary>
    public class MapperManager
    {
        private Dictionary<string, Mapper> mapper = new Dictionary<string, Mapper>();



        public void addMapper(string id,Mapper mapper)
        {
            this.mapper.Add(id, mapper);
        }

    }
}
