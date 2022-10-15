using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private RectTransform _titleMenu;
    [SerializeField] private RectTransform _settingsMenu;
    [SerializeField] private RectTransform _creditsMenu;
    private RectTransform _currentMenu;

    private void Start() {
        _currentMenu = _titleMenu;
    }

    public void OpenMenu(RectTransform menuToOpen) {
        Menu.OpenMenu(ref _currentMenu, menuToOpen);
    }

    public void EnterLevel(int levelId) {
        Menu.TransitionScene(levelId);
    }

}
