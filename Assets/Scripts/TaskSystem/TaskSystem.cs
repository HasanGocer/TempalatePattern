using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoSingleton<TaskSystem>
{
    public Contract focusContract;

    [System.Serializable]
    public class Contract
    {
        public List<int> objectTypeCount = new List<int>();
        public List<int> objectCount = new List<int>();
        public int maxItem;
    }

    public void FirstStart()
    {
        int taskObjectCount;
        if (GameManager.Instance.level < 10) taskObjectCount = 1;
        else taskObjectCount = 3;

        focusContract = NewContract(taskObjectCount, ItemData.Instance.field.objectCount);
    }
    public void ObjectCountUpdate(int objectTypeCount)
    {
        focusContract.objectCount[objectTypeCount]--;
        if (focusContract.objectCount[objectTypeCount] <= 0) focusContract.objectTypeCount[objectTypeCount] = -2;

        CheckContract();
    }

    private Contract NewContract(int taskCount, int maxItemCount)
    {
        Contract contract = new Contract();

        for (int i = 0; i < taskCount; i++)
        {
            bool isFree;
            int itemTypeCount;
            do
            {
                isFree = false;
                int childCount = 0;
                if (i == 0)
                {
                    if (GameManager.Instance.level < 10)
                        childCount = Random.Range(0, 4);
                    childCount += ItemData.Instance.field.objectCount;
                    childCount = childCount % PlacementSystem.Instance.objectCount;
                    itemTypeCount = childCount;
                }
                else if (i == 1)
                    itemTypeCount = contract.objectTypeCount[0] + 1 % PlacementSystem.Instance.objectCount;
                else
                    itemTypeCount = contract.objectTypeCount[0] + 2 % PlacementSystem.Instance.objectCount;



                for (int j = 0; j < contract.objectTypeCount.Count; j++)
                    if (contract.objectTypeCount[j] == itemTypeCount) isFree = true;
            }
            while (isFree);
            int itemCount = Random.Range(35, maxItemCount);

            contract.maxItem += itemCount;
            contract.objectTypeCount.Add(itemTypeCount);
            contract.objectCount.Add(itemCount);
        }

        return contract;
    }
    private void CheckContract()
    {
        bool isFinish = true;

        for (int i = 0; i < focusContract.objectCount.Count; i++)
            if (focusContract.objectCount[i] > 0) isFinish = false;

        if (isFinish)
            FinishSystem.Instance.FinishCheck();
    }
}