using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{

    public Animator animator;

    public List<AnimatorSetup> animatiorSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        DEAD
    }

    public void Play(AnimationType type, float currentSpeedFactor = 1f)
    {
        animatiorSetups.ForEach(i => 
        {
            if (i.type == type) 
            {
                animator.SetTrigger(i.trigger); 
                animator.speed = i.speed * currentSpeedFactor;
            } 
        });
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            Play(AnimationType.RUN);
        if(Input.GetKeyDown(KeyCode.Alpha2))
            Play(AnimationType.DEAD);
        if(Input.GetKeyDown(KeyCode.Alpha3))
            Play(AnimationType.IDLE);
    }

}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
    public float speed = 1f;
}
