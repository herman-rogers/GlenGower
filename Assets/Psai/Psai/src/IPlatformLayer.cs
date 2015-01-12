//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace psai.net
{
    interface IPlatformLayer
    {
        Stream GetStreamOnPsaiCoreBinary(string filename);
        string ConvertFilePathForPlatform(string filepath);
    }
}



