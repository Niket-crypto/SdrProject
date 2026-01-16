using System;
using System.Collections.Generic;

namespace SdrClientApp.Messages
{
    public static class MessageHelper
    {
        // Константи протоколу NetSDR (як у друга)
        public const ushort SET_CONTROL_ITEM = 0x0000;
        public const ushort ACK = 0x0003;

        // Коди елементів керування
        public const ushort CID_RECEIVER_STATE = 0x0018;
        public const ushort CID_RECEIVER_FREQUENCY = 0x0020;
        public const ushort CID_RECEIVER_SAMPLE_RATE = 0x00B0;

        // --- МЕТОДИ ДЛЯ ТЕСТІВ (Лаба №3) ---

        // Цей метод вимагав твій тест у SdrClientTests
        public static byte[] CreateFrequencyPayload(uint frequencyHz)
        {
            // У NetSDR частота передається як 5-байтне ціле (Little Endian)
            byte[] fullBytes = BitConverter.GetBytes((long)frequencyHz);
            byte[] payload = new byte[5];
            Array.Copy(fullBytes, 0, payload, 0, 5);
            return payload;
        }

        // Цей метод потрібен для тесту довжини заголовка
        public static byte[] GetControlItemMessage(ushort controlCode, byte itemCode, byte[] parameters)
        {
            var message = new List<byte>();
            // Заголовок (Довжина = параметри + тип + код + айтем)
            ushort length = (ushort)(parameters.Length + 5);
            message.AddRange(BitConverter.GetBytes(length));
            message.AddRange(BitConverter.GetBytes(SET_CONTROL_ITEM));
            message.Add(itemCode);
            message.AddRange(parameters);

            return message.ToArray();
        }

        // --- ОСНОВНІ МЕТОДИ КЛІЄНТА ---

        public static byte[] CreateCaptureCommand(ushort code, byte[] parameters)
        {
            var message = new List<byte>();

            // Заголовок: Довжина повідомлення (2 байти)
            ushort length = (ushort)(parameters.Length + 4);
            message.AddRange(BitConverter.GetBytes(length));

            // Код команди (2 байти)
            message.AddRange(BitConverter.GetBytes(code));

            // Дані команди
            message.AddRange(parameters);

            return message.ToArray();
        }

        // Метод для форматування частоти (5-байтне представлення)
        public static byte[] FormatFrequency(long hz)
        {
            byte[] freqBytes = BitConverter.GetBytes(hz);
            byte[] result = new byte[5];
            Array.Copy(freqBytes, 0, result, 0, 5);
            return result;
        }
    }
}