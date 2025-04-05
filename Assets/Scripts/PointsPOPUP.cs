using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsPOPUP : MonoBehaviour
{
    public static PointsPOPUP current;
    public GameObject popUp;

    void Start()
    {

    }

    void Update()
    {

    }

    public void PointsPopUp(Vector3 position, string text)
    {
        var popup = Instantiate(popUp, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;

        Destroy(popup, 1f);
    }
}
