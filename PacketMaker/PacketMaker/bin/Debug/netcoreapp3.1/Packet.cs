namespace CSharp_Server
{
	public class PK_CS_MESSAGE_REQ : Packet
	{
		public string message;

		public PK_CS_MESSAGE_REQ()
		{
			this.packetType = (ushort)ClientProtocol.CS_MESSAGE_REQ;
		}

		protected override void Serialize()
		{
			Stream.write(buffer,message, ref this.offset);
		}

		protected override void Deserialize(byte[] recvBytes)
		{
			Stream.read(recvBytes, ref message, ref this.offset);
		}

	}

	public class PK_CS_LOGIN_REQ : Packet
	{
		public string message;

		public PK_CS_LOGIN_REQ()
		{
			this.packetType = (ushort)ClientProtocol.CS_LOGIN_REQ;
		}

		protected override void Serialize()
		{
			Stream.write(buffer,message, ref this.offset);
		}

		protected override void Deserialize(byte[] recvBytes)
		{
			Stream.read(recvBytes, ref message, ref this.offset);
		}

	}

	public class PK_SD_CHECK_LOGIN : Packet
	{
		public string message;

		public PK_SD_CHECK_LOGIN()
		{
			this.packetType = (ushort)ServerProtocol.SD_CHECK_LOGIN;
		}

		protected override void Serialize()
		{
			Stream.write(buffer,message, ref this.offset);
		}

		protected override void Deserialize(byte[] recvBytes)
		{
			Stream.read(recvBytes, ref message, ref this.offset);
		}

	}

}