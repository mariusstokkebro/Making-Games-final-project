using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Random = System.Random;

public class SeedDisplay : MonoBehaviour
{
    [SerializeField] private TMP_InputField seedInput;
    [SerializeField] public List<int> seeds;
    [SerializeField] public bool usePredefinedSeeds;

    public void Start() {
        if (seedInput == null) seedInput = GetComponent<TMP_InputField>();
        int seed = seeds.Count > 0 && usePredefinedSeeds
            ? seeds[new Random(Environment.TickCount).Next(0, seeds.Count - 1)]
            : Environment.TickCount;
        GameSeed.Initialize(seed);

        seedInput.text = $"{GameSeed.Seed}";

        seedInput.onEndEdit.AddListener(OnSeedEdited);
    }

    private void OnSeedEdited(string newText)
    {
        if (int.TryParse(newText, out int newSeed))
        {
            GameSeed.Initialize(newSeed);
        }
        else
        {
            Debug.LogWarning($"Invalid seed input: {newText}");
            seedInput.text = $"{GameSeed.Seed}";
        }
    }
}
