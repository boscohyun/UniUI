using System.Collections;
using UnityEngine;

namespace Boscohyun.UniUI.Examples.UIPresenter
{
    public class UIPresenterWithAnimation : UniUI.UIPresenter
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        [Header("Animation")]
        
        [SerializeField, Range(0f, 2f)]
        private float showDuration = 1f;
        
        [SerializeField, Range(0f, 2f)]
        private float hideDuration = 1f;
        
        protected override IEnumerator ShowAnimation(bool skip = default)
        {
            animationState = AnimationState.InShowAnimation;
            
            if (skip)
            {
                canvasGroup.alpha = 1f;
                gameObject.SetActive(true);
                yield break;
            }

            canvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            
            float deltaAlpha = 1f / showDuration;
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += deltaAlpha * Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 1f;
        }
        
        protected override IEnumerator HideAnimation(bool skip = default)
        {
            animationState = AnimationState.InHideAnimation;
            
            if (skip)
            {
                gameObject.SetActive(false);
                yield break;
            }
            
            
            float deltaAlpha = 1f / hideDuration;
            while (canvasGroup.alpha > 0.001f)
            {
                canvasGroup.alpha -= deltaAlpha * Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }
    }
}
