using UnityEngine;
using TMPro;

public class SeedDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;

    public void Start() {
        if (_title == null) _title = GetComponent<TMP_Text>();
        _title.text = $"Seed: {GameSeed._seed}";
        Debug.Log($"Seed: {GameSeed._seed}");
    }
}
