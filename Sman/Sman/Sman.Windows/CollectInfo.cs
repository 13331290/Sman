using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Sman
{
    class CollectInfo
    {
        string type;      // 有music、video、picture、book四种类型
        StorageFile file;

        public CollectInfo(string type, StorageFile file) 
        {
            this.type = type;
            this.file = file;
        }


        public string getType()
        {
            return type;
        }

        public StorageFile getFile()
        {
            return file;
        }
    }
}
