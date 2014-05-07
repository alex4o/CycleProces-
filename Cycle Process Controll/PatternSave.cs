using CycleProcessControl.Pattern.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CycleProcessControl
{
    class PatternSave
    {
        public static void Save(String Path,StaticPatternModel Model) {
            IFormatter formatter = new BinaryFormatter();

            FileStream Stream = new FileStream(Path, FileMode.Create);

            formatter.Serialize(Stream, Model);
            Stream.Flush();
            Stream.Dispose();
        }

        public static StaticPatternModel Load(String Path)
        {
            IFormatter formatter = new BinaryFormatter();

            FileStream Stream = new FileStream(Path, FileMode.OpenOrCreate);
            if (Stream.Length == 0) {
                Stream.Flush();
                Stream.Dispose(); 
                return null;

            }
            StaticPatternModel Model = formatter.Deserialize(Stream) as StaticPatternModel;
            Stream.Flush();
            Stream.Dispose();
            return Model;
        }

    }
}
