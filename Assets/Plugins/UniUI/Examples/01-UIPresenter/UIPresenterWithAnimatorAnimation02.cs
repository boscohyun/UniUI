using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Boscohyun.UniUI.Examples.UIPresenter
{
    public class UIPresenterWithAnimatorAnimation02 : UniUI.UIPresenter
    {
        private static readonly int AnimatorHashHide = Animator.StringToHash("Hide");
        
        private static readonly int AnimatorHashShow = Animator.StringToHash("Show");

        [SerializeField]
        private Animator animator;

        private void Awake()
        {
            animator.enabled = false;
        }

        public override IObservable<UniUI.UIPresenter> ShowAsObservable(bool skipAnimation = default)
        {
            if (animationState != AnimationState.Hidden)
            {
                return Observable.Return(this);
            }

            ShowAnimationBegin(skipAnimation);
            
            IObservable<UniUI.UIPresenter> observable = skipAnimation
                ? Observable.Return(this)
                : Observable.FromCoroutine(ShowAnimation)
                    .Select(_ => this);

            return observable
                .DelayFrame(1)
                .Finally(ShowAnimationEnd);
        }

        protected override void ShowAnimationBegin(bool skip = default)
        {
            base.ShowAnimationBegin(skip);
            animator.enabled = true;
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

        protected override void ShowAnimationEnd()
        {
            animator.enabled = false;
            base.ShowAnimationEnd();
        }
        
        public override IObservable<Unit> HideAsObservable(bool skipAnimation = default)
        {
            if (animationState != AnimationState.Shown)
            {
                return Observable.Empty<Unit>();
            }
            
            HideAnimationBegin(skipAnimation);

            IObservable<Unit> observable = skipAnimation
                ? Observable.Return(Unit.Default)
                : Observable.FromCoroutine(HideAnimation);
            
            return observable
                .DelayFrame(1)
                .Finally(HideAnimationEnd);
        }

        protected override void HideAnimationBegin(bool skip = default)
        {
            base.HideAnimationBegin(skip);
            animator.enabled = true;
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

        protected override void HideAnimationEnd()
        {
            animator.enabled = false;
            base.HideAnimationEnd();
        }
    }
}
