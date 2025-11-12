using System;
using UnityEngine;
using Random = System.Random;

public class GameSeed
{
    private static int _seed = Environment.TickCount;
    // All randoms are derived from this seed, making sure the order of things doesn't change the outcome.
    // e.g. opening the chest before killing the enemy will give the same loot for the same seed
    // If we just used one random, we'd get different outcomes
    private static Random _enemyRandom = new Random(_seed);
    private static Random _lootTableRandom = new Random(_seed);
    private static Random _environmentDropRandom = new Random(_seed);
    private static Random _chestRandom = new Random(_seed);
    private static Random _levelRandom = new Random(_seed);
   public static Random EnemyRandom => _enemyRandom;
   public static Random LootTableRandom => _lootTableRandom;
   public static Random EnvironmentDropRandom => _environmentDropRandom;
   public static Random ChestRandom => _chestRandom;
   public static Random LevelRandom => _levelRandom;

   // For use with game manager / Setting seed in main menu
    // public static void Initialize(int? seed)
    // {
    // _seed = seed ?? Environment.TickCount;
    // _enemyRandom = new(_seed);
    // _enemyDropRandom = new(_seed);
    // _environmentDropRandom = new(_seed);
    // _chestRandom = new(_seed);
    // _levelRandom = new(_seed);
    // }
}
