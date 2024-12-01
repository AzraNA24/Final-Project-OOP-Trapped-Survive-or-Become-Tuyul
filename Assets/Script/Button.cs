using UnityEngine;

public class ButtonActionHandler : MonoBehaviour
{
    public Animator animator;

    public void OnDrinkPotionButtonPressed()
    {
        animator.SetTrigger("Drink");
        StartCoroutine(ReturnToStanceAfterAnimation("Drink"));
    }

    public void OnLongRangeButtonPressed()
    {
        animator.SetTrigger("LongRange");
        StartCoroutine(ReturnToStanceAfterAnimation("LongRange"));
    }
    public void OnShortRangeButtonPressed()
    {
        animator.SetTrigger("ShortRange");
        StartCoroutine(ReturnToStanceAfterAnimation("ShortRange"));
    }

    private System.Collections.IEnumerator ReturnToStanceAfterAnimation(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (!stateInfo.IsName(animationName))
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        yield return new WaitForSeconds(stateInfo.length);

        animator.ResetTrigger("Drink"); 
    }
}
