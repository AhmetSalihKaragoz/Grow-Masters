using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    public Transform fightingPoint;
    

    private string[] titles = { "Tiny", "Average", "Big", "KingSize" };


    private void Start()
    {
        var newSize = transform.localScale;
        if (newSize.x < 2)
        {
            title.text = titles[0];
        }
        else if (newSize.x >= 2 && newSize.x < 4)
        {
            title.text = titles[1];
        }
        else if (newSize.x >= 4 && newSize.x < 6)
        {
            title.text = titles[2];
        }
        else if (newSize.x >= 6)
        {
            title.text = titles[3];
        }
    }
}
