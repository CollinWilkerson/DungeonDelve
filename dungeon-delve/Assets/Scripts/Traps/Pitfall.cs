using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Pitfall : TrapBase
{
    [SerializeField] private int platformsToSpawn = 10;

    [Header("Gameobjects")]
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject floor_s;
    [SerializeField] private GameObject floor_m;
    [SerializeField] private GameObject floor_l;
    [SerializeField] private GameObject floor_goal;

    [Header("Control Sensitivity")]
    [SerializeField] private float groundedCheckDistance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveModifier;

    private bool first = true;
    private bool tryJump = false;
    private Vector3 spawnPos = new Vector3(0,0,0);
    private Rigidbody playerRb;
    private InputAction moveAction;
    private InputAction jumpAction;

    void Start()
    {
        GetHeroes(Job.ranger);

        if (heroes > 5)
        {
            Pass();
            return;
        }

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        SpawnPlatforms();
        playerRb = playerObj.GetComponent<Rigidbody>();
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (end)
        {
            return;
        }
        if(playerObj.transform.position.x > spawnPos.x)
        {
            Pass();
            return;
        }
        if(playerObj.transform.position.y < 0)
        {
            Fail();
            return;
        }

        if (CheckJump())
        {
            tryJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (end)
        {
            return;
        }
        playerRb.AddForce(Vector3.down * 100, ForceMode.Acceleration);
        playerRb.AddForce(HorizontalForce(), ForceMode.VelocityChange);
        if (tryJump)
        {
            Debug.Log("Jump");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            tryJump = false;
        }
    }

    private Vector3 HorizontalForce()
    {
        return Vector3.right * moveAction.ReadValue<Vector2>().x * moveModifier;
    }

    private void SpawnPlatforms()
    {
        for(int i = 0; i < platformsToSpawn - (heroes * 2); i++)
        {
            int j = Random.Range(0, 3);
            switch (j)
            {
                case 0:
                    MakePlatform(floor_s);
                    continue;
                case 1:
                    MakePlatform(floor_m);
                    continue;
                default:
                    MakePlatform(floor_l);
                    continue;
            }
        }

        Instantiate(floor_goal, spawnPos, Quaternion.identity);
    }

    private void MakePlatform(GameObject platform)
    {
        if (first)
        {
            spawnPos.x += platform.transform.localScale.x / 2;
            first = false;
        }
        Instantiate(platform, spawnPos, Quaternion.identity);
        spawnPos.x += platform.transform.localScale.x;
        //add random gap
        spawnPos.x += Random.Range(60,90);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        Fail();
    }

    private bool CheckJump()
    {
        return Physics.Raycast(playerObj.transform.position +
            Vector3.left * playerObj.transform.localScale.x / 2,
            Vector3.down, groundedCheckDistance)
            && jumpAction.IsPressed();
    }

    public override void TrapLossEffects()
    {
        return;
        //should reduce heroes damage by 1
    }
}
