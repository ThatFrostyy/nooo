using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Game Config", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Min(1)]
    public int targetFrameRate = 60;
}
