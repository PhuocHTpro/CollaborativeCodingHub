using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingServer.Core
{
    public static class FileLockManager
    {
        public static Dictionary<int, string> LockedFiles = new();
    }
}
