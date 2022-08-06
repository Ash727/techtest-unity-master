using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modal : MonoBehaviour
{
    /// <summary>
    /// Enum types 
    /// </summary>
    public enum Types
    {
        InsufficantFundsModal,
        BetZero
    }

    public void Close()
    {
       gameObject.SetActive(false);
    }
}
