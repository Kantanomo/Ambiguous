using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Ambiguous
{
    public class MapHeader
    {
        [Category("Header")]
        [Description("Blam engine version which this map was compiled for")]
        public int EngineBuildVersion { get; set; }
        [Category("Header")]
        [Description("The size of the map file")]
        public int MapFileSize { get; set; }
        [Category("Header")]
        [Description("The Offset to the Tag Index")]
        public int IndexOffset { get; set; }
        [Category("Header")]
        [Description("The total size of the index")]
        public int IndexSize { get; set; }
        [Category("Header")]
        [Description("The total size of the size of the meta table")]
        public int MetaTableSize { get; set; }
        [Category("Header")]
        [Description("The total size of the Non Raw data")]
        public int UnRawSize { get; set; }
        [Category("Header")]
        [Description("The date which the map was built on")]
        public string BuildDate { get; set; }
        [Category("Header")]
        [Description("The name of the map")]
        public string MapName { get; set; }
        [Category("Header")]
        [Description("The name of the scenario")]
        public string Scenario { get; set; }
        [Category("Header")]
        [Description("The total size of the index")]
        public int SIDMetaTableOffset { get; set; }
        [Category("Header")]
        [Description("The total could of String items")]
        public int SIDCount { get; set; }
        [Category("Header")]
        [Description("The total size of the String item table")]
        public int SIDTableSize { get; set; }
        [Category("Header")]
        [Description("The offset to the String item index")]
        public int SIDIndexOffset { get; set; }
        [Category("Header")]
        [Description("The offset to the String item Table")]
        public int SIDTableOffset { get; set; }
        [Category("Header")]
        [Description("The total amount of tags in the tag table")]
        public int TagCount { get; set; } //Always 15065 (Was broke during port to PC Fucking Microsoft)
        [Category("Header")]
        [Description("The offset to the file table")]
        public int FileTableOffset { get; set; }
        [Category("Header")]
        [Description("The total size of the File table")]
        public int FileTableSize { get; set; }
        [Category("Header")]
        [Description("The offset to the file table index")]
        public int FileTableIndexOffset { get; set; }
        [Category("Header")]
        [Description("The offset to the raw model table")]
        public int ModelRawTableStart { get; set; }
        [Category("Header")]
        [Description("The total size of the raw model table")]
        public int ModelRawTableSize { get; set; }
        [Category("Header")]
        [Description("The checksum value of the map")]
        public int Checksum { get; set; }
        public static MapHeader GetHeader(BinaryReader Reader)
        {
            MapHeader Base = new MapHeader();
            Reader.BaseStream.Position = 4;
            Base.EngineBuildVersion = Reader.ReadInt32();
            if (Base.EngineBuildVersion != 8)
                throw new Exception("Not a Halo 2 Map Stupid.");
            Base.MapFileSize = Reader.ReadInt32();
            Reader.BaseStream.Position += 4;
            Base.IndexOffset = Reader.ReadInt32();
            Base.IndexSize = Reader.ReadInt32();
            Base.MetaTableSize = Reader.ReadInt32();
            Base.UnRawSize = Reader.ReadInt32();
            Reader.BaseStream.Position = 300;
            Base.BuildDate = new string(Reader.ReadChars(32));
            Reader.BaseStream.Position = 420;
            Base.MapName = new string(Reader.ReadChars(36));
            Base.Scenario = new string(Reader.ReadChars(80));
            Reader.BaseStream.Position = 364;
            Base.SIDMetaTableOffset = Reader.ReadInt32();
            Base.SIDCount = Reader.ReadInt32();
            Base.SIDTableSize = Reader.ReadInt32();
            Base.SIDIndexOffset = Reader.ReadInt32();
            Base.SIDTableOffset = Reader.ReadInt32();
            Reader.BaseStream.Position = 716;
            Base.TagCount = Reader.ReadInt32();
            Base.FileTableOffset = Reader.ReadInt32();
            Base.FileTableSize = Reader.ReadInt32();
            Base.FileTableIndexOffset = Reader.ReadInt32();
            Reader.BaseStream.Position = 744;
            Base.ModelRawTableStart = Reader.ReadInt32();
            Base.ModelRawTableSize = Reader.ReadInt32();
            Base.Checksum = Reader.ReadInt32();
            return Base;
        }
    }
    public class MapIndex
    {
        [Category("Index")]
        [Description("The total size Index header")]
        public int IndexHeaderSize { get; set; }
        [Category("Index")]
        [Description("The offset to the first tag instance")]
        public int TagStart { get; set; }
        [Category("Index")]
        [Description("The Primary Address Modifier (I don't even know if it's used in this engine)")]
        public int PrimaryMagic { get; set; }
        [Category("Index")]
        [Description("The Secondary Address Modifier (Main Address Modifier in this engine)")]
        public int SecondaryMagic { get; set; }
        public static MapIndex GetMapIndex(BinaryReader Reader, MapHeader Header)
        {
            MapIndex Base = new MapIndex();
            Reader.BaseStream.Position = Header.IndexOffset;
            Base.IndexHeaderSize = Reader.ReadInt32();
            Reader.ReadInt32();
            Base.TagStart = Reader.ReadInt32() + Header.IndexOffset;
            Base.PrimaryMagic = Reader.ReadInt32() - (Header.IndexOffset + 32);
            Reader.BaseStream.Position = Base.TagStart + 8;
            Base.SecondaryMagic = Reader.ReadInt32() - (Header.IndexOffset + Header.IndexSize);
            return Base;
        }
    }
}
