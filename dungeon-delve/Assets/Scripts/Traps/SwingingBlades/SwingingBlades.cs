using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SwingingBlades : TrapBase
{
    [SerializeField] private GameObject blade;
    [SerializeField] private GameObject player;
    [SerializeField] private int bladesToSpawn = 12;

    private Vector3 spawnPos = new Vector3(0, 17, 10);
    private InputAction moveAction;

    void Start()
    {
        GetHeroes(Job.ranger);
        if(heroes > 5)
        {
            Pass();
        }

        moveAction = InputSystem.actions.FindAction("Move");
        SpawnBlades();
        player.GetComponent<BladesPlayer>().Initialize(this);
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (end)
        {
            return;
        }
        if(player.transform.position.z > spawnPos.z)
        {
            Pass();
            return;
        }
        if (!moveAction.triggered)
        {
            return;
        }

        //compencating for sticks being imperfect, I should really do this for everything
        if(moveAction.ReadValue<Vector2>().y > 0.9f)
        {
            player.transform.position = player.transform.position + Vector3.forward * 10;
        }
        if (moveAction.ReadValue<Vector2>().y < -0.9f)
        {
            player.transform.position = player.transform.position + Vector3.back * 10;
        }
    }

    private void SpawnBlades()
    {
        for(int i = 0; i < bladesToSpawn - heroes * 2; i++)
        {
            for(int j = 0; j < Random.Range(1, 5); j++)
            {
                Instantiate(blade, spawnPos, Quaternion.identity);
                spawnPos += Vector3.forward * 10;
            }
            spawnPos += Vector3.forward * 10;
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        Fail();
    }
}
