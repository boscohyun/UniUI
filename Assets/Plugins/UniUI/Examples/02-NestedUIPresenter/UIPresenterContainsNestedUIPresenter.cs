using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Boscohyun.UniUI.Examples.NestedUIPresenter
{
    public class UIPresenterContainsNestedUIPresenter : UniUI.UIPresenter
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private NestedUIPresenterWithTextInjection nestedPresenter;

        [Header("Animation")]
        
        [SerializeField, Range(0f, 2f)]
        private float showDuration = 1f;
        
        [SerializeField, Range(0f, 2f)]
        private float hideDuration = 1f;

        private string _injectedText;

        private void Awake()
        {
            nestedPresenter.OnShowAnimationBegin
                .Subscribe(_ => Debug.Log("NestedUIPresenter Show Animation Begin"))
                .AddTo(gameObject);
            nestedPresenter.OnShowAnimationEnd
                .Subscribe(_ => Debug.Log("NestedUIPresenter Show Animation End"))
                .AddTo(gameObject);
            nestedPresenter.OnHideAnimationBegin
                .Subscribe(_ => Debug.Log("NestedUIPresenter Hide Animation Begin"))
                .AddTo(gameObject);
            nestedPresenter.OnHideAnimationEnd
                .Subscribe(_ => Debug.Log("NestedUIPresenter Hide Animation End"))
                .AddTo(gameObject);
        }

        public void Show(string text, bool skipAnimation = default)
        {
            _injectedText = text;
            Show(skipAnimation);
        }

        protected override void ShowAnimationBegin(bool skip = default)
        {
            base.ShowAnimationBegin(skip);
            canvasGroup.alpha = skip ? 1f : 0f;

            if (skip)
            {
                nestedPresenter.Show(_injectedText);
            }
            else
            {
                nestedPresenter.Hide(true);    
            }
        }

        protected override async UniTask ShowAnimationAsync()
        {
            float deltaAlpha = 1f / showDuration;
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += deltaAlpha * Time.deltaTime;
                await UniTask.NextFrame();
            }

            canvasGroup.alpha = 1f;
            
            await nestedPresenter.ShowAsObservable(_injectedText).ToUniTask();
        }

        protected override void HideAnimationBegin(bool skip = default)
        {
            canvasGroup.alpha = skip ? 0f : 1f;
            base.HideAnimationBegin(skip);
        }

        protected override async UniTask HideAnimationAsync()
        {
            float deltaAlpha = 1f / hideDuration;
            while (canvasGroup.alpha > 0.001f)
            {
                canvasGroup.alpha -= deltaAlpha * Time.deltaTime;
                await UniTask.NextFrame();
            }

            canvasGroup.alpha = 0f;
        }
    }
}
