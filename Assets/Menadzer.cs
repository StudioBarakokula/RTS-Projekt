using TMPro;
using UnityEngine;

public class Menadzer : MonoBehaviour
{


    public TMP_Text materijalText;
    public Transform roditeljJedinica;


    public int materijal = 10;



    public void DodajMaterijal()
    {
        materijal++;
        materijalText.text = materijal.ToString();
    }

    public void MakniMaterijal(int makniMat)
    {
        materijal -= makniMat;
        materijalText.text = materijal.ToString();
    }



}
