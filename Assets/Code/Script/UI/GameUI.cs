using UnityEngine;

public class GameUI : MonoBehaviour {

    [SerializeField] private AudioSource _bgmSource;

    [SerializeField] private RectTransform _hud;
    [SerializeField] private RectTransform _pauseMenu;
    [SerializeField] private RectTransform _settingsMenu;
    private RectTransform _currentMenu;

    private void Start() {
        _currentMenu = _hud;
    }

    public void OpenMenu(RectTransform menuToOpen) {
        Menu.OpenMenu(ref _currentMenu, menuToOpen);
        if (menuToOpen == _hud) {
            _bgmSource.Play();
            Time.timeScale = 1f;
        }

        else if (menuToOpen == _pauseMenu) {
            _bgmSource.Pause();
            Time.timeScale = 0f;
        }

    }

    public void EnterLevel(int levelId) {
        Time.timeScale = 1f;
        Menu.TransitionScene(levelId);
    }

}
