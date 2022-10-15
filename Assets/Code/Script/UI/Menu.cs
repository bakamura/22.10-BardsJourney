using UnityEngine;
using UnityEngine.SceneManagement;

public static class Menu {

    public static void TransitionScene(int sceneId) {
        SceneManager.LoadScene(sceneId);
    }

    public static void OpenMenu(ref RectTransform menuToClose, RectTransform menuToOpen) {
        menuToClose.gameObject.SetActive(false);
        menuToOpen.gameObject.SetActive(true);
        menuToClose = menuToOpen;
    }

}
