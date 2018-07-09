using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace OS_CD {


    #region 别名

    using FileNodeId = Int32;
    using UserId = Int32;
    using DiscBlockId = Int32;

    #endregion
    [Serializable]
    public class Disc
    {
        //记录空闲的块数
        private int freeAmount = 0;

        //已经占用的块以及文件id
        public Dictionary<DiscBlockId, FileNodeId> usageBlockList = new Dictionary<int, int>();
        //空闲块链表
        public List<DiscBlockId> freeBlockList = new List<FileNodeId>();
        public bool AddDiscBlock(int blockId)
        {
            freeBlockList.Add(blockId);
            return true;
        }
        public int GetDiscBlock()
        {
            return 1;
        }
        public void Init(int DiscMaxAmount)
        {
            for (int i = DiscMaxAmount; i >= 0; i--)
            {
                AddBlockToFreeGroup(i);
            }

            Debug.Print("Disc init finish\n");
        }

        public void AddBlockToFreeGroup(int blockId)
        {
            freeAmount++;
            freeBlockList.Add(blockId);

            usageBlockList.Remove(blockId);
        }

        public int GetBlockFromFreeGroup(FileNodeId fileNodeId)
        {

            int freeBlockId;
            if (freeBlockList.Count == 0)
            {
                freeBlockId = -1;
            }
            else
            {
               freeBlockId = freeBlockList.Last();
               freeBlockList.Remove(freeBlockId);
            }

            Debug.Print("get a freeblock :" + freeBlockId + "\n");
            if (freeBlockId != -1)
            {
                freeAmount--;
                usageBlockList.Add(freeBlockId, fileNodeId);
            }
            return freeBlockId;
        }

        public bool IsFreeBlockEnough(int amount)
        {
            return amount <= freeAmount ? true : false;
        }
    }

    
    



}