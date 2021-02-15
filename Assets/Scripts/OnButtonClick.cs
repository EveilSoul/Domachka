using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonClick : MonoBehaviour
{
    public void OnClick()
    {
        Manager.instance.CheckCorrect(this.gameObject);
    }
}
