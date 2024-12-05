using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    public float[] elementEnergy = new float[5]; // Energy bars for Earth, Fire, Water, Wood, Metal
    public float maxEnergy = 100f; // Maximum energy for each bar
    public float regenRate = 1f; // Energy regeneration rate per second

    void Update()
    {
        RegenerateEnergy();
    }

    public void RegenerateEnergy()
    {
        for (int i = 0; i < elementEnergy.Length; i++)
        {
            elementEnergy[i] = Mathf.Min(elementEnergy[i] + regenRate * Time.deltaTime, maxEnergy);
        }
    }

    public bool HasEnoughEnergy(int elementIndex)
    {
        return elementEnergy[elementIndex] >= 10f; // Check if energy is sufficient
    }

    public void ConsumeEnergy(int elementIndex, float amount)
    {
        elementEnergy[elementIndex] -= amount;
        elementEnergy[elementIndex] = Mathf.Max(0, elementEnergy[elementIndex]); // Clamp to 0
    }
}
