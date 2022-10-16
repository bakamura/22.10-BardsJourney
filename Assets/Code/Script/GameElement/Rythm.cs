using UnityEngine;

[CreateAssetMenu(fileName = "Rythm", menuName = "GameElement/Rythm", order = 1)]
public class Rythm : ScriptableObject {

    public Sprite[] noteSprites = new Sprite[5];
    
    public enum DrumNote {
        Null,
        Red,
        Purple,
        Blue,
        Yellow
    }
    public DrumNote[] drumNote = new DrumNote[7];
    public int Size {
        get {
            if (drumNote[6] == DrumNote.Null) {
                if (drumNote[5] == DrumNote.Null) {
                    if (drumNote[4] == DrumNote.Null) {
                        if (drumNote[3] == DrumNote.Null) return 3;
                        return 4;
                    }
                    return 5;
                }
                else return 6;
            }
            else return 7;
        }
    }

}
