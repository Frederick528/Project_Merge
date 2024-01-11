using UnityEngine;

public class Notice : MonoBehaviour
{
    private static bool _isProcessing = false;
    private static bool _flag = true;

    private static Animator _anim;

    public static bool isImportant = false;

    public GameObject Siren;
    
    
    private void OnEnable()
    {
        _anim ??= GetComponent<Animator>();
        _anim.SetTrigger("FadeIn");
        
        Siren.SetActive(isImportant);
        
    }

    public static void Dispose()
    {
        _anim.ResetTrigger("FadeIn");
        _anim.SetTrigger("FadeOut");
    }

    public void EndFadeOut()
    {
        BearManager.Instance.bearApear.gameObject.SetActive(false);
        BearManager._turnSkip.interactable = true;
    }
}
