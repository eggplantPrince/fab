using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHelper : MonoBehaviour
{
    [SerializeField]
    Invector.vCharacterController.vThirdPersonController controller;

    public void PickedUp() {
        controller.PickedUp();
    }
}
