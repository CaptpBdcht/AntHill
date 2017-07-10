using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Serializer
{
    public interface ISerializer
    {
        string Serialize(Simulator simulator);

        Simulator Deserialize(string data);
    }
}
