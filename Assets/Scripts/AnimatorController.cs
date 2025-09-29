using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMoveInput(float moveInput)
    {
        animator.SetFloat("MoveInput", moveInput);
    }
}
