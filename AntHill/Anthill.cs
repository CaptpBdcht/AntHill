using System;
using System.Collections.Generic;
using Engine;
using Engine.Entity;
using Engine.Map;
using Engine.Serializer;
using System.IO;

namespace Anthill
{
    [Serializable]
    public class Anthill : Simulator
    {
        public Anthill(WorldFactory worldFactory) : base(worldFactory) {}

        public override void Save(string path)
        {
            var data = new BinarySerializer().Serialize(this);
            string time = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            using (StreamWriter outputFile = new StreamWriter(path + "\\anthill_" + time + ".dat"))
            {
                outputFile.WriteLine(data);
            }
        }

        public override Simulator Load(string path)
        {
            using (StreamReader inputFile = new StreamReader(path))
            {
                string data = inputFile.ReadLine();
                return new BinarySerializer().Deserialize(data);
            }
        }
    }
}