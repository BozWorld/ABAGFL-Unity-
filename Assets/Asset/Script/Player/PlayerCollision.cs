using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour, IDoDamage
{
    public ObjectPool pool;
    public GameObject spawner;
    #region Collision

    private void OnTriggerEnter(Collider coll) {
        ProcessCollision(coll.gameObject,"trigger");
    }

    private void OnCollisionEnter(Collision coll) {
        ProcessCollision(coll.gameObject, "Collision");
    }

    private void ProcessCollision(GameObject coll, string message){
        if(coll.CompareTag("Damage"))
        {
            DoDamage();
            Debug.Log(message);
        }
    }
    #endregion

    #region InterfaceImplementation
    public void DoDamage()
    {
        Debug.Log("Damage");
    }
    #endregion

    public void BulletInstance(float Vspeed)
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if(bullet != null) {
            bullet.GetComponent<Bullet>().speed = Vspeed;
            bullet.transform.position = spawner.transform.position;
            bullet.SetActive(true);
        }
    }
}
