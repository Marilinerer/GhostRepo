using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsPopGenerator : MonoBehaviour
{
    public static PointsPopGenerator current;
    public GameObject popUp;
    public GameObject popUpClose;

    void Start()
    {

    }

    void Update()
    {

    }

    public void PointsPopUp(Vector3 position, string text)
    {
        //Debug.Log("PointsPopUp Called!");

        var popup = Instantiate(popUp, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;

        //Destroy(popup, 1f);
    }

    public void PointsPopUpClose(Vector3 position, string text)
    {
        //Debug.Log("ClosePointsPopUp Called!");

        var popup = Instantiate(popUpClose, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;

        //Destroy(popup, 1f);
    }
}
