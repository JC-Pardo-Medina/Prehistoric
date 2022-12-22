using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RaptorAnimator : MonoBehaviour
{
    public Rigidbody body;
    Animator animator;

    private float animationSpeed;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (body == null)
            return;
        float speed = body.velocity.magnitude;

        animationSpeed = Mathf.Lerp(animationSpeed, speed / 20, Time.deltaTime*5);
        //if (speed)

        animator.SetFloat("speed", animationSpeed);
    }
}
