using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float timeOffset;

    [SerializeField] Vector2 posOffset;


    [SerializeField] float leftLimit = 0f;
    [SerializeField] float rightLimit = 0f;
    [SerializeField] float bottomLimit = 0f;
    [SerializeField] float topLimit = 0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 startPos = transform.position;
        // Player Current Position
        Vector3 endPos = player.transform.position;
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z += -10;


        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        transform.position = new Vector3
           (
               Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
               Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
               transform.position.z
           );
    }

    private void OnDrawGizmos()
    {
        // Draw a box around our camera boundary
        Gizmos.color = Color.red;

        // Create A LINE sized BOX
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));

        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }
}
