﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zergatul.Network.Tls
{
    class GenericAEADCiphertext : GenericCiphertext
    {
        public byte[] NonceExplicit;
    }
}
