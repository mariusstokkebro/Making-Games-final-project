#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using UnityEngine.Assertions;
using Random = System.Random;

public static class LootTable
{
    private static readonly List<BaseItem> passiveItems = new (Resources.LoadAll<BaseItem>("Passive Items"));

    private static readonly List<BaseItem> weapons = new (Resources.LoadAll<BaseItem>("Weapons"));
    private static List<BaseItem> drops;

    private static readonly Random Rng = GameSeed.LootTableRandom; 

    public static BaseItem? GetPassiveDrop()
    {
        return GetDrop(true);
    }

    public static BaseItem? GetWeaponDrop()
    {
        return GetDrop(false);
    }

    private static BaseItem? GetDrop(bool getPassiveItemDrop)
    {
        if (getPassiveItemDrop)
        {
            drops = passiveItems;
        }
        if (drops.Count == 0) return null;
        
        
        double chance = Rng.NextDouble();
        if (chance > DropThreshold()) return null;
        
        // All items that could be dropped from this chance
        Rarity rarity = GetRarity(chance);
        List<BaseItem> sublist = drops.Where(predicate: item => item.rarity == rarity).ToList();
        if (sublist.Count == 0) return null;
        
        // Pick one
        int idx = Rng.Next(0, sublist.Count);
        BaseItem drop = sublist[idx];
        Debug.Log($"Picked drop: {drop} from [{string.Join(", ", sublist)}]");
        // drops.Remove(drop);
        return drop;
    }
    
    private static double DropThreshold()
    {
        var rarities = Enum.GetValues(typeof(Rarity));
        double sum = default;
        foreach (Rarity rarity in rarities)
        {
            sum += rarity.Chance();
        }

        return sum;
    }

    private static Rarity GetRarity(double chance)
    {
        Assert.IsTrue(chance <= DropThreshold());
        
        if (chance <= Rarity.Unfathomable.Chance())
            return Rarity.Unfathomable;
        if (chance <= Rarity.Imaginary.Chance())
            return Rarity.Imaginary;
        if (chance <= Rarity.Ethereal.Chance())
            return Rarity.Ethereal;
        if (chance <= Rarity.Supernatural.Chance())
            return Rarity.Supernatural;
        
        return Rarity.Natural;
    }
}
