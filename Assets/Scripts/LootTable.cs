#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Items_and_Weapons;
using UnityEngine;
using UnityEngine.Assertions;
using Random = System.Random;

public static class LootTable
{
    private static readonly List<PassiveItemData> Drops = 
        new (Resources.LoadAll<PassiveItemData>("Passive Items"));

    private static readonly Random Rng = GameSeed.LootTableRandom; 

    public static PassiveItemData? GetDrop()
    {
        if (Drops.Count == 0) return null;
        
        
        double chance = Rng.NextDouble();
        if (chance > DropThreshold()) return null;
        
        // All items that could be dropped from this chance
        Rarity rarity = GetRarity(chance);
        List<PassiveItemData> sublist = Drops.Where(predicate: item => item.rarity == rarity).ToList();
        if (sublist.Count == 0) return null;
        
        // Pick one
        int idx = Rng.Next(0, sublist.Count);
        PassiveItemData drop = sublist[idx];
        Debug.Log($"Picked drop: {drop} from [{string.Join(", ", sublist)}]");
        
        Drops.Remove(drop);
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
