using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public GameObject Prefab;
    public bool Debuff = false;
    public Sprite Icon;
    public string Name;
    [Multiline]
    public string Description;
    public bool CanHaveMultiple;
    public int Price = 1;
}
