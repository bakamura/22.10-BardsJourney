using UnityEngine;

[CreateAssetMenu(fileName = "Localization", menuName = "GameElement/Localization", order = 0)]
public class Localization : ScriptableObject {

    public string[] _tutorialDialogue;
    public string[] _creditsContent = new string[5];

}
