using UnityEngine;

public class Button: MonoBehaviour
{
    public Animator animator;

    public void OnDrinkPotionButtonPressed()
    {
        TriggerAnimation("Drink");
    }

    public void OnLongRangeButtonPressed()
    {
        TriggerAnimation("LongRange");
    }

    public void OnShortRangeButtonPressed()
    {
        TriggerAnimation("ShortRange");
    }

    private void TriggerAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
        StartCoroutine(ReturnToStanceAfterAnimation(animationName));
    }

    private System.Collections.IEnumerator ReturnToStanceAfterAnimation(string animationName)
    {
        AnimatorStateInfo stateInfo;
        do
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        while (!stateInfo.IsName(animationName));

        yield return new WaitForSeconds(stateInfo.length);

        animator.ResetTrigger("Drink");
        animator.ResetTrigger("LongRange");
        animator.ResetTrigger("ShortRange");

    }
}
