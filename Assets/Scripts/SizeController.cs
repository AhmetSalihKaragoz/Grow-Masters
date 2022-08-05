using UnityEngine;
using DG.Tweening;
using TMPro;

public class SizeController : MonoBehaviour
{
    [Header("Size")]
    [SerializeField] Vector3 growSize;
    [HideInInspector] public Vector3 currentSize = new Vector3(1, 1, 1);
    
    public Vector3 maxGrowSize = new Vector3(6, 6, 6);
    public Vector3 minGrowSize = new Vector3(0.5f, 0.5f, 05f);

    [Header("Titles")]
    [SerializeField] TextMeshProUGUI title;

    private string[] titles = { "Tiny", "Average", "Big", "KingSize" };

    private void Start()
    {
        title.text = titles[0];
    }


    private void Update()
    {
        UpdateTitle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Gate")) 
        { 
            if (other.gameObject.GetComponent<GateController>().isGateActive)
            {
                

                if (other.gameObject.GetComponent<GateController>().operation == GateController.Operation.Add)
                {

                    AddSize(other.gameObject.GetComponent<GateController>().operationValue);

                }
                else if (other.gameObject.GetComponent<GateController>().operation == GateController.Operation.Multiply)
                {
                    SubstractSize(other.gameObject.GetComponent<GateController>().operationValue);
                }
                other.gameObject.GetComponent<GateController>().isGateActive = false;
            }
        }
        else if (other.CompareTag("Hammer"))
        {
            SubstractSize(other.gameObject.GetComponent<Obstacles>().hammerDamage);
        }
    }

    
    private void AddSize(int operationValue)
    {
        var newSize = transform.localScale;
        transform.localScale = new Vector3(Mathf.Clamp(newSize.x, 0, maxGrowSize.x), Mathf.Clamp(newSize.y, 0 , maxGrowSize.y), Mathf.Clamp(newSize.z, 0, maxGrowSize.z));
        transform.DOScale(transform.localScale + (growSize * operationValue), 0.5f);
    }

    private void SubstractSize(int operationValue)
    {
        var newSize = transform.localScale;
        transform.DOScale(transform.localScale - (operationValue * growSize), 0.5f);
    }

    public void UpdateTitle()
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
