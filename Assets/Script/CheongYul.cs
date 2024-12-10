using UnityEngine;

public class BounceController : MonoBehaviour
{
    public Animator StoneThrow;

    private Animator CheongYul;

    void Start()
    {
        
    }

    // Call this method when the bounce animation starts
    public void OnThrow()
    {
        CheongYul = GetComponent<Animator>();
        // Trigger the ball bounce animation
        StoneThrow.SetTrigger("Bounce");
    }
}
