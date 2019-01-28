using System;
using System.Collections.Generic;
using com.xxy.entity.Base.GameRole;
using com.xxy.entity.model;

namespace com.xxy.logic.Base.Card
{
    public class UseCardEventArgs:EventArgs
    {
        public List<BaseRoleAction> targets;
    }
    public class UseSkillEventArgs:EventArgs
    {
        public List<BaseRoleAction> targets;
    }
    /// <summary>
    /// 使用卡牌的实现效果
    /// </summary>
    public delegate ReturnDTO UseCard(object sender, UseCardEventArgs e);
    /// <summary>
    /// 使用卡牌的实现效果
    /// </summary>
    public delegate ReturnDTO UseSkill(object sender, UseSkillEventArgs e);
    public delegate ReturnDTO CallBack(object sender, EventArgs args);

}