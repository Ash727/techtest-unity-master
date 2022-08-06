using System.Collections.Generic;
using UnityEngine;
public class Unlockables : MonoBehaviour 
{
    [SerializeField]
    private List<GameObject> LockedObjs; 
    

    public void UnlockFeature (int userLevel)
    {
        switch (userLevel)
        {
            case 2:
                LockedObjs[0].SetActive(true);
                break;
            default:
                break;
        }
    }

}