using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagInfo
{
    private static List<BagInfo> Instances = new List<BagInfo>(){ 
        new(LootType.McDrink,50,1,"McDrink"),
        new(LootType.McBurg,100,1.5f,"McBurg"),
        new(LootType.PerfectBaguette,250,1.5f,"Perfect Baguette"),
        new(LootType.Air,0,1,"Air"),
        new(LootType.Server,150,2.2f,"Server"),
        new(LootType.Body,-1,1.5f,"x-x")
    };

    public static BagInfo GetInfo(LootType lt)
    {
        return Instances.Find(x => x.LootType == lt);
    }

    public LootType LootType { get; private set; }
    public int Value { get; private set; }
    public float Weight { get; private set; }
    public string Name { get; private set; }

    private BagInfo(LootType lootType, int value, float weight, string name)
    {
        LootType = lootType;
        Value = value;
        Weight = weight;
        Name = name;
    }
}

public enum LootType
{
    Body,
    McDrink,
    McBurg,
    PerfectBaguette,
    Air,
    Server
}