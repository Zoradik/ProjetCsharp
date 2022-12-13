using Castle.Core.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace test2
{
    public static class Locker
    {
        public static readonly object ReadData = new object();
        public static readonly object WriteData = new object();
        public static readonly object Write_State_LK = new object();
        public static readonly object Locker_State_Display = new object();
        public static readonly object Read_State = new object();
    }
}
