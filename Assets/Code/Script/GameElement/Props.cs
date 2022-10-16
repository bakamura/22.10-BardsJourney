using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Try using polymorphism later
public class Props : MonoBehaviour {

    private void Start() {
        GetComponent<MovingElement>().onCompletePath.AddListener(ResetPosition);
    }

    private void ResetPosition() {

    }

}
