using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace PacketMaker
{
    class ScriptMaker
    {

        private string DirPath = Directory.GetCurrentDirectory();
        protected string FileName = "ScriptMaker.cs";

        private Dictionary<string, string> keyData = PacketGenerator.instance.keyData;
        private PacketStruct[] packetList = PacketGenerator.instance.packetList;

        public void Make_File()
        {
            string pullText = Make_Code(keyData, packetList);

            string path = DirPath + "\\" + FileName;

            File.WriteAllText(path, pullText);

        }

        protected virtual string Make_Code(Dictionary<string, string> keyData, PacketStruct[] packetList)
        {
            return null;
        }

    }
}
