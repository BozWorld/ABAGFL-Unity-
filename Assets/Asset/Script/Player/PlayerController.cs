using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    [Range(10, 80)]
    public float Speed;
    private float RotationSpeed = 10f;
    public float maxRotationSpeed;
    public float RotationReturnValue;
    public float RotationCurrentValue;
    public Vector3 RawMovement { get; private set; }
    private Vector2 _inputVector;
    public PlayerCollision pc;
    private bool _canShoot;
    private float _shootCoolDownT;
    private float _shootCoolDown = 0.2f;
    public AnimationCurve rotateCurve;

    private bool _isShooting;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Enable();
        _isShooting = false;
        _input.Player.Fire.started += ctx => _isShooting = true;
        _input.Player.Fire.canceled += ctx => _isShooting = false;
        _input.Player.Move.performed += ctx => _inputVector = ctx.ReadValue<Vector2>();
        _input.Player.Move.canceled += _ => _inputVector = Vector2.zero;
    }
    private void Update()
    {
        Move();
        if (!_canShoot) UpdateShootCooldown();
        else if (_isShooting) Shoot();
    }

    #region Shooting
    void StartShootCooldown()
    {
        _shootCoolDownT = _shootCoolDown;
        _canShoot = false;
    }

    private void UpdateShootCooldown()
    {
        _shootCoolDownT -= Time.deltaTime;
        if (_shootCoolDownT <= 0) _canShoot = true;
    }

    void Shoot()
    {
        
        pc.BulletInstance(Speed);
        StartShootCooldown();
    }
    #endregion

    #region Movement
    private void Move()
    {
        //récupère l'input du player
        RawMovement = new Vector3(_inputVector.x, _inputVector.y);

        //la rotation voulue quand l'avion se déplace
        Vector3 wantedRotation = new Vector3(-90f, 0, -_inputVector.x * 30f);
        Quaternion myRotation = Quaternion.Euler(wantedRotation);

        //la rotation effectué quand le joueur se déplace
        if (RawMovement.x < 0)
        {
            RotationCurrentValue = 0;
            //c'est ce qui nous permet de lerp la valeur selon l'animation
            RotationCurrentValue += Time.deltaTime * RotationSpeed;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(-90f,0, maxRotationSpeed*(-_inputVector.x)),
                rotateCurve.Evaluate(RotationCurrentValue)
            );
            //le timer qui fait que si le joueur appuie pas au bout d'un temps bah il reset la valeur de l'avion
        }
        if (RawMovement.x > 0 )
        {
            RotationCurrentValue = 0;
            RotationCurrentValue += Time.deltaTime * RotationSpeed;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(-90f,0, maxRotationSpeed*(-_inputVector.x)),
                rotateCurve.Evaluate(RotationCurrentValue)
            );
        }
        else
        {
            //je lerp mon retour historie d'avoir un truc bien smooth tu capte
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90,0,0), RotationCurrentValue);
            
        }
        
        //si ça fait quelque temps que le joueur a pas bougé je reset
        //je déplace mon vaisseaux vrai blanc fait vrai chose, btw je pense a tenter d'autre fonction mais ça je sais pas trop ptet lerp le déplacement?
        transform.position += RawMovement * Speed * Time.deltaTime;
    }
    #endregion
}