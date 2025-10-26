using UnityEngine;
using System.Collections;

public class Explosives : TrapBase
{
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject bomb;
    [SerializeField] private float spawnRate = 0.1f;
    [SerializeField] private float spawnY;

    private float spawnMinX;
    private float spawnMaxX;
    private float spawnMinZ;
    private float spawnMaxZ;
    private void Start()
    {
        GetHeroes(Job.ranger);

        if(heroes > 9)
        {
            Pass();
            return;
        }

        Vector3 min = floor.transform.position - floor.transform.localScale / 2;
        spawnMinX = min.x;
        spawnMinZ = min.z;
        
        Vector3 max = min + floor.transform.localScale;
        spawnMaxX = max.x;
        spawnMaxZ = max.z;

        spawnRate *= heroes;

        StartCoroutine(Spawner());
        StartCoroutine(Timer());
    }


    private IEnumerator Spawner()
    {
        while (!end)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 spawnPos = new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnY, Random.Range(spawnMinZ, spawnMaxZ));
            Instantiate(bomb, spawnPos, Quaternion.identity);
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        Pass();
    }
}
