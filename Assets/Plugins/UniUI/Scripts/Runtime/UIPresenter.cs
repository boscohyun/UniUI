using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Boscohyun.UniUI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class UIPresenter : MonoBehaviour, IPresenter<UIPresenter>
    {
        protected enum AnimationState
        {
            Shown = default,
            Hidden,
            InShowAnimation,
            InHideAnimation,
        }
        
        [SerializeField]
        protected AnimationState animationState;

        protected readonly Subject<UIPresenter> ShowAnimationBeginSubject = new Subject<UIPresenter>();
        
        protected readonly Subject<UIPresenter> ShowAnimationEndSubject = new Subject<UIPresenter>();
        
        protected readonly Subject<UIPresenter> HideAnimationBeginSubject = new Subject<UIPresenter>();
        
        protected readonly Subject<UIPresenter> HideAnimationEndSubject = new Subject<UIPresenter>();

        public bool IsShowing => animationState == AnimationState.InShowAnimation ||
                                 animationState == AnimationState.Shown;
        
        public IObservable<UIPresenter> OnShowAnimationBegin => ShowAnimationBeginSubject;

        public IObservable<UIPresenter> OnShowAnimationEnd => ShowAnimationEndSubject;
        
        public IObservable<UIPresenter> OnHideAnimationBegin => HideAnimationBeginSubject;

        public IObservable<UIPresenter> OnHideAnimationEnd => HideAnimationEndSubject;

        #region Show

        public virtual void Show() => Show(default, null);
        
        public virtual void Show(bool skipAnimation) => Show(skipAnimation, null);
        
        public virtual void Show(Action<UIPresenter> callback) => Show(default, callback);
        
        public virtual void Show(bool skipAnimation, Action<UIPresenter> callback) =>
            ShowAsObservable(skipAnimation).Subscribe(presenter => callback?.Invoke(presenter));

        public virtual IObservable<UIPresenter> ShowAsObservable(bool skipAnimation = default)
        {
            if (animationState != AnimationState.Hidden)
            {
                return Observable.Return(this);
            }

            ShowAnimationBegin(skipAnimation);
            
            IObservable<UIPresenter> observable = skipAnimation
                ? Observable.Return(this)
                : ShowAnimationAsync().ToObservable().Select(_ => this);

            return observable.DoOnCompleted(ShowAnimationEnd);
        }

        protected virtual void ShowAnimationBegin(bool skip = default)
        {
            animationState = AnimationState.InShowAnimation;
            gameObject.SetActive(true);
            ShowAnimationBeginSubject.OnNext(this);
        }
        
        protected virtual async UniTask ShowAnimationAsync()
        {
            await UniTask.CompletedTask;
        }

        protected virtual void ShowAnimationEnd()
        {
            ShowAnimationEndSubject.OnNext(this);
            animationState = AnimationState.Shown;
        }

        #endregion

        #region Hide

        public virtual void Hide() => Hide(default, null);
        
        public virtual void Hide(bool skipAnimation) => Hide(skipAnimation, null);
        
        public virtual void Hide(Action callback) => Hide(default, callback);
        
        public virtual void Hide(bool skipAnimation, Action callback) =>
            HideAsObservable(skipAnimation).Subscribe(_ => callback?.Invoke());

        public virtual IObservable<Unit> HideAsObservable(bool skipAnimation = default)
        {
            if (animationState != AnimationState.Shown)
            {
                return Observable.Empty<Unit>();
            }
            
            HideAnimationBegin(skipAnimation);

            IObservable<Unit> observable = skipAnimation
                ? Observable.Return(Unit.Default)
                : HideAnimationAsync().ToObservable().Select(_ => Unit.Default);
            
            return observable.DoOnCompleted(HideAnimationEnd);
        }

        protected virtual void HideAnimationBegin(bool skip = default)
        {
            animationState = AnimationState.InHideAnimation;
            HideAnimationBeginSubject.OnNext(this);
        }
        
        protected virtual async UniTask HideAnimationAsync()
        {
            await UniTask.CompletedTask;
        }

        protected virtual void HideAnimationEnd()
        {
            gameObject.SetActive(false);
            HideAnimationEndSubject.OnNext(this);
            animationState = AnimationState.Hidden;
        }

        #endregion
    }
}
