using UnityEngine;

public class Zgrada : MonoBehaviour
{

    [SerializeField] Transform jedinicaZaStvaranje;
    [SerializeField] Transform tockaStvaranja;
    [SerializeField] Transform roditelj;
    [SerializeField] float vrijemeZaStvaranje = 4;
    float zadnjeStvaranje;

    public int cijenaMaterijala = 100;




    void Start()
    {

        roditelj = FindFirstObjectByType<Menadzer>().roditeljJedinica;


    }
    void Update()
    {
        
        if(Time.time > zadnjeStvaranje + vrijemeZaStvaranje)
        {
            // instantiate stvara nesto na novo, sa ovon postavon daje poziciju i rotaciju
            Instantiate(jedinicaZaStvaranje, tockaStvaranja.position, transform.rotation, roditelj);
            zadnjeStvaranje = Time.time;
        }


    }



}
