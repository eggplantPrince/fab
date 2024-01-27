using UnityEngine;

public class SmoothTrack : MonoBehaviour
{
    public Transform littleGuy;
    public Transform bigGuy;
    public float smoothSpeed;

    Transform ogTransform;
    Vector3 offset;

    void Start()
    {
        ogTransform = transform;
        offset = littleGuy.position - ogTransform.position;
    }

    void Update()
    {
        Vector3 targetPosition = littleGuy.position - offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(bigGuy);
    }
}
