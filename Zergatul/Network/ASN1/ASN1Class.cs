﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zergatul.Network.ASN1
{
    public enum ASN1TagClass : byte
    {
        Universal = 0,
        Application = 1,
        ContextSpecific = 2,
        Private = 3
    }
}