using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;

namespace OS_CD {
    #region 别名
    
    using FileNodeId = Int32;
    using UserId = Int32;

    #endregion
    public class User
    {
        public int ID;
        public string name;
        //用户打开文件表
        public Dictionary<FileNodeId, UserOpenFileRecord> openFileRecordList = new Dictionary<int, UserOpenFileRecord>();

        public User(int id, string name)
        {
            this.ID = id;
            this.name = name;
        }


    }
}