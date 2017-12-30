using UnityEngine;

[CreateAssetMenu(menuName = "Terraforming Mars/New FactoryConfig")]
public class FactoryConfig : ScriptableObject
{
    public string Name;
    public int MinLevel;
    public bool UseTerraformingValueAsLevel;
    public string TransferStockToOtherFactory;
}
