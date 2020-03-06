using System;
using System.Collections.Generic;
using System.Text;

namespace PacketMaker
{
    class PacketScript : ScriptMaker
    {

        public PacketScript()
        {
            FileName = "Packet.cs";
        }
        protected override string Make_Code(Dictionary<string, string> keyData, PacketStruct[] packetList)
        {

            string header = keyData["Header"];
            string nameSpace = keyData["namespace"];

            Dictionary<string, string> protocolDefinitions = new Dictionary<string, string>(); // protocol , packet

            //start namespace block
            string pullText = $"namespace {nameSpace}\n" + "{\n";


            foreach (var packet in packetList)
            {
                if (packet == null) continue;


                //start func and block
                string packetText = "\tpublic class PK_" + packet.ProtocolName + " : Packet\n\t{";
                

                //field
                foreach(var data in packet.DataList)
                {
                    packetText += "\n\t\t" + $"public {data.Data_Type} {data.Data_Name};" + "\n\n";
                }


                //constructor
                packetText += "\t\t" + $"public PK_{packet.ProtocolName}()\n";

                //constructor body
                packetText += "\t\t{\n\t\t\t" + $"this.packetType = (ushort){packet.Classification}.{packet.ProtocolName};" +"\n\t\t}\n\n";


                //Serialize
                packetText += "\t\t" + "protected override void Serialize()\n";

                //Serialize body
                packetText += "\t\t{";
                foreach(var data in packet.DataList)
                {
                    packetText += "\n\t\t\t" + $"Stream.write(buffer,{data.Data_Name}, ref this.offset);\n";
                }
                packetText += "\t\t}\n\n";



                //Deserialize
                packetText += "\t\t" + "protected override void Deserialize(byte[] recvBytes)\n";

                //Deserialize body
                packetText += "\t\t{";
                foreach (var data in packet.DataList)
                {
                    packetText += "\n\t\t\t" + $"Stream.read(recvBytes, ref {data.Data_Name}, ref this.offset);\n";
                }
                packetText += "\t\t}\n\n";


                //end block
                packetText += "\t}\n\n";


                pullText += packetText;
            }



            //end namespace block
            pullText += "}";

            return pullText;

        }



}
}
