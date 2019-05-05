using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private AnimationClip animation;

    void Start()
    {
        var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        animation = clipInfo[0].clip;
    }

    // Update is called once per frame
    void Update() {
        Destroy(this.gameObject, animation.length);
    }
}
