using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Instant : SceneTransition
{
    public CanvasGroup canvasGroup;

    public override IEnumerator AnimateTransitionIn()
    {
        var tweener = canvasGroup.DOFade(1f, 0);
        yield return tweener.WaitForCompletion();
    }

    public override IEnumerator AnimateTransitionOut()
    {
        var tweener = canvasGroup.DOFade(0f, 0);
        yield return tweener.WaitForCompletion();
    }
}
