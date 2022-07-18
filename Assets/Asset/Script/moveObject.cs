using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{
    public AnimationCurve test;
    public GameObject goTo;
    public float speed;
    public Transform objectTransform;


    private void Update() {
        speed += test.Evaluate(Time.deltaTime * speed);
        transform.position = Vector3.Lerp(transform.position, goTo.transform.position,speed);
    }
}
