using UI;
using UnityEngine;
using Views;

[CreateAssetMenu(fileName = nameof(Prefabs), menuName = "MyScriptables/Prefabs")]
public class Prefabs : ScriptableObject
{
    public PickUpItemView PickUpPrefab;
    public FilterButton FilterButton;
    public GridItem GridItem;
}