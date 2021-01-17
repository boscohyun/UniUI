using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples.UIPresenter
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField]
        protected UniUI.UIPresenter uiPresenter;

        [SerializeField]
        protected Button showButton;

        [SerializeField]
        protected Button hideButton;

        protected virtual void Awake()
        {
            uiPresenter.OnShowAnimationBegin
                .Subscribe(_ => Debug.Log("UIPresenter Show Animation Begin"))
                .AddTo(gameObject);
            uiPresenter.OnShowAnimationEnd
                .Subscribe(_ => Debug.Log("UIPresenter Show Animation End"))
                .AddTo(gameObject);
            uiPresenter.OnHideAnimationBegin
                .Subscribe(_ => Debug.Log("UIPresenter Hide Animation Begin"))
                .AddTo(gameObject);
            uiPresenter.OnHideAnimationEnd
                .Subscribe(_ => Debug.Log("UIPresenter Hide Animation End"))
                .AddTo(gameObject);
            
            showButton.onClick.AsObservable().Subscribe(_ => uiPresenter.Show()).AddTo(gameObject);
            hideButton.onClick.AsObservable().Subscribe(_ => uiPresenter.Hide()).AddTo(gameObject);
        }
    }
}
