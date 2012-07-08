using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ambiguous
{
    public class TagTable : System.Collections.CollectionBase, System.Collections.IEnumerable
    {
        public List<string> TagTypes = new List<string>();
        private Map Map;
        public TagTable(Map Map)
        {
            this.Map = Map;
            bool Go = true;
            int tmp = 1;
            Map.Reader.BaseStream.Position = Map.Index.TagStart;
            while (Go)
            {
                if (this.Count == 0)
                {
                    byte[] Buffer = new byte[] { 0xFF, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    byte[] Buffer1 = Map.Reader.ReadBytes(16);
                    while (Buffer1[0] == Buffer[0] & Buffer1[1] == Buffer[1] & Buffer[2] == Buffer1[2])
                    {
                        Buffer1 = Map.Reader.ReadBytes(16);
                        tmp += 1;
                    }
                    Map.Reader.BaseStream.Position -= 16;
                    tmp -= 1;
                }
                else
                {
                    byte[] Buffer = new byte[] { 0xFF, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    byte[] Buffer1 = Map.Reader.ReadBytes(16);
                    if (Buffer1[0] == Buffer[0] && Buffer1[1] == Buffer[1] && Buffer1[2] == Buffer[2])
                        break;
                    else
                        Map.Reader.BaseStream.Position -= 16;
                }
                TagObject t = new TagObject();
                t.Class = Globals.ReverseStr(new string(Map.Reader.ReadChars(4)));
                t.Identifier = Map.Reader.ReadInt32();
                t.RawMetaOffset = Map.Reader.ReadInt32();
                t.MetaSize = Map.Reader.ReadInt32();
                t.MetaOffset = t.RawMetaOffset - Map.Index.SecondaryMagic;
                t.Index = tmp;
                tmp += 1;
                if (this.Count < 15065 && (uint)t.Identifier != 0xFFFFFFFF && tmp < 15065)
                {
                    List.Add(t);
                    if (!this.TagTypes.Contains(t.Class)) this.TagTypes.Add(t.Class);
                }
                if (tmp == 15064 || t.Class == "ugh!") Go = false;
            }
            Map.Header.TagCount = this.Count;
            Map.Reader.BaseStream.Position = Map.Header.FileTableOffset;
            for (int i = 0; i < this.Count; i++)
            {
                char c;
                while ((c = Map.Reader.ReadChar()) != '\0') this[i].Path += c;
            }
        }
        public TagObject this[int index]
        {
            get { return ((TagObject)List[index]); }
            set { List[index] = value; }
        }
        public TagObject this[string path, string Class]
        {
            get
            {
                foreach (object tag in List)
                    if (((TagObject)tag).Path == path && ((TagObject)tag).Class == Class)
                        return ((TagObject)tag);
                return new TagObject();
            }
        }
        public int IdentFromName(string Name)
        {
            if (Name != "Null")
            {
                for (int i = 0; i < List.Count; i++)
                    if (((TagObject)List[i]).Path == Name)
                        return this[i].Identifier;
                throw new Exception("The given tag name was not found inside of the array \r\n Error No. 142");
            }
            return -1;
        }
        public string NameFromIdent(int ident)
        {
            if (ident != -1)
            {
                for (int i = 0; i < List.Count; i++)
                    if (((TagObject)List[i]).Identifier == ident)
                        return this[i].Path;
                return "Not Found/External Refrence";
                //throw new Exception("The given tag name was not found inside of the array \r\n Error No. 142.1");
            }
            return "Null";
        }
        public int IndexOfIdentifier(int Identifier)
        { 
            for (int i = 0; i < List.Count; i++)
                if (((TagObject)List[i]).Identifier == Identifier) return i;
            throw new Exception("The given tag identifier was not found inside of the array \r\n Error No. 143");
        }
        public int IndexOfName(string Name)
        {
            for (int i = 0; i < List.Count; i++)
                if (((TagObject)List[i]).Path == Name) return i;
            throw new Exception("The given tag name was not found inside of the array \r\n Error No. 144");
        }
        public void Remove(int index)
        {
            if (index > Count - 1 || index < 0) throw new Exception("The given index was not inside of the array \r\n Error No. 145");
            else List.RemoveAt(index);
        }
        public void Remove(TagObject Tag)
        {
            if (List.Contains(Tag)) List.Remove(Tag);
            else throw new Exception("The given tag was not found inside of the array \r\n Error No. 146");
        }
        public TagObject[] GetTagsByClass(string Class)
        {
            List<TagObject> T = new List<TagObject>();
            foreach (object t in List) if (((TagObject)t).Class == Class) T.Add(((TagObject)t));
            return T.ToArray();
        }
        public int IndexOf(TagObject Tag)
        {
            return List.IndexOf(Tag);
        }
    }
    public class TagObject
    {
        public string Class;
        public string Path;
        public int Identifier;
        public int RawMetaOffset;
        public int MetaSize;
        public int MetaOffset;
        public int Index;
    }
}
