using System;
using Random = System.Random;

public class GameSeed
{
    private static int Seed { get; } = Environment.TickCount;
    // All randoms are derived from this seed, making sure the order of things doesn't change the outcome.
    // e.g. opening the chest before killing the enemy will give the same loot for the same seed
    // If we just used one random, we'd get different outcomes
    public static Random EnemyRandom { get; } = new(Seed);
    public static Random EnemyDropRandom { get; } = new(Seed);
    public static Random EnvironmentDropRandom { get; } = new(Seed);
    public static Random ChestRandom { get; } = new(Seed);

    public static Random LevelRandom { get; } = new(Seed);
}
