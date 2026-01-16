using System;
using System.Collections.Generic;

namespace SdrClientApp.Messages
{
    public static class MessageHelper
    {
        
        public const ushort SET_CONTROL_ITEM = 0x0000;
        public const ushort ACK = 0x0003;

        
        public const ushort CID_RECEIVER_STATE = 0x0018;
        public const ushort CID_RECEIVER_FREQUENCY = 0x0020;
        public const ushort CID_RECEIVER_SAMPLE_RATE = 0x00B0;

       
        public static byte[] CreateFrequencyPayload(uint frequencyHz)
        {
           
            byte[] fullBytes = BitConverter.GetBytes((long)frequencyHz);
            byte[] payload = new byte[5];
            Array.Copy(fullBytes, 0, payload, 0, 5);
            return payload;
        }
 public static byte[] CreateFrequencyPayloadBackup(uint frequencyHz)
        {
           
            byte[] fullBytes = BitConverter.GetBytes((long)frequencyHz);
            byte[] payload = new byte[5];
            Array.Copy(fullBytes, 0, payload, 0, 5);
            return payload;
        }
        
        public static byte[] GetControlItemMessage(ushort controlCode, byte itemCode, byte[] parameters)
        {
            var message = new List<byte>();
            
            ushort length = (ushort)(parameters.Length + 5);
            message.AddRange(BitConverter.GetBytes(length));
            message.AddRange(BitConverter.GetBytes(SET_CONTROL_ITEM));
            message.Add(itemCode);
            message.AddRange(parameters);

            return message.ToArray();
        }

        
        public static byte[] CreateCaptureCommand(ushort code, byte[] parameters)
        {
            var message = new List<byte>();

            // Çàãîëîâîê: Äîâæèíà ïîâ³äîìëåííÿ (2 áàéòè)
            ushort length = (ushort)(parameters.Length + 4);
            message.AddRange(BitConverter.GetBytes(length));

            // Êîä êîìàíäè (2 áàéòè)
            message.AddRange(BitConverter.GetBytes(code));

            // Äàí³ êîìàíäè
            message.AddRange(parameters);

            return message.ToArray();
        }

        // Ìåòîä äëÿ ôîðìàòóâàííÿ ÷àñòîòè (5-áàéòíå ïðåäñòàâëåííÿ)
        public static byte[] FormatFrequency(long hz)
        {
            byte[] freqBytes = BitConverter.GetBytes(hz);
            byte[] result = new byte[5];
            Array.Copy(freqBytes, 0, result, 0, 5);
            return result;
        }
    }
}
