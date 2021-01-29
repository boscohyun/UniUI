using System;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples.UICanvasPresenter
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField]
        private Button previousSceneButton;

        [SerializeField]
        private AssetReference previousScene;

        [SerializeField]
        private Button nextSceneButton;

        [SerializeField]
        private AssetReference nextScene;

        [SerializeField]
        private UniUI.UICanvasPresenter uiCanvasPresenter;

        [SerializeField]
        private Button showButton;
        
        [SerializeField]
        private Toggle skipShowAnimationToggle;

        [SerializeField]
        private Button hideButton;
        
        [SerializeField]
        private Toggle skipHideAnimationToggle;

        [SerializeField]
        private Dropdown hideModeDropdown;

        protected virtual void Awake()
        {
            InitializeSceneButtons();
            SubscribeButtons();
            SubscribePresenterState();
        }

        protected void InitializeSceneButtons()
        {
            previousSceneButton.onClick.AsObservable().Subscribe(_ =>
            {
                if (!previousScene.RuntimeKeyIsValid())
                {
                    Debug.LogWarning($"{nameof(previousScene)} is invalid.");
                    return;
                }

                Addressables.LoadSceneAsync(previousScene);
            }).AddTo(gameObject);
            nextSceneButton.onClick.AsObservable().Subscribe(_ =>
            {
                if (!nextScene.RuntimeKeyIsValid())
                {
                    Debug.LogWarning($"{nameof(nextScene)} is invalid.");
                    return;
                }

                Addressables.LoadSceneAsync(nextScene);
            }).AddTo(gameObject);
        }

        protected virtual void SubscribeButtons()
        {
            showButton.onClick.AsObservable().Subscribe(_ => uiCanvasPresenter.Show(skipShowAnimationToggle.isOn))
                .AddTo(gameObject);
            hideButton.onClick.AsObservable().Subscribe(_ => uiCanvasPresenter.Hide(
                    (UniUI.UICanvasPresenter.HideMode) hideModeDropdown.value,
                    skipHideAnimationToggle.isOn))
                .AddTo(gameObject);
        }

        protected virtual void SubscribePresenterState()
        {
            uiCanvasPresenter.OnShowAnimationBegin
                .Subscribe(_ =>
                    Debug.Log($"UICanvasPresenter Show Animation Begin. Skip({skipShowAnimationToggle.isOn})"))
                .AddTo(gameObject);
            uiCanvasPresenter.OnShowAnimationEnd
                .Subscribe(
                    _ => Debug.Log($"UICanvasPresenter Show Animation End. Skip({skipShowAnimationToggle.isOn})"))
                .AddTo(gameObject);
            uiCanvasPresenter.OnHideAnimationBegin
                .Subscribe(_ =>
                    Debug.Log($"UICanvasPresenter Hide Animation Begin. Skip({skipHideAnimationToggle.isOn})"))
                .AddTo(gameObject);
            uiCanvasPresenter.OnHideAnimationEnd
                .Subscribe(
                    _ => Debug.Log($"UICanvasPresenter Hide Animation End. Skip({skipHideAnimationToggle.isOn})"))
                .AddTo(gameObject);
        }
    }
}
