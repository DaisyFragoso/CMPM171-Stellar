using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float xSmoothSpeed = 2f;
    public float ySmoothSpeed = 1f;

    public Vector3 offset = new Vector3(0f, 2f, -10f);

    public float minYFollowDifference = 1.5f;

    private float lockedY;

    void Start()
    {
        if (player != null)
        {
            lockedY = transform.position.y;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 currentPosition = transform.position;

        // always follow player horizontally
        float targetX = player.position.x + offset.x;

        //keeps the camera's current Y
        float targetY = lockedY;

        // adjust the Y if player is far enough away vertically
        float playerTargetY = player.position.y + offset.y;
        float yDifference = Mathf.Abs(playerTargetY - lockedY);

        if (yDifference > minYFollowDifference)
        {
            targetY = playerTargetY;
            lockedY = Mathf.Lerp(lockedY, targetY, ySmoothSpeed * Time.deltaTime);
        }

        Vector3 targetPosition = new Vector3(targetX, lockedY, offset.z);

        transform.position = Vector3.Lerp(
            currentPosition,
            targetPosition,
            xSmoothSpeed * Time.deltaTime
        );
    }
}
