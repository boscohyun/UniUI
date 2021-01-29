using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Boscohyun.UniUI.Examples.UIPresenter
{
    public class UIPresenterWithAnimatorAnimation : UniUI.UIPresenter
    {
        protected static readonly int AnimatorHashHide = Animator.StringToHash("Hide");
        
        protected static readonly int AnimatorHashShow = Animator.StringToHash("Show");

        [SerializeField]
        protected Animator animator;

        protected override void ShowAnimationBegin(bool skip = default)
        {
            base.ShowAnimationBegin(skip);
            animator.Play(AnimatorHashShow, 0, skip ? 1f : 0f);
        }

        protected override async UniTask ShowAnimationAsync()
        {
            await UniTask.WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AnimatorHashShow);
            await UniTask.WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
        }

        protected override void HideAnimationBegin(bool skip = default)
        {
            base.HideAnimationBegin(skip);
            animator.Play(AnimatorHashHide, 0, skip ? 1f : 0f);
        }

        protected override async UniTask HideAnimationAsync()
        {
            await UniTask.WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AnimatorHashHide);
            await UniTask.WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
        }
    }
}
