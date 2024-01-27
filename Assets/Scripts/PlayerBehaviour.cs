using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector3 startPos = transform.position;
        startPos.y += 1f;
        Gizmos.DrawRay(startPos, transform.forward);
    }

}
