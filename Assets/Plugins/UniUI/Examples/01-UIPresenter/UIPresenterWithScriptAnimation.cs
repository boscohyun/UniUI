using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Boscohyun.UniUI.Examples.UIPresenter
{
    public class UIPresenterWithScriptAnimation : UniUI.UIPresenter
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        [Header("Animation")]
        
        [SerializeField, Range(0f, 2f)]
        private float showDuration = 1f;
        
        [SerializeField, Range(0f, 2f)]
        private float hideDuration = 1f;

        protected override void ShowAnimationBegin(bool skip = default)
        {
            base.ShowAnimationBegin(skip);
            canvasGroup.alpha = skip ? 1f : 0f;
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
