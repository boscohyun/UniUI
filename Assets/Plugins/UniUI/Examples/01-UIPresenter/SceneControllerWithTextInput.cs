using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples.UIPresenter
{
    public class SceneControllerWithTextInput : SceneControllerWithSkipAnimationToggle
    {
        [SerializeField]
        private UIPresenterWithTextInjection uiPresenterWithTextInjection;
        
        [SerializeField]
        private InputField inputField;

        protected override void SubscribeButtons()
        {
            showButton.onClick.AsObservable()
                .Subscribe(_ => uiPresenterWithTextInjection.Show(inputField.text, skipShowAnimationToggle.isOn))
                .AddTo(gameObject);
            hideButton.onClick.AsObservable().Subscribe(_ => uiPresenterWithTextInjection.Hide(skipHideAnimationToggle.isOn))
                .AddTo(gameObject);
        }
        
        protected override void SubscribePresenterState()
        {
            uiPresenterWithTextInjection.OnShowAnimationBegin
                .Subscribe(_ => Debug.Log("UIPresenter Show Animation Begin"))
                .AddTo(gameObject);
            uiPresenterWithTextInjection.OnShowAnimationEnd
                .Subscribe(_ => Debug.Log("UIPresenter Show Animation End"))
                .AddTo(gameObject);
            uiPresenterWithTextInjection.OnHideAnimationBegin
                .Subscribe(_ => Debug.Log("UIPresenter Hide Animation Begin"))
                .AddTo(gameObject);
            uiPresenterWithTextInjection.OnHideAnimationEnd
                .Subscribe(_ => Debug.Log("UIPresenter Hide Animation End"))
                .AddTo(gameObject);
        }
    }
}
