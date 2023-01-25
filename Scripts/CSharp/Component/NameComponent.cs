using Chess.Base;
using Chess.Util;

namespace Chess.Component
{
    public class NameComponent: IComponent
    {
        private string _name = ConstUtil.Empty;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        private int _id = ConstUtil.Zero;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public override void LoggerString()
        {
            Console.WriteLine("---> NameComponent:{name:" + name + ", id:" + id + "}");
        }
    }
}