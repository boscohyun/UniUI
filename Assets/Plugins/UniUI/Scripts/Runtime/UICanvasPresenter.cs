using System;
using UniRx;
using UnityEngine;

namespace Boscohyun.UniUI
{
    [RequireComponent(typeof(Canvas))]
    public class UICanvasPresenter : UIPresenter
    {
        public enum HideMode
        {
            UseCanvas = default,
            UseGameObject,
        }

        [SerializeField]
        protected Canvas canvas;

        protected HideMode hideMode;

        protected override void ShowAnimationBegin(bool skip = default)
        {
            animationState = AnimationState.InShowAnimation;

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            if (!canvas.enabled)
            {
                canvas.enabled = true;
            }

            ShowAnimationBeginSubject.OnNext(this);
        }

        public override void Hide() => Hide(default, default, null);

        public override void Hide(bool skipAnimation) => Hide(default, skipAnimation, null);

        public override void Hide(Action callback) => Hide(default, default, callback);

        public virtual void Hide(HideMode hideMode, bool skipAnimation, Action callback)
        {
            this.hideMode = hideMode;
            HideAsObservable(skipAnimation).Subscribe(_ => callback?.Invoke());
        }

        protected override void HideAnimationEnd()
        {
            switch (hideMode)
            {
                case HideMode.UseCanvas:
                    canvas.enabled = false;
                    break;
                case HideMode.UseGameObject:
                    gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            HideAnimationEndSubject.OnNext(this);
            animationState = AnimationState.Hidden;
        }
    }
}
