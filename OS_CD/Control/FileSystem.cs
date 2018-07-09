using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OS_CD {
    #region 别名

    using FileNodeId = Int32;
    using UserId = Int32;


    #endregion

    public enum FileEvent {
        Write,
        Open,
        Close,
        Create,
        MoveOut,
        MoveIn,
        Empty,

    }
    [Serializable]
    public class FileSystem {
        //单例
        public static FileSystem instance;

        public static FileSystem Instance {
            get {
                if (instance is null)
                {
                    instance = new FileSystem();
                }
                return instance;
            }
        }

        public FileSystem() {
            int GroupMaxAmount = 20;
            int DiscMaxAmount = 511;
            disc.Init(20, 512);
            Init();
        }
        //盘块
        Disc disc = new Disc();

        //FCB文件控制块 List
        private Dictionary<FileNodeId, FileNode> fCBList = new Dictionary<int, FileNode>();
        public Dictionary<FileNodeId, FileNode> FCBList {
            get => fCBList;
            set {
                fCBList = value;
            }
        }

        //根目录
        private FileNodeId rootFolderId = -1;

        //当前目录
        private FileNodeId currentFolderId = -1;


        //UCb用户控制块 List
        [field:NonSerialized]
        public event EventHandler UCBListChanged;
        private Dictionary<UserId, User> uCBList = new Dictionary<UserId, User>();
        public Dictionary<UserId, User> UCBList {
            get { return uCBList; }
            set { uCBList = value; }
        }

        //超级用户
        private UserId rootUserId = -1;

        //当前用户
        private UserId currentUserId = -1;

        //系统打开文件表
        private Dictionary<FileNodeId, SystemOpenFileRecord> systemOpenFileRecordList = new Dictionary<int, SystemOpenFileRecord>();

        public void Init() {
            //rootUserId 为0
            var rootUser = new User(GetNextUserfulUserID(), "Admin", "Admin");
            UCBList.Add(rootUser.ID, rootUser);
            this.rootUserId = rootUser.ID;
            //rootFolderId 为0
            var rootFolder = new Folder(GetNextUserfulFileNodeId(), "rootFolder");
            FCBList.Add(rootFolder.ID, rootFolder);
            this.rootFolderId = rootFolder.ID;

        }

        //作为分配的ID
        private  int UserIdTimeLine = 0;
        private UserId GetNextUserfulUserID() {
            return UserIdTimeLine++;
        }
        private  int FileNodeIdTimeLine = 0;
        private FileNodeId GetNextUserfulFileNodeId() {
            return FileNodeIdTimeLine++;
        }
        public FileNodeId GetErrorID() {
            return -1;
        }

        //----------------文件操作

        public FileNodeId CreateFile(string name, FileNodeId ownerFileNodeId, UserId ownerUserId) {
            //所属文件节点
            var ownerFileNode = FCBList[ownerFileNodeId];
            if (ownerFileNode.GetType() != typeof(Folder))
            {
                Debug.Print("fileNode " + ownerFileNode.name + " is not a folder,we can`t create a file in it\n");
                return GetErrorID();
            }
            //创建新文件
            File newFile = new File(GetNextUserfulFileNodeId(), name);
            var blockAmount = newFile.GetFileSize();
            //分配内存空间
            if (this.disc.IsFreeBlockEnough(blockAmount))
                for (int i = blockAmount; i > 0; i--)
                {
                    newFile.blockIdList.Add(this.disc.GetBlockFromFreeGroup(newFile.ID));
                }
            else return GetErrorID();

            //将文件的索引以及文件控制块保存到系统列表中
            //并构建目录
            RegisterFileNode(newFile);
            ((Folder)ownerFileNode).subFileNodeIdList.Add(newFile.ID);
            newFile.fatherFileNodeId = ownerFileNodeId;
            //更新文件的事件信息
            UpdateFileEvent(newFile.ID, ownerUserId, FileEvent.Create, DateTime.Now);

           // Debug.Print("make a file ,id is :" + newFile.ID + "\n");
            return newFile.ID;
        }

        public FileNodeId CreateFolder(string name, FileNodeId ownerFileNodeId, UserId ownerUserId) {
            //所属节点
            var ownerFileNode = FCBList[ownerFileNodeId];
            if (ownerFileNode.GetType() != typeof(Folder))
            {
                Debug.Print("fileNode " + ownerFileNode.name + " is not a folder,we can`t create a file in it\n");
                return GetErrorID();
            }
            //创建文件夹
            Folder newFile = new Folder(GetNextUserfulFileNodeId(), name);
            //将文件的索引以及文件控制块保存到系统列表中
            //并构建目录
            RegisterFileNode(newFile);
            ((Folder)ownerFileNode).subFileNodeIdList.Add(newFile.ID);
            newFile.fatherFileNodeId = ownerFileNodeId;
            //更新文件夹的事件信息
            UpdateFileEvent(newFile.ID, ownerUserId, FileEvent.Create, DateTime.Now);

            Debug.Print("make a folder ,id is:" + newFile.ID + "\n");
            return newFile.ID;
        }


        public void RegisterFileNode(FileNode fileNode) {
            //将文件节点出移入系统的记录
            FCBList.Add(fileNode.ID, fileNode);
        }

        public void LogOffFileNode(FileNodeId fileNodeId) {
            //将文件节点移出系统的记录
            FCBList.Remove(fileNodeId);
        }

        public bool MoveFileNode(FileNodeId fileNodeId, Folder to, UserId userId) {
            var fileNode = GetFileNodeById(fileNodeId);
            var from = (Folder)GetFileNodeById(fileNode.fatherFileNodeId);

            if (!from.IsExist(fileNodeId))
            {
                return false;
            }
            else
            {
                //移入移出，并添加文件的事件消息
                from.subFileNodeIdList.Remove(fileNodeId);
                fileNode.eventInfo.AddEventTime(FileEvent.MoveOut, userId, DateTime.Now);
                to.subFileNodeIdList.Add(fileNodeId);
                fileNode.eventInfo.AddEventTime(FileEvent.MoveIn, userId, DateTime.Now);
                return true;
            }
        }

        public bool DestoryFileNode(FileNodeId fileNodeId) {
            //文件节点
            var fileNode = GetFileNodeById(fileNodeId);
            var fatherFileNode = (Folder)GetFileNodeById(fileNode.fatherFileNodeId);

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
                    this.disc.AddBlockToFreeGroup(blockId);
                }
                //目录重构
                fatherFileNode.subFileNodeIdList.Remove(fileNodeId);
                //移除系统记录的索引
                LogOffFileNode(fileNodeId);
                Debug.Print("remove a folder ,id is :" + fileNodeId + "\n");
            }

            return true;
        }

        public void SwitchCurrentFolder(FileNodeId fileNodeId) {
            //切换当前的文件夹
            currentFolderId = fileNodeId;
        }

        public FileNode GetFileNodeById(FileNodeId fileNodeId) {
            //通过FileNodeId得到当前系统中存放的对应文件节点的对象
            return FCBList[fileNodeId];
        }
        //-----------------用户操作

        public void SwitchCurrentUser(UserId userId) {
            //切换当前用户
            currentUserId = userId;
        }

        public FileNodeId CreateUser(string userName, string password) {
            //创建用户，保存到系统记录中
            User newUser = new User(GetNextUserfulUserID(), userName, password);
            RegisterUser(newUser);
            //    Debug.Print("make a user ,id is :" + newUser.ID + "\n");
            UCBListChanged?.Invoke(this, EventArgs.Empty);
            return newUser.ID;
        }

        public bool DestoryUser(UserId userId) {
            //销毁用户,并删除其所属文件

            /*
             *  foreach (var fileNode in FCBList)
             {
                 //得到用户的文件，并删除
                 var ownerUserId = fileNode.Value.eventInfo.GetLastEventTime(FileEvent.Create).Key;
                 if (ownerUserId == userId)
                 {
                     DestoryFileNode(fileNode.Key);
                 }
             }
             */
            LogOffUser(userId);
            UCBListChanged?.Invoke(this, EventArgs.Empty);
            // Debug.Print("Remove a user ,id is:" + userId + "\n");
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

        //-----------------文件功能
        public bool OpenFileNode(FileNodeId fileNodeId, UserId userId) {
            //文件节点和用户
            var user = GetUserById(userId);
            var fileNode = GetFileNodeById(fileNodeId);

            //判断文件类型
            if (fileNode.GetType() == typeof(Folder))
            {

                Debug.Print("OpenFile funciton is user for open file ,not folder\n");
            }
            else if (fileNode.GetType() == typeof(File))
            {
                var file = (File)fileNode;
                //添加信息到系统打开文件表
                CreateSystemOpenFileRecord(fileNodeId);
                //添加信息到用户打开文件表
                //将文件内容放到用户打开文件表指向的缓冲区中
                user.openFileRecordList.Add(fileNodeId, new UserOpenFileRecord(fileNodeId, file.fileBody));

            }
            else
            {
                Debug.Print("Can`t open a unknown file type,please check out the typeof fileNode");
                return false;
            }
            //添加事件记录            
            fileNode.eventInfo.AddEventTime(FileEvent.Open, userId, DateTime.Now);
            return true;
        }

        public bool CloseFile(FileNodeId fileNodeId, UserId userId, UserOpenFileRecord userOpenFileRecord) {

            var fileNode = GetFileNodeById(fileNodeId);
            if (fileNode.GetType() == typeof(Folder))
            {
                Debug.Print("there be something to have folder in SystemOpenFileRecordList\n");
                return false;
            }
            else if (fileNode.GetType() == typeof(File))
            {
                var file = (File)fileNode;
                var user = GetUserById(userId);
                //检查文件状态（是否有更新）
                //将缓冲区内容填充到对应的区域
                //将更新文件状态（是否产生更新）
                //也可手动调用updateFileBody
                updateFile(fileNodeId, userId, userOpenFileRecord);

                //将记录从用户打开文件表中删除
                user.openFileRecordList.Remove(fileNodeId);
                //将记录从系统打开文件表中删除
                DestorySystemOpenFileRecord(fileNodeId);
                //添加文件事件的记录
                UpdateFileEvent(fileNodeId, userId, FileEvent.Close, DateTime.Now);
            }
            else
            {
                Debug.Print("Can`t close a unknown file type,please check out the typeof fileNode");
                return false;
            }

            return true;
        }

        //将用户对文件的修改更新到文件中
        public bool updateFile(FileNodeId fileNodeId, UserId userId, UserOpenFileRecord userOpenFileRecord) {

            var fileNode = GetFileNodeById(fileNodeId);
            if (fileNode.GetType() != typeof(File))
            {
                Debug.Print("there be something to have folder in SystemOpenFileRecordList\n");
                return false;
            }

            var file = (File)fileNode;
            var user = GetUserById(userId);
            var buff = userOpenFileRecord.buff;
            var needFreeBlockAmount = (userOpenFileRecord.buff.GetSize() - file.GetFileSize());

            if (userOpenFileRecord.fileEvent == FileEvent.Write)
            {

                if (userOpenFileRecord.CreateTime < file.eventInfo.GetLastEventTime(FileEvent.Write).Value &&
                        file.eventInfo.GetLastEventTime(FileEvent.Write).Key != userId)
                {
                    //从打开到递交修改的这段时间,其他的用户对其进行过修改
                    Debug.Print("before you change the file,there have been something change in this file,please check out the change.\n");
                    return false;
                }
                else if (!this.disc.IsFreeBlockEnough(needFreeBlockAmount))
                {
                    //存储容量不足够
                    Debug.Print("file change failure,there is no such big memory to save it\n");
                    return false;
                }
                else
                {
                    //其他的情况都可以将修改更新到文件中

                    //将缓冲修改内容添加到源文件
                    file.fileBody.Copy(buff);
                    userOpenFileRecord.fileEvent = FileEvent.Empty;
                    //将分配的内存块号添加到源文件中
                    foreach (var index in Enumerable.Range(0, needFreeBlockAmount))
                    {
                        file.blockIdList.Add(this.disc.GetBlockFromFreeGroup(file.ID));
                    }
                    //更新文件事件的记录
                    file.eventInfo.AddEventTime(FileEvent.Write, userId, DateTime.Now);
                    return true;
                }
            }
            else if (userOpenFileRecord.fileEvent == FileEvent.Create)
            {
                Debug.Print("get a create event,but i don`t do anything\n");
                return false;
            }
            else if (userOpenFileRecord.fileEvent == FileEvent.Open)
            {
                Debug.Print("get a open event,but i don`t do anything\n");
                return false;
            }
            else
            {
                return false;
            }
        }

        private void UpdateFileEvent(FileNodeId fileNodeId, UserId userId, FileEvent fileEvent, DateTime time) {
            var file = GetFileNodeById(fileNodeId);
            var user = GetUserById(userId);
            file.eventInfo.AddEventTime(fileEvent, userId, time);
        }
        //-----------------系统打开文件表
        private void CreateSystemOpenFileRecord(FileNodeId fileNodeId) {
            if (!systemOpenFileRecordList.ContainsKey(fileNodeId))
            {
                systemOpenFileRecordList.Add(fileNodeId, new SystemOpenFileRecord(fileNodeId));
            }

            systemOpenFileRecordList[fileNodeId].CounterIncrease();

        }

        private SystemOpenFileRecord GetSystemOpenFileRecordById(FileNodeId fileNodeId) {
            return systemOpenFileRecordList[fileNodeId];
        }

        private bool DestorySystemOpenFileRecord(FileNodeId fileNodeId) {

            if (systemOpenFileRecordList[fileNodeId].counter > 1)
            {
                //还有其他的用户或正在打开这个文件
                Debug.Print("there still someone using it,you can`t destory it");
                systemOpenFileRecordList[fileNodeId].CounterDecrease();
                return false;
            }
            else if (systemOpenFileRecordList[fileNodeId].counter == 1)
            {
                //只有一个用户正在打开文件
                systemOpenFileRecordList.Remove(fileNodeId);
                return true;
            }
            else
            {
                Debug.Print("there somthing wrong to remain a zero counter openFileRecord");
                return false;
            }
        }
        //-----------------缓冲区操作
        public void WriterBuff(UserId userId, FileNodeId fileNodeId, FileBody fileBody) {
            var user = GetUserById(userId);
            user.openFileRecordList[fileNodeId].buff.Copy(fileBody);
        }
    }
}