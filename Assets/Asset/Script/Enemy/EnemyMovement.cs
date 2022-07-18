using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Range (0,5)]
    public float EnemySpeed;
    public GameObject Player;
    public AnimationCurve SpeedCurve;

    private void Update() {
        Move();
    }

    private void Move(){
        EnemySpeed += SpeedCurve.Evaluate(Time.deltaTime * EnemySpeed);
        Vector3 playerDir = new Vector3(Player.transform.position.x, transform.position.y, 0);
        transform.position = Vector3.Lerp(transform.position, playerDir, EnemySpeed);
    }
}
 