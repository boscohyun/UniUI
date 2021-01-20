using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples.UIPresenter
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
        protected UniUI.UIPresenter uiPresenter;

        [SerializeField]
        protected Button showButton;

        [SerializeField]
        protected Button hideButton;

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
            showButton.onClick.AsObservable().Subscribe(_ => uiPresenter.Show()).AddTo(gameObject);
            hideButton.onClick.AsObservable().Subscribe(_ => uiPresenter.Hide()).AddTo(gameObject);
        }
        
        protected virtual void SubscribePresenterState()
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
        }
    }
}
