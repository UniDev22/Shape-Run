using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform player;
    public Cube cube;
    public Vector3 offset;
    public float smoothTime = .25f;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {

    }

    void FixedUpdate()
    {
        if(!cube.isDead){
        if(player != null){
            Vector3 targetPos = player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
        }
    }
}
