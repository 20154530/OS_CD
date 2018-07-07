using System;
using System.Collections.Generic;
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
    public enum FileState
    {
        WithoutChange,
        BeWrite,
        //BeRead,
        BeDestory,
    }
    
    public class OpenFileRecord
    {
        private FileNodeId fileNodeId;
        public DateTime createTime {get;}
        
        public OpenFileRecord(FileNodeId fileNodeId)
        {
            this.fileNodeId = fileNodeId;
            this.createTime = DateTime.Now;
        }
    }

    public class SystemOpenFileRecord:OpenFileRecord
    {
        //共享计数
        private int counter;
        
        public SystemOpenFileRecord(int fileNodeId) : base(fileNodeId)
        {
            counter = 0;
        }

        public void CounterIncrease()
        {
            counter++;
        }

        public void CounterDecrease()
        {
            counter--;
        }
        
        
    }
    
}