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

        protected virtual HideMode DefaultHideMode => HideMode.UseCanvas;

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

        public override void Hide() => Hide(DefaultHideMode, default, null);

        public override void Hide(bool skipAnimation) => Hide(DefaultHideMode, skipAnimation, null);

        public override void Hide(Action callback) => Hide(DefaultHideMode, default, callback);
        
        public virtual void Hide(HideMode hideMode, bool skipAnimation) => Hide(hideMode, skipAnimation, null);

        public virtual void Hide(HideMode hideMode, Action callback) => Hide(hideMode, default, callback);

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
