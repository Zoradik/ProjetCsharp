﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class Log
    {
        public static string filePath = @"..\..\..\Files\log.json";

        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public string FileSize { get; set; }
        public string FileTransferTime { get; set; }
        public string time { get; set; }
        public string TimeToCrypt { get; set; }
        

    }
}