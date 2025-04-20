using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAdvice : MonoBehaviour
{
    public Transform npc;
    public Vector3 offset;

    void Update()
    {
        transform.position = npc.position + offset; // O texto segue o gameObject da NPC Layla
    }
}
