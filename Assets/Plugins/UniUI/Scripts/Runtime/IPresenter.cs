using System;
using UniRx;

namespace Boscohyun.UniUI
{
    public interface IPresenter<T> where T : UIPresenter
    {
        bool IsShowing { get; }

        IObservable<T> OnShowAnimationBegin { get; }

        IObservable<T> OnShowAnimationEnd { get; }

        IObservable<T> OnHideAnimationBegin { get; }

        IObservable<T> OnHideAnimationEnd { get; }

        void Show();

        void Show(bool skipAnimation);

        void Show(Action<T> callback);

        void Show(bool skipAnimation, Action<T> callback);

        IObservable<T> ShowAsObservable(bool skipAnimation = default);

        void Hide();

        void Hide(bool skipAnimation);

        void Hide(Action callback);

        void Hide(bool skipAnimation, Action callback);

        IObservable<Unit> HideAsObservable(bool skipAnimation = default);
    }
}
