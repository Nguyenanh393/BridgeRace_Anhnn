using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColourSO", menuName = "ScriptableObjects/ColourData")]
public class ColourManager : ScriptableObject
{
    public List<Material> colourMaterials;
    
    public Material GetMaterial(ColourType colorType)
    {
        return colourMaterials[(int) colorType];
    }
}
