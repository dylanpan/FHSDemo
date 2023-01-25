using Chess.Base;
using Chess.Util;

namespace Chess.Component
{
    public class SkillComponent: IComponent
    {
        private int _skill_id = ConstUtil.None;
        public int skill_id
        {
            get
            {
                return _skill_id;
            }
            set
            {
                _skill_id = value;
            }
        }
        public override void LoggerString()
        {
            Console.WriteLine("---> SkillComponent:{skill_id:" + skill_id + "}");
        }
    }
}