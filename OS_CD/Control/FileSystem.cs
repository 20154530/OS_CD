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

    public enum FileEvent {
        Write,
        Open,
        Create,
    }

    public class FileSystem {
        //单例
        private static FileSystem _Instance;
        public static FileSystem Instance {
            set { }
            get {
                if (_Instance is null)
                    _Instance = new FileSystem();
                return _Instance;
            }
        }

        private FileSystem() {
            Init();
        }

        //FCB文件控制块 List
        private Dictionary<FileNodeId, FileNode> FCBList = new Dictionary<int, FileNode>();

        //根目录
        private FileNodeId rootFolderId = -1;

        //当前目录
        private FileNodeId currentFolderId = -1;

        //UCb用户控制块 List
        private Dictionary<UserId, User> UCBList = new Dictionary<UserId, User>();

        //超级用户
        private UserId rootUserId = -1;

        //当前用户
        private UserId currentUserId = -1;

        //系统打开文件表
        private Dictionary<FileNodeId, SystemOpenFileRecord> systemOpenFileRecordList = new Dictionary<int, SystemOpenFileRecord>();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init() {
            var rootUser = new User(GetNextUsefulID(), "rootUser");
            UCBList.Add(rootUser.ID, rootUser);
            this.rootUserId = rootUser.ID;

            var rootFolder = new Folder(GetNextUsefulID(), "rootFolder");
            FCBList.Add(rootFolder.ID, rootFolder);
            this.rootFolderId = rootFolder.ID;

        }



        //作为分配的ID
        private static int IDTimeLine = 0;

        private FileNodeId GetNextUsefulID() {
            return IDTimeLine++;
        }

        public FileNodeId GetErrorID() {
            return -1;
        }

        //----------------文件操作

        public FileNodeId MakeFile(string name, int blockAmount, FileNodeId ownerFileNodeId) {

            var ownerFileNode = FCBList[ownerFileNodeId];
            if (ownerFileNode.GetType() != typeof(Folder)) return GetErrorID();

            File newFile = new File(GetNextUsefulID(), name);
            if (Disc.Instance.IsFreeBlockEnough(blockAmount))
                //分配内存块
                for (int i = blockAmount; i > 0; i--)
                {
                    newFile.blockIdList.Add(Disc.Instance.GetBlockFromGroup());
                }
            else return GetErrorID();

            RegisterFileNode(newFile);
            ((Folder)ownerFileNode).subFileNodeIdList.Add(newFile.ID);
            Debug.Print("make a file ,id is :" + newFile.ID + "\n");
            return newFile.ID;
        }

        public FileNodeId MakeFolder(string name, FileNodeId ownerFileNodeId) {
            var ownerFileNode = FCBList[ownerFileNodeId];
            if (ownerFileNode.GetType() != typeof(Folder)) return GetErrorID();

            Folder newFile = new Folder(GetNextUsefulID(), name);
            RegisterFileNode(newFile);
            ((Folder)ownerFileNode).subFileNodeIdList.Add(newFile.ID);
            Debug.Print("make a folder ,id is:" + newFile.ID + "\n");
            return newFile.ID;
        }

        public void RegisterFileNode(FileNode fileNode) {
            FCBList.Add(fileNode.ID, fileNode);
        }

        public void LogOffFileNode(FileNodeId fileNodeId) {
            FCBList.Remove(fileNodeId);
        }

        public bool MoveFileNode(int fileNodeId, Folder from, Folder to) {
            if (!from.IsExist(fileNodeId))
            {
                return false;
            }
            else
            {
                from.subFileNodeIdList.Remove(fileNodeId);
                to.subFileNodeIdList.Add(fileNodeId);
                return true;
            }
        }

        public bool DestoryFileNode(FileNodeId fileNodeId) {
            var fileNode = GetFileNodeById(fileNodeId);
            if (fileNode.GetType() == typeof(Folder))
            {
                //销毁其下的子文件
                foreach (var subFileNodeId in ((Folder)fileNode).subFileNodeIdList)
                {
                    //FileNode subFileNode = GetFileNodeById(subFileNodeId);

                    DestoryFileNode(subFileNodeId);
                }

                //销毁文件夹
                LogOffFileNode(fileNodeId);
            }
            else
            {
                //内存回收文件
                foreach (var blockId in ((File)fileNode).blockIdList)
                {
                    Disc.Instance.AddBlockToGroup(blockId);
                }

                Debug.Print("remove a folder ,id is :" + fileNodeId + "\n");
                LogOffFileNode(fileNodeId);
            }

            return true;
        }

        public void SwitchCurrentFolder(FileNodeId fileNodeId) {
            currentFolderId = fileNodeId;
        }

        private FileNode GetFileNodeById(FileNodeId fileNodeId) {
            return FCBList[fileNodeId];
        }
        //-----------------用户操作

        public void SwitchCurrentUser(UserId userId) {
            currentUserId = userId;
        }

        public FileNodeId MakeUser(string userName) {
            User newUser = new User(GetNextUsefulID(), userName);
            RegisterUser(newUser);
            Debug.Print("make a user ,id is :" + newUser.ID + "\n");
            return newUser.ID;
        }

        public bool DestoryUser(UserId userId) {
            //销毁用户,并删除其所属文件
            LogOffUser(userId);
            Debug.Print("Remove a user ,id is:" + userId + "\n");
            return true;
        }

        public void RegisterUser(User user) {
            UCBList.Add(user.ID, user);
        }

        public void LogOffUser(UserId userId) {
            UCBList.Remove(userId);
        }

        public User GetUserById(UserId userId) {
            return UCBList[userId];
        }

        public User GetCurrentUser() {
            return UCBList[currentUserId];
        }

        //-----------------文件功能
        public bool OpenFile(FileNodeId fileNodeId) {
            //添加信息到系统打开文件表

            //添加信息到用户打开文件表

            //将文件内容放到用户打开文件表指向的缓冲区中



            return true;
        }

        public bool CloseFile(FileNodeId fileNodeId) {
            //检查文件状态（是否有更新）

            //将缓冲区内容填充到对应的区域

            //将更新文件状态（是否产生更新）

            //将记录从用户打开文件表中删除

            //将记录从系统打开文件表中删除

            return true;
        }

        //-----------------系统打开文件表
        private void CreateSystemOpenFileRecord(FileNodeId fileNodeId) {
            if (!systemOpenFileRecordList.ContainsKey(fileNodeId))
            {
                systemOpenFileRecordList.Add(fileNodeId, new SystemOpenFileRecord(fileNodeId));
            }
            else
            {
                systemOpenFileRecordList[fileNodeId].CounterIncrease();
            }
        }

        private SystemOpenFileRecord GetSystemOpenFileRecord(FileNodeId fileNodeId) {
            return systemOpenFileRecordList[fileNodeId];
        }

        private void DestorySystemOpenFileRecord() {

        }
    }
}