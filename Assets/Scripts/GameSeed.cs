using System;
using UnityEngine;
using Random = System.Random;

public class GameSeed
{
    public static int Seed {get; private set;} = Environment.TickCount;
    // All randoms are derived from this seed, making sure the order of things doesn't change the outcome.
    // e.g. opening the chest before killing the enemy will give the same loot for the same seed
    // If we just used one random, we'd get different outcomes
    private static Random _enemyRandom;
    private static Random _lootTableRandom;
    private static Random _environmentDropRandom;
    private static Random _chestRandom;
    private static Random _levelRandom;

    public static Random EnemyRandom => _enemyRandom;
    public static Random LootTableRandom => _lootTableRandom;
    public static Random EnvironmentDropRandom => _environmentDropRandom;
    public static Random ChestRandom => _chestRandom;
    public static Random LevelRandom => _levelRandom;

   // For use with game manager / Setting seed in main menu
    public static void Initialize(int? seed)
    {
        Seed = seed ?? Environment.TickCount;
        _enemyRandom = new(Seed);
        _lootTableRandom = new(Seed);
        _environmentDropRandom = new(Seed);
        _chestRandom = new(Seed);
        _levelRandom = new(Seed);
    }
}
