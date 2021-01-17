using System.Collections;
using UnityEngine;

namespace Boscohyun.UniUI.Examples.UIPresenter
{
    public class UIPresenterWithAnimatorAnimation : UniUI.UIPresenter
    {
        private static readonly int AnimatorHashHide = Animator.StringToHash("Hide");
        
        private static readonly int AnimatorHashShow = Animator.StringToHash("Show");

        [SerializeField]
        private Animator animator;

        protected override void ShowAnimationBegin(bool skip = default)
        {
            base.ShowAnimationBegin(skip);
            animator.Play(AnimatorHashShow, 0, skip ? 1f : 0f);
        }

        protected override IEnumerator ShowAnimation()
        {
            while (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AnimatorHashShow)
            {
                yield return null;
            }
            
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }
        }

        protected override void HideAnimationBegin(bool skip = default)
        {
            base.HideAnimationBegin(skip);
            animator.Play(AnimatorHashHide, 0, skip ? 1f : 0f);
        }

        protected override IEnumerator HideAnimation()
        {
            while (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AnimatorHashHide)
            {
                yield return null;
            }
            
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }
        }
    }
}
