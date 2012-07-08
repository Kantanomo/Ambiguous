using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using H2Memory_class;
namespace Ambiguous
{
    public static class Poke
    {
        static H2Memory H2 = new H2Memory(H2Type.Halo2Vista, false);
        public static void PokeValue(Type type, object Value, int Offset, Map Map)
        {
            if (GoodToGo(Map))
            {
                if (type == typeof(float))
                    H2.H2Mem.WriteFloat(false, Offset - H2.SecondaryMagic(), float.Parse(Value.ToString()));
                if (type == typeof(byte))
                    H2.H2Mem.WriteByte(false, Offset - H2.SecondaryMagic(), byte.Parse(Value.ToString()));
                if (type == typeof(short))
                    H2.H2Mem.WriteShort(false, Offset - H2.SecondaryMagic(), short.Parse(Value.ToString()));
                if (type == typeof(ushort))
                    H2.H2Mem.WriteUShort(false, Offset - H2.SecondaryMagic(), ushort.Parse(Value.ToString()));
                if (type == typeof(int))
                    H2.H2Mem.WriteInt(false, Offset - H2.SecondaryMagic(), int.Parse(Value.ToString()));
                if (type == typeof(uint))
                    H2.H2Mem.WriteUInt(false, Offset - H2.SecondaryMagic(), uint.Parse(Value.ToString()));
                if (type == typeof(long))
                    H2.H2Mem.WriteLong(false, Offset - H2.SecondaryMagic(), long.Parse(Value.ToString()));
            }
        }
        public static void PokeIdent(string Class, uint Ident, int Offset, Map Map)
        {
            if (GoodToGo(Map))
            {
                int temp = H2.SecondaryMagic();
                H2.H2Mem.WriteStringAscii(false, Offset - temp, Globals.ReverseStr(Class));
                H2.H2Mem.WriteUInt(false, (Offset + 4) - H2.SecondaryMagic(), Ident);
            }
        }
        private static bool GoodToGo(Map Map)
        {
            try
            {
                string tmp = H2Memory_class.Map.CurrentMap(H2);
                if (Map.Header.MapName.Trim('\0') == tmp || Map.Header.MapName.Trim('\0') == "shared")
                    return true;
                else
                    return false;
            }
            catch (Exception) 
            {
                if (MessageBox.Show("Halo 2 has either restarted or exited, would you like to rescan for it?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    H2 = new H2Memory(H2Type.Halo2Vista, false);
                    if (H2.H2Found)
                    {
                        MessageBox.Show("Halo 2 was found :)");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Halo 2 was not found");
                        return false;
                    }
                }
                else
                    return false;
            }
        }
    }
}
