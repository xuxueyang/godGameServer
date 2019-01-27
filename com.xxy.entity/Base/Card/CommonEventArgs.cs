using System;
using com.xxy.entity.Base.GameRole;

namespace com.xxy.logic.Base.Card
{
    public class UseCardEventArgs:EventArgs
    {
        public BaseRole target;
    }
    public class UseSkillEventArgs:EventArgs
    {
        public BaseRole target;
    }
    /// <summary>
    /// 使用卡牌的实现效果
    /// </summary>
    public delegate void UseCard(object sender, UseCardEventArgs e);
    /// <summary>
    /// 使用卡牌的实现效果
    /// </summary>
    public delegate void UseSkill(object sender, UseSkillEventArgs e);
}