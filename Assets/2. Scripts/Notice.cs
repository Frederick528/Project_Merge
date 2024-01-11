using UnityEngine;

public class Notice : MonoBehaviour
{
    private static bool _isProcessing = false;
    private static bool _flag = true;

    private static Animator _anim;

    private void OnEnable()
    {
        _anim ??= GetComponent<Animator>();
        _anim.SetTrigger("FadeIn");
    }

    public static void Dispose()
    {
        var v = _anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (v <= 0 && v >= 1 )
            _anim.ResetTrigger("FadeIn");
        _anim.SetTrigger("FadeOut");
    }

    public void EndFadeOut()
    {
        Debug.Log(true);
        BearManager.Instance.bearApear.gameObject.SetActive(false);
    }
}
