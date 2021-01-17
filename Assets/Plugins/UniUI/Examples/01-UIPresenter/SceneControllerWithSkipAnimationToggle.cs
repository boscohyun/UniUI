using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples.UIPresenter
{
    public class SceneControllerWithSkipAnimationToggle : SceneController
    {
        [SerializeField]
        private Toggle skipShowAnimationToggle;
        
        [SerializeField]
        private Toggle skipHideAnimationToggle;

        protected override void Awake()
        {
            uiPresenter.OnShowAnimationBegin
                .Subscribe(_ => Debug.Log($"UIPresenter Show Animation Begin. Skip({skipShowAnimationToggle.isOn})"))
                .AddTo(gameObject);
            uiPresenter.OnShowAnimationEnd
                .Subscribe(_ => Debug.Log($"UIPresenter Show Animation End. Skip({skipShowAnimationToggle.isOn})"))
                .AddTo(gameObject);
            uiPresenter.OnHideAnimationBegin
                .Subscribe(_ => Debug.Log($"UIPresenter Hide Animation Begin. Skip({skipHideAnimationToggle.isOn})"))
                .AddTo(gameObject);
            uiPresenter.OnHideAnimationEnd
                .Subscribe(_ => Debug.Log($"UIPresenter Hide Animation End. Skip({skipHideAnimationToggle.isOn})"))
                .AddTo(gameObject);
            
            showButton.onClick.AsObservable().Subscribe(_ => uiPresenter.Show(skipShowAnimationToggle.isOn)).AddTo(gameObject);
            hideButton.onClick.AsObservable().Subscribe(_ => uiPresenter.Hide(skipHideAnimationToggle.isOn)).AddTo(gameObject);
        }
    }
}
