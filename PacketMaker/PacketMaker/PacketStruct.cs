using System;
using System.Collections.Generic;
using System.Text;

namespace PacketMaker
{
    struct DataStruct
    {
        public string Data_Name;
        public string Data_Type;
    }
    class PacketStruct
    {
        public string Classification;
        public string ProtocolName;
        public string ProtocolNumber;
        public List<DataStruct> DataList = new List<DataStruct>();

        public PacketStruct()
        {
        }
    }
}
