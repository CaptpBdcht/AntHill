using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class BoardMetadata
    {
        public static int BoardSize { get; set; }

        public static Random Random = new Random(DateTime.Now.Second);
    }
}
