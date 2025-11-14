using UnityEngine;
using TMPro;
using System;

public class SeedDisplay : MonoBehaviour
{
    [SerializeField] private TMP_InputField _seedInput;

    public void Start() {
        if (_seedInput == null) _seedInput = GetComponent<TMP_InputField>();
        GameSeed.Initialize(null);

        _seedInput.text = $"{GameSeed.Seed}";
        Debug.Log($"Seed: {GameSeed.Seed}");

        _seedInput.onEndEdit.AddListener(OnSeedEdited);
    }

    public void OnSeedEdited(string newText)
    {
        if (int.TryParse(newText, out int newSeed))
        {
            GameSeed.Initialize(newSeed);
            Debug.Log($"Game seed set to {newSeed}");
        }
        else
        {
            Debug.LogWarning($"Invalid seed input: {newText}");
            _seedInput.text = $"{GameSeed.Seed}";
        }
    }
}
