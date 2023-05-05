using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoSingleton<ItemSystem>
{
    public enum ItemStat
    {
        money = 0,
        item = 1
    }
    public enum PlaneStat
    {
        win = 1,
        lose = 0
    }
    public enum WallStat
    {
        plus = 0,
        minus = 1
    }
}