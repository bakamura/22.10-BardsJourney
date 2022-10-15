using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TESTEFEEDBACK : MonoBehaviour {

    [SerializeField] private Image _image;

    public void ShowFB(bool good) {
        StopAllCoroutines();
        StartCoroutine(ShowFeedback(good));
    }

    private IEnumerator ShowFeedback(bool good) {
        _image.color = new Color(good ? 0 : 1, good ? 1 : 0, 0, 1);

        float alpha = 1;
        while (alpha > 0.15f) {
            alpha -= Time.deltaTime;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);

            yield return null;
        }
    }

}
