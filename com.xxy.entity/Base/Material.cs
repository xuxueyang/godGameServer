using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.logic.Base
{
    /// <summary>
    /// 材料
    /// </summary>
    public class Material
    {
        private string id;
        private string name;
        private string imgUri;
        private string message;

        public Material(string id, string name, string imgUri, string message)
        {
            this.id = id;
            this.name = name;
            this.imgUri = imgUri;
            this.message = message;
        }
    }
}
