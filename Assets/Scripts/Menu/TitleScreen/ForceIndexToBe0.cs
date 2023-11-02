using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceIndexToBe0 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PushTo0();
    }

    public void PushTo0()
    {
        transform.SetSiblingIndex(0);
    }

    public void PushTo(int index)
    {
        transform.SetSiblingIndex(index);
    }

}
