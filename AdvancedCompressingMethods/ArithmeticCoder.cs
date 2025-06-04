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
        public int[] char_to_index = new int[NO_OF_CHARS];     /* To index from character          */
        public byte[] index_to_char = new byte[NO_OF_SYMBOLS + 1];     /* To character from index    */
        public int[] cum_freq = new int[NO_OF_SYMBOLS + 1];		/* Cumulative symbol frequencies    */
        public int[] freq = new int[NO_OF_SYMBOLS + 1];	/* Symbol frequencies                       */

        public ulong range = 0;
        public uint High = 0xFFFFFFFF;
        public uint Low = 0x00000000;

        public ArithmeticCoder()
        {

        }

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
                cum_freq[i] = NO_OF_SYMBOLS - i;
            }
            freq[0] = 0;
        }

        public void UpdateModel()
        {
            MessageBox.Show("update");
        }
    }
}
