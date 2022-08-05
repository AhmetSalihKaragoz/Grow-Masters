using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GateController : MonoBehaviour
{
    public bool isGateActive = true;
    [SerializeField] GateController neighbourGate;
    public Operation operation;

    [Header("Text")]
    [SerializeField] TextMeshPro valueText;
    [SerializeField] string symbol;
    public int operationValue;
   
    
    private void Start()
    {
        valueText.text = symbol + operationValue;
    }

     void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            neighbourGate.isGateActive = false;
        }
    }

    public enum Operation
    {
        Add,
        Multiply
    }

}
