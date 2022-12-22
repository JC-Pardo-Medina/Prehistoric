using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject movingPlatform;
    public float speed;

    private bool going = true;
    private bool returning = false;
    private Vector3 vectorA;
    private Vector3 vectorB;
    private Vector3 vectorC;
    private Vector3 offset;

    private void Start()
    {
        vectorA = new Vector3(pointA.transform.position.x, pointA.transform.position.y, pointA.transform.position.z);
        Destroy (pointA);
        vectorB = new Vector3(pointB.transform.position.x, pointB.transform.position.y, pointB.transform.position.z);
        Destroy (pointB);
        vectorC = new Vector3(movingPlatform.transform.position.x, movingPlatform.transform.position.y, movingPlatform.transform.position.z);
        offset = transform.position - vectorC;
        movingPlatform.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if (going)
        {
            transform.position = Vector3.MoveTowards(transform.position, vectorA + offset, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, vectorA + offset) < 0.5)
            {
                going = false;
                returning = true;
            }
        }
        if (returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, vectorB + offset, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, vectorB + offset) < 0.5)
            {
                going = true;
                returning = false;
            }
        }
    }
}
