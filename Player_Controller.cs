using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[SelectionBase]
public class Player_Controller : MonoBehaviour
{
    [Header("Movement Attributes")]
    [SerializeField] float _moveSpeed=50f;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D _rb;
private Vector2 _moveDir = Vector2.zero;
void Update()
{
    Gatherinput();
}

private void FixedUpdate()
{
    MovementUpdate();
}
private void Gatherinput()
{
    _moveDir.x=Input.GetAxisRaw("Horizontal");
    _moveDir.y=Input.GetAxisRaw("Vertical");
        print(_moveDir);
}

private void MovementUpdate()
{
  _rb.velocity=_moveDir*_moveSpeed*Time.fixedDeltaTime;
}

}
