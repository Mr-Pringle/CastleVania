using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float minXClamp;
    public float maxXClamp;

    // Start is called before the first frame update
    private void LateUpdate()
    {
        if (GameManager.instance.playerInstance)
        {
            Vector3 cameraPos;

            cameraPos = transform.position;
            cameraPos.x = Mathf.Clamp(GameManager.instance.playerInstance.transform.position.x, minXClamp, maxXClamp);
            transform.position = cameraPos;
        }
    }
}
