using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator animator;
    public void OnDrinkPotionButtonPressed()
    {
        animator.SetTrigger("Drink");
    }

    // Method for the "Long Range" button
    public void OnLongRangeButtonPressed()
    {
        animator.SetTrigger("LongRange");
    }

    public void OnShortRangeButtonPressed()
    {
        animator.SetTrigger("ShortRange");
    }
}
