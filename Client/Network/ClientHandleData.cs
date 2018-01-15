using System.Collections.Generic;
using System.Diagnostics;
using Bindings;
using GeonBit.UI.Utils;

namespace Client
{
    class ClientHandleData
    {
        public static ClientHandleData instance = new ClientHandleData();

        private delegate void Packets(byte[] data);
        private static Dictionary<int, Packets> _packets;

        public void Start()
        {
            _packets = new Dictionary<int, Packets>
            {
                //Add your packets in here, so the client knows which methode to execute.
                { (int)ServerPackets.SAlertMsg, HandleAlertMsg },
                //{ (int)ServerPackets.SLoginOk, HandleLoginOk },
                //{(int)ServerPackets.SPlayerData, HandlePlayerData },

            };
        }

        public void HandleData(byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteBytes(data);
            int packet = buffer.ReadInteger();
            buffer.Dispose();
            if (_packets.TryGetValue(packet, out Packets _packet))
            {
                _packet.Invoke(data);
            }
        }

        public void HandleAlertMsg(byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInteger();
            string msg = buffer.ReadString();

            MessageBox.ShowMsgBox("Info!", msg, new MessageBox.MsgBoxOption[]
                    {
                        new MessageBox.MsgBoxOption("Okay!" ,() => {return true; })
                    });

            Debug.WriteLine(msg);
            buffer.Dispose();
        }
    }
}
