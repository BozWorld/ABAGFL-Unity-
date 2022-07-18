using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followerScript : MonoBehaviour
{
    public GameObject objectToFollow;
    public float moveSpeed;


    private void Update() {
        transform.position = Vector3.Lerp(transform.position, objectToFollow.transform.position, moveSpeed);
    }
}
