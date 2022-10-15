using UnityEngine;

[CreateAssetMenu(fileName = "Rythm", menuName = "GameElement/Rythm", order = 1)]
public class Rythm : ScriptableObject {

    public int Size {
        get {
            if (drumNote[4] == DrumNote.Null) {
                if (drumNote[3] == DrumNote.Null) return 3;
                return 4;
            }
            return 5;
        }
    }
            
    public enum DrumNote {
        Null,
        Green,
        Yellow,
        Red,
        Blue
    }
    public DrumNote[] drumNote = new DrumNote[5];

    public static DrumNote IntToDrumNote(int drumNote) {
        switch (drumNote) {
            case 1: return DrumNote.Green;
            case 2: return DrumNote.Yellow;
            case 3: return DrumNote.Red;
            case 4: return DrumNote.Blue;
            default: return DrumNote.Null;
        }
    }
}
