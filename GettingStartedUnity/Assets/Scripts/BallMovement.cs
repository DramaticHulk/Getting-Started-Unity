using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Material sizeIncreaseMat;
    [SerializeField] private Material jumpIncreaseMat;
    [SerializeField] private Material speedIncreaseMat;
    [SerializeField] private Material speedDecreaseMat;
    [SerializeField] private Material sizeDecreaseMat;
    [SerializeField] private Material jumpDecreaseMat;
    [SerializeField] Transform groundCheckTransform;
    
    private Rigidbody rigidbodyCompoent;
    private Vector3 scaleTiny;
    private Vector3 scaleLarge;
    private Vector3 scaleRevert;
    public int jumpHeight = 5;
    private int moveSpeed;
    private bool jumpKeyPressed;
    private float horizontalInput;
    private float verticalInput;
    private bool sizePowerUpCollected;
    private bool jumpPowerUpCollected;
    private bool speedPowerUpCollected;
    private bool speedDebuffCollected;
    private bool sizeDebuffCollected;
    private bool jumpDebuffCollected;
    private bool powerUpCollected;
    private Material playerMat;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyCompoent = GetComponent<Rigidbody>();
        scaleRevert = new Vector3(2.0f, 2.0f, 2.0f);
        scaleLarge = new Vector3(4.0f, 4.0f, 4.0f);
        scaleTiny = new Vector3(1.0f, 1.0f, 1.0f);
        playerMat = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (sizePowerUpCollected || jumpPowerUpCollected || speedPowerUpCollected || speedDebuffCollected || sizeDebuffCollected || jumpDebuffCollected)
        {
            powerUpCollected = true;
        }
        else
        {
            powerUpCollected = false;
        }
        //Cancel Size Power Up
        if (Input.GetKeyDown(KeyCode.X) && transform.localScale == scaleLarge)
        {
            sizePowerUpCollected = false;
            transform.localScale = scaleRevert;
            gameObject.GetComponent<Renderer>().material = playerMat;
        }
        //Cancel Jump Power Up
        if (Input.GetKeyDown(KeyCode.X) && powerUpCollected)
        {
            jumpPowerUpCollected = false;
            gameObject.GetComponent<Renderer>().material = playerMat;

        }
        //Cancel Speed Power Up
        if (Input.GetKeyDown(KeyCode.X) && powerUpCollected)
        {
            speedPowerUpCollected = false;
            gameObject.GetComponent<Renderer>().material = playerMat;
        }
        //Cancel Speed Debuff
        if (Input.GetKeyDown(KeyCode.X) && powerUpCollected)
        {
            speedDebuffCollected = false;
            gameObject.GetComponent<Renderer>().material = playerMat;

        }
        // Cancel Size Debuff
        if (Input.GetKeyDown(KeyCode.X) && transform.localScale == scaleTiny)
        {
            sizeDebuffCollected = false;
            transform.localScale = scaleRevert;
            gameObject.GetComponent<Renderer>().material = playerMat;
        }
        //Cancel Jump Debuff
        if (Input.GetKeyDown(KeyCode.X) && powerUpCollected)
        {
            jumpDebuffCollected = false;
            gameObject.GetComponent<Renderer>().material = playerMat;
        }
        if (powerUpCollected)
        {
            if (speedPowerUpCollected)
            {
                moveSpeed = 14;
            }
            if (speedDebuffCollected)
            {
                moveSpeed = 3;
            }
        }
        else
        {
            moveSpeed = 7;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !jumpDebuffCollected)
        {
            jumpKeyPressed = true;
        }
        if (powerUpCollected)
        {
            if (sizePowerUpCollected)
            {
                jumpHeight = 9;
            } 
            else if (jumpPowerUpCollected)
            {
                jumpHeight = 20;
            }
            else if (sizeDebuffCollected)
            {
                jumpHeight = 2;
            }
            else
            {
                jumpHeight = 5;
            }
        }
        else
        {
            jumpHeight = 5;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
    }

    private void FixedUpdate()
    { 
        rigidbodyCompoent.velocity = new Vector3(horizontalInput*moveSpeed, rigidbodyCompoent.velocity.y, verticalInput * moveSpeed);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 2)
        {
            return;
        }

        if (jumpKeyPressed == true)
        {
            rigidbodyCompoent.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        //Size Power Up
        if (!powerUpCollected)
        {

            if (other.gameObject.layer == 7)
            {
                sizePowerUpCollected = true;

                gameObject.GetComponent<Renderer>().material = sizeIncreaseMat;

                Destroy(other.gameObject);
                transform.localScale = scaleRevert;
                transform.localScale = scaleLarge;
            }

        }
        //Jump Power Up
        if (!powerUpCollected)
        {

            if (other.gameObject.layer == 9)
            {
                jumpPowerUpCollected = true;

                gameObject.GetComponent<Renderer>().material = jumpIncreaseMat;

                Destroy(other.gameObject);
            }

        }
        //Speed Power Up
        if (!powerUpCollected)
        {
            if (other.gameObject.layer == 10)
            {
                speedPowerUpCollected = true;

                gameObject.GetComponent<Renderer>().material = speedIncreaseMat;

                Destroy(other.gameObject);
            }
        }
        //Speed Debuff
        if (!powerUpCollected)
        {
            if (other.gameObject.layer == 11)
            {
                speedDebuffCollected = true;

                gameObject.GetComponent<Renderer>().material = speedDecreaseMat;

                Destroy(other.gameObject);

            }
        }
       //Size Debuff
        if (!powerUpCollected)
        {
            if (other.gameObject.layer == 12)
            {
                sizeDebuffCollected = true;

                gameObject.GetComponent<Renderer>().material = sizeDecreaseMat;

                Destroy(other.gameObject);
                transform.localScale = scaleRevert;
                transform.localScale = scaleTiny;
            }
        } 
        //Jump Debuff
        if (!powerUpCollected)
        {
            if (other.gameObject.layer == 13)
            {
                jumpDebuffCollected = true;

                gameObject.GetComponent<Renderer>().material = jumpDecreaseMat;

                Destroy(other.gameObject);
            }
        }


    }
}
