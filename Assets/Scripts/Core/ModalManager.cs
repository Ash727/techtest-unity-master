using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class ModalManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _modals;

    

   
    public  void ShowModal(Modal.Types type)
    { 
        switch (type)
        {
            case Modal.Types.InsufficantFundsModal:
                _modals[0].SetActive(true);
                break;
            case Modal.Types.BetZero:
                _modals[1].SetActive(true);
                break;
            default:
                break;
        }
    }
}
