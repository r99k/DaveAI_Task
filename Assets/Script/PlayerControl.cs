using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Reference to the Animator component of the player
    public Animator anim;
    public float speed = 5f;
    public CharacterController cc;
    public Camera cam;
    [SerializeField] private Vector3 camOffset;
    [SerializeField] private Vector3 playerMove;
    Vector2 turn;
    public void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        playerMove = new Vector3(x, 0, z).normalized;
        if (playerMove.magnitude > 0)
        {

            cc.Move(playerMove * speed * Time.deltaTime);
            float targetAngel = Mathf.Atan2(playerMove.x, playerMove.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngel, 0);
            Quaternion newDirection = Quaternion.LookRotation(playerMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, 5f);
        }
        anim.SetFloat("speed", playerMove.magnitude);
    }
    private void Update()
    {
        Movement();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.collider.name);
    }
}
