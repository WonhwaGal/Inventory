using System.Collections.Generic;
using UnityEngine;
using ForInventory;

[CreateAssetMenu(fileName = nameof(CharacteristicSO), menuName = "MyScriptables/CharacteristicSO")]
public class CharacteristicSO : ScriptableObject
{
    [SerializeField] private List<Characteristic> _characteristics;

    public IReadOnlyList<Characteristic> Characteristics => _characteristics;

    public Sprite FindByType(CharacteristicType type)
    {
        var result = _characteristics.Find(x => x.Type == type);
        if (result == null)
        {
            Debug.LogWarning($"{name}: element of type {type} was not found");
            return null;
        }

        return result.Image;
    }
}