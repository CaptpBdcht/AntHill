using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Serializer
{
    public class BinarySerializer : ISerializer
    {
        public string Serialize(Simulator simulator)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, simulator);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public Simulator Deserialize(string data)
        {
            byte[] bytes = Convert.FromBase64String(data);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return (Simulator)(new BinaryFormatter().Deserialize(ms));
            }
        }
    }
}
