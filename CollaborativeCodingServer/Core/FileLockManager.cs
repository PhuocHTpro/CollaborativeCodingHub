using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace CollaborativeCodingServer.Core
{
    public static class FileLockManager
    {
        public static ConcurrentDictionary<int, string> LockedFiles = new ConcurrentDictionary<int, string>();
    }
}
