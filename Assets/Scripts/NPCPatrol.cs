using System.Collections;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    public float speed = 2f;
    public float waitTime = 2f;
    private bool isWaiting = false;

    private Vector3 target;
    private MeshRenderer meshRenderer;

    void Start()
    {
        target = pointB.position;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
    }

    void Update()
    {
        if (isWaiting) return;

        meshRenderer.enabled = true;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            StartCoroutine(WaitAndSwitch());
        }
    }

    IEnumerator WaitAndSwitch()
    {
        isWaiting = true;
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(waitTime);

        // Switch target and rotate to face new direction
        target = (target == pointA.position) ? pointB.position : pointA.position;
        transform.LookAt(target);
        speed = Random.Range(2f, 6f);

        isWaiting = false;
    }
}
