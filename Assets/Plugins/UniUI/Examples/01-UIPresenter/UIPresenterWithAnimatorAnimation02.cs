using System;
using UniRx;

namespace Boscohyun.UniUI.Examples.UIPresenter
{
    public class UIPresenterWithAnimatorAnimation02 : UIPresenterWithAnimatorAnimation
    {
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
        }
        
        protected override void HideAnimationEnd()
        {
            animator.enabled = false;
            base.HideAnimationEnd();
        }
    }
}
