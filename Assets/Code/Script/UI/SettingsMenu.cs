using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    [Header("Audio")]
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private SliderData _musicVol;
    [SerializeField] private SliderData _sfxVol;

    private void Start() {
        _musicVol.slider.value = GameManager.musicVol;
        _sfxVol.slider.value = GameManager.sfxVol;
    }

    public void SetVolume(int volToChange) {
        SliderData slider = GetSliderVol(volToChange);
        _mixer.SetFloat(slider.name, Mathf.Log10(slider.slider.value) * 20); // Slider lowest must be 0.001 !!!
        GetManagerVol(volToChange) = Mathf.Round(slider.slider.value * 1000f) / 1000f;
        // SaveSystem.SaveProgress(GameManager.currentSave); // Save system is still to be implemented
    }

    private ref float GetManagerVol(int volVarID) {
        switch (volVarID) {
            case 0: return ref GameManager.musicVol;
            case 1: return ref GameManager.sfxVol;
            default:
                Debug.Log("Error Fetching Volume Var From Manager, used sfxVol Instead");
                return ref GameManager.sfxVol;
        }
    }

    private SliderData GetSliderVol(int volVarID) {
        switch (volVarID) {
            case 0: return _musicVol;
            case 1: return _sfxVol;
            default:
                Debug.Log("Error Fetching Volume Slider Value, used sfxVolSlider Instead");
                return _sfxVol;
        }
    }

}

[System.Serializable]
public class SliderData {

    public string name;
    public Slider slider;

}