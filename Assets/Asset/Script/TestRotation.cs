using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    public AnimationCurve cubeCurve;
    public float time = 0;
    public float lerpSpeed;


    private void Update() {
        rotate();
    }

    private void rotate(){
        Quaternion test = Quaternion.Euler(0, 180, 0);
        
        if ( time <1 )
        {
            time += Time.deltaTime * lerpSpeed;
            transform.rotation = Quaternion.Lerp(
            Quaternion.identity, 
            Quaternion.Euler(0, 180, 0), 
            cubeCurve.Evaluate(time)
            );
        }
        if ( time >= 1 )
        {
            time -= Time.deltaTime*lerpSpeed;
            transform.rotation = Quaternion.Lerp(
            Quaternion.identity, 
            Quaternion.Euler(0, 0, 0), 
            cubeCurve.Evaluate(time)
            );

        }
        
    }
}
