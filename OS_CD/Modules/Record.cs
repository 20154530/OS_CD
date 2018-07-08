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
    public class OpenFileRecord
    {
        private FileNodeId fileNodeId;
        public FileNodeId FileNodeId {
            get => fileNodeId;
            set {
                fileNodeId = value;
            }
        }

        private DateTime createTime;
        public DateTime CreateTime {
            get => createTime;
            set { createTime = value; }
        }

        public OpenFileRecord(FileNodeId fileNodeId)
        {
            FileNodeId = fileNodeId;
            createTime = DateTime.Now;
        }
    }

    public class SystemOpenFileRecord : OpenFileRecord
    {
        //共享计数
        public int counter { get; private set; }

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

    public class UserOpenFileRecord : OpenFileRecord
    {
        //打开文件的缓存
        public FileBody buff = new FileBody();
        public FileEvent fileEvent = FileEvent.Empty;
        public UserOpenFileRecord(int fileNodeId, FileBody fileBody) : base(fileNodeId)
        {
            this.buff.Copy(fileBody);
            this.buff.fileBodyChangeEvent += this.FileBodyChangeEvent;
        }
        public void FileBodyChangeEvent()
        {
            fileEvent = FileEvent.Write;
        }
    }
}