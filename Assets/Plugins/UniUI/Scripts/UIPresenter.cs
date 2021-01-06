using System;
using UniRx;
using UnityEngine;

namespace Boscohyun.UniUI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIPresenter : MonoBehaviour
    {
        private enum AnimationState
        {
            Shown = 0,
            Hidden,
            InShowAnimation,
            InHideAnimation,
        }
        
        [SerializeField]
        private AnimationState animationState;

        public bool IsShowing => animationState == AnimationState.InShowAnimation ||
                                 animationState == AnimationState.Shown;

        #region Show

        public void Show() => Show(default, null);
        
        public void Show(bool skipAnimation) => Show(skipAnimation, null);
        
        public void Show(Action<UIPresenter> callback) => Show(default, callback);
        
        public void Show(bool skipAnimation, Action<UIPresenter> callback) =>
            ShowAsObservable(skipAnimation).Subscribe(presenter => callback?.Invoke(presenter));
        
        public virtual IObservable<UIPresenter> ShowAsObservable(bool skipAnimation = default)
        {
            if (animationState != AnimationState.Hidden)
            {
                return Observable.Return(this);
            }
            
            return ShowAnimation(skipAnimation)
                .Select(_ => this)
                .Finally(Shown);
        }
        
        // To be async or coroutine?
        protected virtual IObservable<Unit> ShowAnimation(bool skip = default)
        {
            animationState = AnimationState.InShowAnimation;
            gameObject.SetActive(true);
            return Observable.Empty<Unit>();
        }

        protected virtual void Shown()
        {
            animationState = AnimationState.Shown;
        }

        #endregion

        #region Hide

        public void Hide() => Hide(default, null);
        
        public void Hide(bool skipAnimation) => Hide(skipAnimation, null);
        
        public void Hide(Action callback) => Hide(default, callback);
        
        public void Hide(bool skipAnimation, Action callback) =>
            HideAsObservable(skipAnimation).Subscribe(_ => callback?.Invoke());

        public virtual IObservable<Unit> HideAsObservable(bool skipAnimation = default)
        {
            if (animationState != AnimationState.Shown)
            {
                return Observable.Empty<Unit>();
            }
            
            return HideAnimation(skipAnimation)
                .Finally(Hidden);
        }

        // To be async or coroutine?
        protected virtual IObservable<Unit> HideAnimation(bool skip = default)
        {
            animationState = AnimationState.InHideAnimation;
            gameObject.SetActive(false);
            return Observable.Empty<Unit>();
        }

        protected virtual void Hidden()
        {
            animationState = AnimationState.Hidden;
        }

        #endregion
    }

    public class UIPresenter<T> : UIPresenter where T : UIPresenter<T>
    {
        public void Show(Action<T> callback) => Show(default, callback);
        
        public void Show(bool skipAnimation, Action<T> callback) =>
            ShowAsObservable(skipAnimation).Subscribe(presenter => callback?.Invoke((T) presenter));
        
        public virtual IObservable<TResult> ShowAsObservable<TResult>(bool skipAnimation = default) where TResult : T
        {
            return ShowAsObservable(skipAnimation).Select(presenter => (TResult) presenter);
        }
    }
}
