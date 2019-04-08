using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKillable, IDetectable
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public int Hp = 5;
    private int direction = 1;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    public GameObject playerMesh;
    public Vector3 startPosition;
    public GameObject teleporter;
    public GameObject bullet;
    public GameObject bombPrefab;
    public Transform camPos;

    public bool detectable = true;

    public bool jumpAbility = false;
    public bool teleport = true;

    public bool shoot = false;
    public bool bomb = true;

    public bool invisibility = false;
    public bool time = true;

    Camera cam; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = transform.position;
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        if(GameManager.instance.startGame)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPos.transform.position, Time.deltaTime * 5f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camPos.transform.rotation, Time.deltaTime * 5f);
        }
    }

    void Update()
    {
        if (GameManager.instance.startGame)
        {
            ControllerCheck();
        }

        if (controller.isGrounded)
        {

            moveDirection = new Vector3(-Input.GetAxis("Vertical"), 0.0f, Input.GetAxis("Horizontal"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump") && jumpAbility)
            {
                moveDirection.y = jumpSpeed;

            }
        }
        else
        {
            moveDirection = new Vector3(-Input.GetAxis("Vertical"), moveDirection.y, Input.GetAxis("Horizontal"));
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        if(moveDirection.z > 0)
        {
            playerMesh.transform.localScale = new Vector3(1, 1, 1);
            direction = 1;
        }
        else if(moveDirection.z < 0)
        {
            playerMesh.transform.localScale = new Vector3(1, 1, -1);
            direction = -1;
        }
        controller.Move(moveDirection * Time.deltaTime);
    }


    public void ControllerCheck()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }

        if (Input.GetButtonDown("Special"))
        {
            Special();
        }

        if (Input.GetButton("Jump") && !jumpAbility)
        {
            traversal();
        }else if (Input.GetButtonUp("Jump") && !jumpAbility)
        {
            controller.enabled = false;
            transform.position = teleporter.transform.position;
            controller.enabled = true;
            teleporter.transform.position = transform.position;
        }
    }

    public void Special()
    {
        if (invisibility && detectable)
        {
            StartCoroutine(StartInvisibility());
        }

        if(time && detectable)
        {
            StartCoroutine(StopTime());
        }
    }

    public void traversal()
    {
        if (teleport)
        {
            teleporter.transform.position += teleporter.transform.forward * 2 * Time.deltaTime;
        }
    }

    public void Interact()
    {
        if (shoot)
        {
            GameObject temp = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y , transform.position.z + (2 * direction)), playerMesh.transform.rotation);
            temp.transform.forward = transform.forward * direction;
        }else if (bomb)
        {
            GameObject temp = Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + (2 * direction)), playerMesh.transform.rotation);

        }
    }

    IEnumerator  StartInvisibility()
    {
        playerMesh.GetComponent<Renderer>().enabled = false;
        detectable = false;
        yield return new WaitForSeconds(3);
        detectable = true;
        playerMesh.GetComponent<Renderer>().enabled = true;

    }

    IEnumerator StopTime()
    {
        GameManager.instance.time = 0;
        detectable = false;
        Color temp = cam.backgroundColor;
        cam.backgroundColor = Color.black;
        yield return new WaitForSeconds(3);
        cam.backgroundColor = temp;
        GameManager.instance.time = 1;
        detectable = true;
    }

    public void Killed()
    {
        moveDirection = Vector3.zero;
        controller.enabled = false;
        gameObject.transform.position = startPosition;
        controller.enabled = true;
    }

    public void Damaged(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            Killed();
        }
    }

    public bool GetDetectable()
    {
        return detectable;
    }
}
