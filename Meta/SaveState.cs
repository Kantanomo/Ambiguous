using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Ambiguous
{
    public static class SaveState
    {
        public static List<object> List = new List<object>();
        public static void Add(State State)
        {
            
            bool Check = false;
            foreach (object i in List)
                if (((State)i).Offset == State.Offset)
                {
                    List[List.IndexOf(i)] = State;
                    Check = true;
                    break;
                }
            if (!Check)
                List.Add(State);
        }
        public static State GetState(int Offset)
        {
                foreach (object i in List)
                    if (((State)i).Offset == Offset)
                        return ((State)i);
                return new State();
        }
    }
    public class State
    {
        public int Offset;
        public object[] value;
        public Type ValueType;
        public Type MetaType;
        public State()
        {
        }
        public State(int Offset, object[] value, Type ValueType, Type MetaType)
        {
            this.Offset = Offset;
            this.value = value;
            this.ValueType = ValueType;
            this.MetaType = MetaType;
        }
    }
}
