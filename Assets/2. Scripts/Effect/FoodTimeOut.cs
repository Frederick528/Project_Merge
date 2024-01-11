using UnityEngine;

public class FoodTimeOut : MonoBehaviour
{
    public void AnimEvt()
    {
        this.transform.parent.gameObject.SetActive(false);
        if (this.transform.parent.parent.Equals(FoodTimeOutManager.Instance.transform))
        {
            return;
        }

        this.transform.parent.parent = FoodTimeOutManager.Instance.transform;
        Debug.Log(FoodTimeOutManager.DestroyQueue.Count);
        var v = FoodTimeOutManager.DestroyQueue.Dequeue();
        if(!CardManager.DestroyCard(v))
            Debug.Log("failed to destroy");
        FoodTimeOutManager.EffectQueue.Enqueue(this.transform.parent.gameObject);
    }
}