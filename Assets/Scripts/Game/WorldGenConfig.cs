using UnityEngine;

[CreateAssetMenu(menuName = "Configs/World Gen Config", fileName = "WorldGenConfig")]
public class WorldGenConfig : ScriptableObject
{
    [Min(1)]
    public int seed = 12345;
}
