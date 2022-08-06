using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class ModalManager : MonoBehaviour
{
    [SerializeField]
    private List<Modal> _modals;


   
    public  void ShowModal(string modalName)
    {
      var modal = _modals.Find(modal => string.Equals(modal.gameObject.name, modalName));
        modal.gameObject.SetActive(true);
    }
}
