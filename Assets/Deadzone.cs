using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
            GameManager.instance.RestartLevel();
    }
}
