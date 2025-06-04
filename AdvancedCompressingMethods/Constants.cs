using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCompressingMethods
{
    internal class Constants
    {
        /* THE SET OF SYMBOLS THAT MAY BE ENCODED. */
        public const int NO_OF_CHARS = 256; /* Number of character symbols      */
        public const int EOF_SYMBOL = (NO_OF_CHARS + 1);	/* Index of EOF symbol              */
        public const int NO_OF_SYMBOLS = (NO_OF_CHARS + 1);	/* Total number of symbols          */

        /* CUMULATIVE FREQUENCY TABLE. */
        public const int Max_frequency = 16383;		/* Maximum allowed frequency count  */
        public const int SYMBOL_COUNT = 256;
        //public const int EOL = 256;
    }
}
