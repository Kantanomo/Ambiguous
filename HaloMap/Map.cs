using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Ambiguous
{
    public class Map
    {
        public string Path;
        private bool Loaded;
        public FileStream FS;
        public BinaryReader Reader;
        public BinaryWriter Writer;
        public MapHeader Header;
        public MapIndex Index;
        public SIDTable StringTable;
        public TagTable Tags;
        public Map(string Path, bool Loaded)
        {
            this.Path = Path;
            this.Loaded = Loaded;
            if (!Loaded)
            {
                FS = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                Reader = new BinaryReader(FS);
                Writer = new BinaryWriter(FS);
            }
            else
            {
                FS = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
                Reader = new BinaryReader(FS);
                Writer = new BinaryWriter(new FileStream(Environment.GetLogicalDrives()[0] + "\\Data.Ambiguous", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
            }
            Header = MapHeader.GetHeader(Reader);
            FS.Flush();
            Index = MapIndex.GetMapIndex(Reader, Header);
            FS.Flush();
            StringTable = new SIDTable(this);
            FS.Flush();
            Tags = new TagTable(this);
            if (Header.MapName.Trim('\0') == "shared")
                Index.SecondaryMagic += 0x3C000;
        }
    }
}
