using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Linq;
namespace PacketMaker
{
    class ProtocolScript : ScriptMaker
    {
        /*
        using System;
        using System.Collections.Generic;
        using System.Text;

        namespace CSharp_Server
        {

            enum ClientProtocol : ushort
            {
                CS_MESSAGE_REQ = 10000,
                CS_LOGIN_REQ = 10001,
            }

            enum ServerProtocol : ushort
            {



            }

        }

        */

        public ProtocolScript()
        {
            FileName = "ProtocolDefinition.cs";

            
            
        }

        protected override string Make_Code(Dictionary<string, string> keyData, PacketStruct[] packetList)
        {
            

            string header = keyData["Header"];
            string nameSpace = keyData["namespace"];

            Dictionary<string, string> protocolDefinitions = new Dictionary<string, string>(); // <protocol , enumText>

            foreach(var packet in packetList)
            {
                if (packet == null) continue;
                string protocol = packet.Classification;

                if(protocolDefinitions.ContainsKey(protocol) == false)
                {
                    
                    string start = "\t" + $"enum {protocol} : ushort" + "\n\t{\n";

                    protocolDefinitions.Add(protocol, start);
                }



                protocolDefinitions[protocol] += "\t\t" + packet.ProtocolName + " = " + packet.ProtocolNumber + ",\n";
                
            }
            
            
            foreach (var key in protocolDefinitions.Keys.ToList())
            {
                protocolDefinitions[key] += "\n\t}";
            }


            string pullText = "";
            pullText += header + "\n\n";
            pullText += "namespace " + nameSpace + "\n{\n";

            foreach (var protocolText in protocolDefinitions)
            {
                pullText += protocolText.Value + "\n";
            }

            pullText += "\n}";

            System.Console.WriteLine(pullText);

            return pullText;
        }


    }
}
