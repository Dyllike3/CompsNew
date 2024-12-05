using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellData
{
    public string spellName; // Name of the spell
    public string mainElement; // Main element of the spell
    public GameObject prefab; // Prefab associated with the spell
}

public class SpellBook : MonoBehaviour
{
    public List<SpellData> spellDefinitions; // Define all spells here
    private Dictionary<string, SpellData> spellDictionary; // Store spells for quick lookup

    void Awake()
    {
        InitializeSpellDictionary();
    }

    private void InitializeSpellDictionary()
    {
        spellDictionary = new Dictionary<string, SpellData>();

        foreach (var spell in spellDefinitions)
        {
            // Use lowercase keys to make matching case-insensitive
            string key = spell.spellName.ToLower();
            if (!spellDictionary.ContainsKey(key))
            {
                spellDictionary.Add(key, spell);
            }
            else
            {
                Debug.LogWarning($"Duplicate spell key detected: {key}. Skipping.");
            }
        }
    }

    public SpellData GetSpell(List<int> selectedElements)
    {
        // Convert selected elements into a key
        string key = GenerateSpellKey(selectedElements);

        // Look up the spell in the dictionary
        if (spellDictionary.TryGetValue(key, out SpellData spell))
        {
            return spell;
        }

        Debug.LogWarning($"No spell found for key: {key}");
        return null;
    }

    private string GenerateSpellKey(List<int> selectedElements)
    {
        if (selectedElements.Count == 0) return string.Empty;

        // Map the main element (first in the list)
        string mainElement = GetElementName(selectedElements[0]);

        if (selectedElements.Count == 1)
        {
            // Single-element key
            return mainElement.ToLower();
        }

        // Sort the remaining elements alphabetically
        List<string> sortedElements = new List<string>();
        for (int i = 1; i < selectedElements.Count; i++)
        {
            sortedElements.Add(GetElementName(selectedElements[i]));
        }
        sortedElements.Sort();

        return $"{mainElement}_{string.Join("_", sortedElements)}".ToLower();
    }


    public string GetElementName(int elementIndex)
    {
        // Map element indices to their names
        switch (elementIndex)
        {
            case 0: return "earth";
            case 1: return "fire";
            case 2: return "water";
            case 3: return "wood";
            case 4: return "metal";
            default: return "unknown";
        }
    }
}
