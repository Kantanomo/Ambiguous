using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ambiguous
{
    public class SIDTable : System.Collections.CollectionBase, System.Collections.IEnumerable
    {
        private Map Map;
        public SIDTable(Map Map)
        {
            this.Map = Map;
            for (int i = 0; i < Map.Header.SIDCount; i++) List.Add(new SIDObject());
            Map.Reader.BaseStream.Position = Map.Header.SIDIndexOffset;
            for (int i = 0; i < Map.Header.SIDCount; i++)
            {
                ((SIDObject)List[i]).Identifier = Map.Reader.ReadInt32();
                ((SIDObject)List[i]).Index = i;
            }
            Map.Reader.BaseStream.Position = Map.Header.SIDTableOffset;
            for (int i = 0; i < Map.Header.SIDCount; i++)
            {
                char c;
                while ((c = Map.Reader.ReadChar()) != '\0')((SIDObject)List[i]).Name += c;
                if (((SIDObject)List[i]).Name == null) ((SIDObject)List[i]).Name = "";
            }
        }
        public int IndetFromString(string Name)
        {
            foreach (object i in List) if (((SIDObject)i).Name == Name) return ((SIDObject)i).Identifier;
            if (Name == "Null") return -1;
            throw new Exception("Error finding the matching ident of the given string: " + Name + "\r\n Error No. 147");
        }
        public string StringFromIdent(int ident)
        {
            foreach (object i in List)if (((SIDObject)i).Identifier == ident - 0x07000000) return ((SIDObject)i).Name;
            if (ident == -1) return "Null";
            return "Uknown/External Refrence";
        }
        public void Remove(int index)
        {
            if (index > Count - 1 || index < 0) throw new Exception("Index was outside of the array \r\n Error No. 149");
            else List.RemoveAt(index);
        }
        public void Remove(SIDObject Object)
        {
            if (List.Contains(Object)) List.Remove(Object);
            else throw new Exception("Object was not found inside the array  \r\n Error No. 150");
        }
        public SIDObject this[int index]
        {
            get { return ((SIDObject)List[index]); }
            set { List[index] = value; }
        }
        public int IndexOf(SIDObject Object)
        {
            if (List.Contains(Object)) return List.IndexOf(Object);
            else return -1;
        }
    }
    public class SIDObject
    {
        public string Name;
        public int Index;
        public int Identifier;
        public int Offset;
    }

}