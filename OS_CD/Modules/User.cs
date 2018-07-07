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
    public class User {
        public int ID;

        public string name;
        public string Name {
            get => name;
            set { name = value; }
        }
        private string password;
        public string Password {
            get => password;
            set {
                password = value;
            }
        }
        //用户打开文件表
        public Dictionary<FileNodeId,OpenFileRecord> openFileRecordList = new Dictionary<int, OpenFileRecord>(); 
        
        public User(int id ,string name )
        {
            this.ID = id;
            this.name = name;
        }


        public void UpdateRecord(FileNodeId fileNodeId)
        {
           
        }

        public void DestoryRecord(FileNodeId fileNodeId)
        {
            openFileRecordList.Remove(fileNodeId);
        }
    }
}