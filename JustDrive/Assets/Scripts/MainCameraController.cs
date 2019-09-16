using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public float animationDuration = 2.0f;

    private Transform lookAt;

    private Vector3 startOffset;
    private Vector3 moveVector;
    private Vector3 animationOffset = new Vector3(0, 5, 5);

    private float transition = 0.0f;

    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    void Update()
    {
        moveVector = lookAt.position + startOffset;

        moveVector.x = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

        if (transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            //Animation at the start of a game
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
    }
}
