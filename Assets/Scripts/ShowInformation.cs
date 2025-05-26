using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInformation : MonoBehaviour
{
    public float visibleRange;
    public GameObject canvasObject;
    public bool lookAtPlayer;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(player.transform.position, transform.position) < visibleRange)
        {
            canvasObject.SetActive(true);
            if (lookAtPlayer)
                canvasObject.transform.LookAt(player.transform);
        }
        else
        {
            canvasObject.SetActive(false);
        }
    }
}
