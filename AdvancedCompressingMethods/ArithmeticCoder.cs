using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdvancedCompressingMethods.Constants;


namespace AdvancedCompressingMethods
{
    class ArithmeticCoder
    {
        public FileController fileController = new FileController("", "");

        public int[] char_to_index = new int[NO_OF_CHARS];     /* To index from character          */
        public byte[] index_to_char = new byte[NO_OF_SYMBOLS + 1];     /* To character from index    */
        public int[] cumulative_freq = new int[NO_OF_SYMBOLS + 1];		/* Cumulative symbol frequencies    */
        public int[] freq = new int[NO_OF_SYMBOLS + 1];	/* Symbol frequencies                       */

        public ulong range = 0;
        public ulong High;
        public ulong Low;
        public ulong bitsToFollow = 0;

        public bool fileLoaded = false;
        public ulong value;

        public ArithmeticCoder() { }

        public void StartModel()
        {
            for (int i = 0; i < NO_OF_CHARS; i++)
            {
                char_to_index[i] = i + 1;
                index_to_char[i + 1] = (byte)i;
            }

            for (int i = 0; i < NO_OF_SYMBOLS; i++)
            {
                freq[i] = 1;
                cumulative_freq[i] = NO_OF_SYMBOLS - i;
            }
            freq[0] = 0;
        }

        public void Encode()
        {
            if (!fileLoaded)
            {
                MessageBox.Show("Please load a file to encode!");
                return;
            }

            StartModel();
            StartEncoding();

            while (true)
            {
                int character = fileController.ReadByte();

                if (character == END_OF_FILE)
                {
                    break;
                }

                int symbol = char_to_index[character];

                EncodeSymbol(symbol, cumulative_freq);
                UpdateModel(symbol);
            }
            EncodeSymbol(EOF_SYMBOL, cumulative_freq);
            DoneEncoding();
            DoneOutputingBits();

            fileController.closeWriter();
            fileController.closeReader();
            fileLoaded = false;
        }

        public void EncodeSymbol(int symbol, int[] cumulative_freq)
        {
            ulong range;
            range = (High - Low) + 1;

            High = Low + (range * (ulong)cumulative_freq[symbol - 1]) / (ulong)cumulative_freq[0] - 1;
            Low = Low + (range * (ulong)cumulative_freq[symbol]) / (ulong)cumulative_freq[0];

            while (true)
            {
                if (High < HALF)
                {
                    WriteBitPlusFollow(0);
                }
                else if (Low >= HALF)
                {
                    WriteBitPlusFollow(1);
                    Low -= HALF;
                    High -= HALF;
                }
                else if (Low >= FIRST_QUARTER && High < THIRD_QUARTER)
                {
                    bitsToFollow += 1;
                    Low -= FIRST_QUARTER;
                    High -= FIRST_QUARTER;
                }
                else {
                    break;
                }
                Low <<= 1;
                High = (High << 1) | 1;
            }
        }

        public void Decode()
        {
            if (!fileLoaded)
            {
                MessageBox.Show("Please load a file to decode!");
                return;
            }

            StartModel();
            StartDecoding();

            while (true)
            {
                int symbol = DecodeSymbol(cumulative_freq);
                if (symbol == EOF_SYMBOL)
                {
                    break;
                }
                int character = index_to_char[symbol];
                fileController.WriteByte((byte)character);
                UpdateModel(symbol);
            }

            fileController.closeWriter();
            fileController.closeReader();
            fileLoaded = false;
        }

        public int DecodeSymbol(int[] cumulative_freq)
        {
            ulong range;
            ulong cumulative;
            int symbol;

            range = (High - Low) + 1;

            cumulative = (((ulong)(value - Low) + 1) * (ulong)cumulative_freq[0] - 1) / range; //????????????????????????????????????

            for (symbol = 1; (ulong)cumulative_freq[symbol] > cumulative; symbol++);

            High = Low + (range * (ulong)cumulative_freq[symbol - 1]) / (ulong)cumulative_freq[0] - 1;
            Low = Low + (range * (ulong)cumulative_freq[symbol]) / (ulong)cumulative_freq[0];

            while (true)
            {
                if (High < HALF)
                {
                }
                else if (Low >= HALF)
                {
                    value -= HALF;
                    Low -= HALF;
                    High -= HALF;
                }
                else if (Low >= FIRST_QUARTER && High < THIRD_QUARTER)
                {
                    value -= FIRST_QUARTER;
                    Low -= FIRST_QUARTER;
                    High -= FIRST_QUARTER;
                }
                else
                { 
                    break;
                }
                Low <<= 1;
                High = (High << 1) | 1;
                value = (value << 1) | (ulong)fileController.ReadSingleBit();
            }

            return symbol;
        }

        public void UpdateModel(int symbol)
        {
            if (cumulative_freq[0] == MAX_FREQUENCY)
            {
                int cumulative = 0;
                for (int i = NO_OF_SYMBOLS; i >= 0; i--)
                {
                    freq[i] = (freq[i] + 1) / 2;
                    if (freq[i] == 0) freq[i] = 1; // prevent zero frequency
                }

                // Recalculate cumulative_freq
                for (int i = NO_OF_SYMBOLS; i >= 0; i--)
                {
                    cumulative_freq[i] = cumulative;
                    cumulative += freq[i];
                }
            }

            freq[symbol]++;

            // Update cumulative_freq[0..symbol - 1] only
            for (int i = symbol - 1; i >= 0; i--)
            {
                cumulative_freq[i]++;
            }
        }

        public void UpdateModel_UNOPTIMIZED(int symbol)
        {
            int i;
            if (cumulative_freq[0] == MAX_FREQUENCY)
            {
                int cumulative;
                cumulative = 0;

                for (i = NO_OF_SYMBOLS; i >= 0; i--)
                {
                    freq[i] = (freq[i] + 1) / 2;
                    cumulative_freq[i] = cumulative;
                    cumulative += freq[i];
                }
            }

            for (i = symbol; freq[i] == freq[i - 1]; i--) ;

            if (i < symbol)
            {
                int ch_i, ch_symbol;
                ch_i = index_to_char[i];
                ch_symbol = index_to_char[symbol];
                index_to_char[i] = (byte)ch_symbol;
                index_to_char[symbol] = (byte)ch_i;
                char_to_index[ch_i] = symbol;
                char_to_index[ch_symbol] = i;
            }

            freq[i] += 1;

            while (i > 0)
            {
                i -= 1;
                cumulative_freq[i] += 1;
            }
        }

        public void StartEncoding()
        {
            Low = 0;
            High = HIGH_VALUE;
            bitsToFollow = 0;
        }

        public void StartDecoding()
        {
            value = 0;
            for (int i = 1; i <= CODE_VALUE_BITS; i++)
            {
                int bit = fileController.ReadSingleBit();

                value = (value << 1) | (ulong)bit;
            }
            Low = 0;
            High = HIGH_VALUE;
        }

        public void DoneEncoding()
        {
            bitsToFollow += 1;
            if (Low < FIRST_QUARTER)
            {
                WriteBitPlusFollow(0);
            }
            else
            {
                WriteBitPlusFollow(1);
            }
        }

        public void DoneOutputingBits()
        {
            fileController.FlushWriteBuffer();
        }

        public void WriteBitPlusFollow(int bit)
        {
            fileController.WriteSingleBit(bit);

            while (bitsToFollow > 0)
            {
                fileController.WriteSingleBit(1 - bit);
                bitsToFollow -= 1;
            }
        }

        public void OutputBitPlusFollow(int bit)
        {
            fileController.WriteSingleBit(bit);
        }

        public void OpenInputFileStream(string inputFile)
        {
            fileLoaded = true;
            fileController.OpenInputFileStream(inputFile);
        }

        public void OpenOutputFileStream(string outputFile)
        {
            fileController.OpenOutputFileStream(outputFile);
        }
    }
}
