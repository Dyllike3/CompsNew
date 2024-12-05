using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    private List<int> selectedElements = new List<int>(); // List of selected elements
    private bool isSelecting = false; // Whether the player is in selection mode
    public Transform firePoint; // Where the spell spawns

    private ElementController elementController;
    private SpellBook spellBook;

    private void Start()
    {
        elementController = GetComponent<ElementController>();
        spellBook = GetComponent<SpellBook>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnterSelectionMode();
        }

        if (isSelecting)
        {
            HandleSelectionInput();

            if (Input.GetKeyDown(KeyCode.Space)) // End selection and cast spell
            {
                CastSpell();
                ExitSelectionMode();
            }
        }
    }

    void EnterSelectionMode()
    {
        isSelecting = true;
        selectedElements.Clear(); // Clear previous selection
        Debug.Log("Spell Selection Mode Activated!");
    }

    void ExitSelectionMode()
    {
        isSelecting = false;
        Debug.Log("Spell Selection Mode Deactivated!");
    }

    void HandleSelectionInput()
    {
        bool selectionChanged = false;

        // Explicitly map KeyCodes 1–5 to elements: metal, wood, water, fire, earth
        Dictionary<KeyCode, int> elementKeyMapping = new Dictionary<KeyCode, int>
    {
        { KeyCode.Alpha1, 4 }, // Metal
        { KeyCode.Alpha2, 3 }, // Wood
        { KeyCode.Alpha3, 2 }, // Water
        { KeyCode.Alpha4, 1 }, // Fire
        { KeyCode.Alpha5, 0 }  // Earth
    };

        foreach (var keyMapping in elementKeyMapping)
        {
            if (Input.GetKeyDown(keyMapping.Key) && elementController.HasEnoughEnergy(keyMapping.Value))
            {
                int elementIndex = keyMapping.Value;
                selectedElements.Add(elementIndex); // Add element index
                elementController.ConsumeEnergy(elementIndex, 10f); // Consume energy
                Debug.Log($"Selected Element: {spellBook.GetElementName(elementIndex)} ({keyMapping.Key})");
                selectionChanged = true;
            }
        }

        if (selectionChanged)
        {
            Debug.Log("Current Selection: " + string.Join(", ", selectedElements));
        }
    }



    void CastSpell()
    {
        if (selectedElements.Count == 0)
        {
            Debug.Log("No elements selected! Cannot cast spell.");
            return;
        }

        SpellData spell = spellBook.GetSpell(selectedElements);

        if (spell != null)
        {
            GameObject spellPrefab = spell.prefab;

            // Get mouse position and direction
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 direction = (mousePosition - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // Instantiate spell
            GameObject spellObject = Instantiate(spellPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
            spellObject.SetActive(true);

            Debug.Log($"Casted {spell.spellName} with main element: {spell.mainElement}");
        }
        else
        {
            Debug.LogWarning("No spell matched the selected elements.");
        }
    }
}
