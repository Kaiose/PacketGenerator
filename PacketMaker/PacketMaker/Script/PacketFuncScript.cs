using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PacketMaker
{
    class PacketFuncScript : ScriptMaker
    {


        public PacketFuncScript()
        {
            FileName = "PacketFunc.cs";
        }

        protected override string Make_Code(Dictionary<string, string> keyData, PacketStruct[] packetList)
        {
            string pullText = "";

            string header = keyData["Header"];
            string nameSpace = keyData["namespace"];


            Dictionary<string, string> protocolHandlerDic = new Dictionary<string, string>();

            foreach (var packet in packetList)
            {
                if (packet == null) continue;

                string handlerScript = "";
                if (protocolHandlerDic.ContainsKey(packet.Classification) == false)
                {
                    handlerScript = $"\tpartial class {packet.Classification}Handler " + "{\n";

                    protocolHandlerDic.Add(packet.Classification, handlerScript);
                }

                // func
                handlerScript = protocolHandlerDic[packet.Classification];
                handlerScript += "\n\t\t" + $"public void Recv_{packet.ProtocolName}(Session session, Packet rowPacket)";
                //func block
                handlerScript += "\n\t\t{" + "\n\n\t\t}\n";


                protocolHandlerDic[packet.Classification] = handlerScript;

            }


            foreach (var key in protocolHandlerDic.Keys.ToList())
            {
                protocolHandlerDic[key] += "\n\t}\n\n";
            }

            //header
            pullText += $"{header}\n\n";

            //namespace
            pullText += $"namespace {nameSpace}\n";

            pullText += "{\n";

            foreach(var protocolHandler in protocolHandlerDic.Values)
            {
                pullText += protocolHandler;
            }


            pullText += "\n\n}";

            return pullText;
        }
    }

}
