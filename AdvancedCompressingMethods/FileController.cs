using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCompressingMethods
{
    class FileController
    {
        private FileStream inputFileStream;
        private FileStream outputFileStream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private byte readBuffer;
        private byte writeBuffer;
        private int readCounter;
        private int writeCounter;
        private long inputFileLength;

        public FileController(string inputFileName, string outputFileName)
        {
            inputFileStream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);
            reader = new BinaryReader(inputFileStream);

            outputFileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
            writer = new BinaryWriter(outputFileStream);

            readBuffer = 0;
            writeBuffer = 0;
            readCounter = 0;
            writeCounter = 0;

            inputFileLength = inputFileStream.Length;
        }

        public void close()
        {
            reader.Close();
            writer.Close();
        }

        public long getRemainingBitsLeft() {
            long currentPosition = inputFileStream.Position;
            long remainingBytes = inputFileLength - currentPosition;
            long remainingBits = remainingBytes * 8;
            return remainingBits;
        }

        public byte ReadByte()
        {
            return reader.ReadByte();
        }

        public void WriteByte(byte b)
        {
            writer.Write(b);
        }

        public int ReadSingleBit()
        {
            if (readCounter == 0)
            {
                readBuffer = reader.ReadByte();
                readCounter = 8;
            }

            if (readCounter > 0)
            {
                int firstBit = (readBuffer >> (7 - (8 - readCounter))) & 1;
                readCounter -= 1;
                return firstBit;
            }

            return 1;
        }

        public void WriteSingleBit(int bit)
        {
            if (writeCounter < 8)
            {
                writeBuffer = (byte)(writeBuffer << 1 | bit);
                writeCounter += 1;

                if (writeCounter == 8)
                {
                    writer.Write(writeBuffer);
                    writeCounter = 0;
                }
            }
        }

        public int[] ReadNBits(int n)
        {
            int[] vector = new int[n];

            for (int i = 0; i < n; i++) {
                vector[i] = ReadSingleBit();
            }

            return vector;
        }

        public void WriteNBits(int[] vector, int n)
        {
            for (int i = 0; i < n; i++)
            {
                WriteSingleBit(vector[i]);
            }
        }

    }
}
