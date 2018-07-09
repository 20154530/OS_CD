using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using OS_CD;
namespace OS_CD.Tools
{
    class SerializeFileSystem
    {
        //序列化
        public static void SerializeObject(string filePath,FileSystem fileSystem)
        {
            Stream fStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fStream, fileSystem);
        }
        //反序列化
        public static bool DeserializeObject(string filePath,out FileSystem fileSystem)
        {
            Stream fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fStream.Length == 0)
            {
                fileSystem = null;
                return false;
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            fileSystem =(FileSystem)binaryFormatter.Deserialize(fStream);
            return true;
        }
    }
}
