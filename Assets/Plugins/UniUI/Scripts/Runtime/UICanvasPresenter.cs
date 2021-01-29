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
            UseGameObject = default,
            UseCanvas,
        }

        [SerializeField]
        protected Canvas canvas;

        [SerializeField]
        protected HideMode hideMode;

        protected override void ShowAnimationBegin(bool skip = default)
        {
            animationState = AnimationState.InShowAnimation;

            if (!canvas.enabled)
            {
                canvas.enabled = true;
            }

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            ShowAnimationBeginSubject.OnNext(this);
        }

        public override void Hide() => Hide(hideMode, default, null);

        public override void Hide(bool skipAnimation) => Hide(hideMode, skipAnimation, null);

        public override void Hide(Action callback) => Hide(hideMode, default, callback);
        
        public virtual void Hide(HideMode hideModeParam, bool skipAnimation) => Hide(hideModeParam, skipAnimation, null);

        public virtual void Hide(HideMode hideModeParam, Action callback) => Hide(hideModeParam, default, callback);

        public virtual void Hide(HideMode hideModeParam, bool skipAnimation, Action callback)
        {
            hideMode = hideModeParam;
            HideAsObservable(skipAnimation).Subscribe(_ => callback?.Invoke());
        }

        protected override void HideAnimationEnd()
        {
            switch (hideMode)
            {
                case HideMode.UseGameObject:
                    gameObject.SetActive(false);
                    break;
                case HideMode.UseCanvas:
                    canvas.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(hideMode.ToString());
            }

            HideAnimationEndSubject.OnNext(this);
            animationState = AnimationState.Hidden;
        }
    }
}
