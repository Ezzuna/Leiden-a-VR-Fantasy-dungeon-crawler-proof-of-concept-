using System.Collections.Generic;
public class DataClass
{
   public class GoodData 
    {
        public string name;
        public string info;
        public int price;
        public bool isSelled;
        public string icon;
        public int id;        
        public int maxAdd;
        public int count;
        public bool isCanAdd;

        public ObjectItem TurnToObjectItem(DataClass.GoodData goodItem)
        {
            ObjectItem objectItem = new ObjectItem();
            objectItem.objName = goodItem.name;
            objectItem.objId = goodItem.id;
            objectItem.maxAdd = goodItem.maxAdd;
            objectItem.Icon = goodItem.icon;
            objectItem.count = goodItem.count;
            objectItem.isCanAdd = goodItem.isCanAdd;
            return objectItem;
        }

    }

    public class TaskData
    {
        public string id;
        public string name;
        public string info;
        public List<TaskRewardData> rewardData;
        public int taskType;
        public bool isFinish;
    }

    public class TaskRewardData
    {
        public int id;
        public int rewardType;
        public int count;
    }
}
