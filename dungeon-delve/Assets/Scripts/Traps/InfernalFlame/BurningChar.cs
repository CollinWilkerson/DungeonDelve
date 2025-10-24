using UnityEngine;

public class BurningChar : MonoBehaviour
{
    private Vector3 targetPoint;
    private Vector2 targetMax;
    private Vector2 targetMin;
    [SerializeField] private float speed;

    public void Initialize(int _burnStacks, Vector2 max, Vector2 min)
    {
        targetMax = max;
        targetMin = min;

        transform.position = GetRandomVector();
        targetPoint = GetRandomVector();
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, targetPoint) < speed)
        {
            targetPoint = GetRandomVector();
        }
        //move to target point
        transform.position += (targetPoint - transform.position).normalized * speed;
    }

    private Vector2 GetRandomVector()
    {
        float randX = Random.Range(targetMin.x, targetMax.x);
        float randY = Random.Range(targetMin.y, targetMax.y);
        return new Vector2(randX, randY);
    }
}
