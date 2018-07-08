using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace OS_CD {
    public class Disc
    {
        public static Disc Instance = new Disc();
        //记录空闲的块数
        private int freeAmount = 0;
        public void Init(int GroupMaxAmount, int DiscMaxAmount)
        {
            DiscBlockGroup.TopBlockGroup.Init(GroupMaxAmount);

            for (int i = DiscMaxAmount; i >= 0; i--)
            {
                AddBlockToGroup(i);
            }

            Debug.Print("Disc init finish\n");
        }

        public void AddBlockToGroup(int blockId)
        {
            freeAmount++;
            DiscBlockGroup.AddDiscBlock(blockId);
        }

        public int GetBlockFromGroup()
        {
            int i = DiscBlockGroup.GetDiscBlock();
            Debug.Print("get a freeblock :" + i + "\n");
            if (i != -1) freeAmount--;
            return i;
        }

        public bool IsFreeBlockEnough(int amount)
        {
            return amount <= freeAmount ? true : false;
        }
    }
    //成组
    internal class DiscBlockGroup
    {
        //单例,保存最上层的组
        public static DiscBlockGroup TopBlockGroup = new DiscBlockGroup(0);

        private int maxAmount = 0;

        public void Init(int maxAmount)
        {
            this.maxAmount = maxAmount;
        }
        //初始化组的大小
        private DiscBlockGroup(int maxAmount)
        {
            this.maxAmount = maxAmount;
        }
        //具体组中的内容的容器
        private List<FreeBlock> blockGroups = new List<FreeBlock>();
        //往容器中添加空闲块块号
        //返回的是最上层的DiscBlockGroup对象
        public static bool AddDiscBlock(int blockId)
        {
            if (TopBlockGroup.blockGroups.Count == 0)
            {
                TopBlockGroup.blockGroups.Add(new FreeBlock(blockId, new DiscBlockGroup(TopBlockGroup.maxAmount)));
                Debug.Print("add a freeblock:" + blockId + "\n");
                return true;
            }
            else if (TopBlockGroup.blockGroups.Count < TopBlockGroup.maxAmount)
            {
                TopBlockGroup.blockGroups.Add(new FreeBlock(blockId, null));
                Debug.Print("add a freeblock:" + blockId + "\n");
                return true;
            }
            else if (TopBlockGroup.blockGroups.Count == TopBlockGroup.maxAmount)
            {
                //创建上一层的组,将当前组放入上层组的第一个块中
                //这时,上层组就是顶层组
                DiscBlockGroup upperBlockGroup = new DiscBlockGroup(TopBlockGroup.maxAmount);
                DiscBlockGroup lowerBlockGroup = DiscBlockGroup.TopBlockGroup;
                DiscBlockGroup.TopBlockGroup = upperBlockGroup;
                DiscBlockGroup.AddDiscBlock(blockId);
                DiscBlockGroup.TopBlockGroup.blockGroups[0].LowerBlockGroup = lowerBlockGroup;
                return true;
            }
            else if (TopBlockGroup.blockGroups.Count > TopBlockGroup.maxAmount)
            {
                Debug.Print("add free block to blockGroups error\n");
                return false;
            }
            else return false;
        }

        public static int GetDiscBlock()
        {
            if (TopBlockGroup.blockGroups.Count == 0)
            {
                Debug.Print("there are not any free block to get\n");
                return -1;
            }
            else if (TopBlockGroup.blockGroups.Count == 1)
            {
                var freeBlockId = TopBlockGroup.blockGroups[0].BlockId;
                DiscBlockGroup.TopBlockGroup = DiscBlockGroup.TopBlockGroup.GetLowerBlockGroup();

                return freeBlockId;
            }
            else if (TopBlockGroup.blockGroups.Count <= TopBlockGroup.maxAmount)
            {
                var lastFreeBlock = TopBlockGroup.blockGroups[TopBlockGroup.blockGroups.Count - 1];
                var freeBlockId = lastFreeBlock.BlockId;
                TopBlockGroup.blockGroups.Remove(lastFreeBlock);
                return freeBlockId;
            }
            else
            {
                Debug.Print("there must be something wrong in TopBlockGroup`s blockGroup`s count \n");
                return -1;
            }
        }

        private DiscBlockGroup GetLowerBlockGroup()
        {
            return this.blockGroups.Count != 0 ? this.blockGroups[0].LowerBlockGroup : null;
        }

    }

    internal class FreeBlock
    {
        public int BlockId { get; }
        public DiscBlockGroup LowerBlockGroup { get; set; }

        public FreeBlock(int blockId, DiscBlockGroup lowerBlockGroup)
        {
            this.BlockId = blockId;
            this.LowerBlockGroup = lowerBlockGroup;
        }
    }



}