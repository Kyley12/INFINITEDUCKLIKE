using UnityEngine;

[CreateAssetMenu(fileName = "CutsceneData", menuName = "Cutscene/Cutscene Data", order = 1)]
public class CutsceneData : ScriptableObject
{
    [TextArea(3, 10)]
    public string textToType;
}
