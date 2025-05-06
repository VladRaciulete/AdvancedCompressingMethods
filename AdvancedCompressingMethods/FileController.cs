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
        }

        public void close()
        {
            reader.Close();
            writer.Close();
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
                // read a whole byte and put it into the readBuffer
                readBuffer = reader.ReadByte();
                readCounter = 8;
                Console.WriteLine(Convert.ToString(readBuffer, 2).PadLeft(8, '0'));
            }

            if (readCounter > 0)
            {
                // read one bit from the readBuffer
                //readBuffer

                //int firstBit = readBuffer.Value

                int firstBit = (readBuffer >> (7 - (8 - readCounter))) & 1;
                Console.WriteLine("read bit number " + (8 - readCounter) + " from the buffer => " + firstBit);

                readCounter -= 1;
                return firstBit;
            }

            return 1;
        }

        public void WriteSingleBit(int bit)
        {
            Console.WriteLine("WriteSingleBit");
            Console.WriteLine(Convert.ToString(writeBuffer, 2).PadLeft(8, '0'));
            Console.Write("Bit ");
            Console.WriteLine(Convert.ToString(bit, 2).PadLeft(8, '0'));
            if (writeCounter < 8)
            {
                //put the bit in the writeBuffer

                writeBuffer = (byte)(writeBuffer << 1 | bit);
                Console.WriteLine(Convert.ToString(writeBuffer, 2).PadLeft(8, '0'));

                writeCounter += 1;

                if (writeCounter == 8)
                {
                    Console.Write("counter = 8 ");
                    //the byte is full write it inside the file
                    Console.WriteLine(writeBuffer);
                    writer.Write(writeBuffer);
                    writeCounter = 0;
                }
            }
            else
            {

            }
        }

        public void ReadNBits(int n)
        {
        }

        public void WriteNBits(int n)
        {
        }

    }
}
