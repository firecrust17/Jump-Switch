using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togglePanel : MonoBehaviour
{

    public GameObject Panel;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;

    public void TogglePanel(bool tf){
        Panel.SetActive(tf);
        if(tf){
            showPage(1);
        }
    }

    public void showPage(int page) {

        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);

        if(page == 1){
            p1.SetActive(true);
        } else if (page == 2) {
            p2.SetActive(true);
        } else if (page == 3) {
            p3.SetActive(true);
        }
    }

}
