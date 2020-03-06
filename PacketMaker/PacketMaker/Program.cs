using System;

namespace PacketMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptMaker[] scriptMaker = { new ProtocolScript(), new PacketScript(), new PacketFuncScript()};

            foreach (var script in scriptMaker)
            {
                script.Make_File();
            }

        }
    }
}
