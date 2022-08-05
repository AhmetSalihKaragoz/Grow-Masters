using UnityEngine;
using DG.Tweening;

public class Obstacles : MonoBehaviour
{
    public int hammerDamage = 20; 
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 100), 1).SetEase(Ease.InQuad).SetLoops(-1,LoopType.Yoyo);
    }

}
