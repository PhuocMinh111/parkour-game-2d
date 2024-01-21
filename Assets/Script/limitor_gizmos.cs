using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitor_gizmos : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] private Transform groundLimit;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawLine(startPoint.position, new Vector2(startPoint.position.x, startPoint.position.x + 100));
        Gizmos.DrawLine(startPoint.position, new Vector2(startPoint.position.x, startPoint.position.x - 100));

        Gizmos.DrawLine(endPoint.position, new Vector2(endPoint.position.x, endPoint.position.x + 100));
        Gizmos.DrawLine(endPoint.position, new Vector2(endPoint.position.x, endPoint.position.x - 100));

        Gizmos.DrawLine(new Vector2(groundLimit.position.x + 100, groundLimit.position.y), groundLimit.position);
        Gizmos.DrawLine(new Vector2(groundLimit.position.x - 100, groundLimit.position.y), groundLimit.position);


    }
}
