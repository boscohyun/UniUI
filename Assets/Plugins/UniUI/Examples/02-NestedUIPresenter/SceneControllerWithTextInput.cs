using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples.NestedUIPresenter
{
    public class SceneControllerWithTextInput : UIPresenter.SceneControllerWithSkipAnimationToggle
    {
        [SerializeField]
        private UIPresenterContainsNestedUIPresenter uiPresenterContainsNestedUIPresenter;
        
        [SerializeField]
        private InputField inputField;

        protected override void SubscribeButtons()
        {
            showButton.onClick.AsObservable()
                .Subscribe(_ => uiPresenterContainsNestedUIPresenter.Show(inputField.text, skipShowAnimationToggle.isOn))
                .AddTo(gameObject);
            hideButton.onClick.AsObservable().Subscribe(_ => uiPresenterContainsNestedUIPresenter.Hide(skipHideAnimationToggle.isOn))
                .AddTo(gameObject);
        }
        
        protected override void SubscribePresenterState()
        {
            uiPresenterContainsNestedUIPresenter.OnShowAnimationBegin
                .Subscribe(_ => Debug.Log("UIPresenter Show Animation Begin"))
                .AddTo(gameObject);
            uiPresenterContainsNestedUIPresenter.OnShowAnimationEnd
                .Subscribe(_ => Debug.Log("UIPresenter Show Animation End"))
                .AddTo(gameObject);
            uiPresenterContainsNestedUIPresenter.OnHideAnimationBegin
                .Subscribe(_ => Debug.Log("UIPresenter Hide Animation Begin"))
                .AddTo(gameObject);
            uiPresenterContainsNestedUIPresenter.OnHideAnimationEnd
                .Subscribe(_ => Debug.Log("UIPresenter Hide Animation End"))
                .AddTo(gameObject);
        }
    }
}
