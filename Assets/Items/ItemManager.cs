using UnityEngine;
using System.Collections.Generic;
using Kutie;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    [SerializeField] List<ItemSO> Items;

    readonly List<ItemSO> DebuffPool = new();
    readonly List<ItemSO> BuffPool = new();

    override protected void Awake()
    {
        base.Awake();
        foreach(var item in Items) {
            var pool = item.Debuff ? DebuffPool : BuffPool;
            pool.Add(item);
        }
    }

    void Shuffle(List<ItemSO> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public (ItemSO, ItemSO) GetRandomDebuffs()
    {
        Shuffle(DebuffPool);
        return (DebuffPool[0], DebuffPool[1]);
    }

    public (ItemSO, ItemSO, ItemSO) GetRandomBuffs()
    {
        Shuffle(BuffPool);
        return (BuffPool[0], BuffPool[1], BuffPool[2]);
    }

    public void AddItem(ItemSO item)
    {
        if(!item.CanHaveMultiple)
        {
            var pool = item.Debuff ? DebuffPool : BuffPool;
            pool.Remove(item);
        }
        Instantiate(item.Prefab);
    }
}
