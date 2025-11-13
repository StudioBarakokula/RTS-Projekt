using UnityEngine;

public class Zgrada : MonoBehaviour
{

    // Transform sa velikin T je vrsta varijable, ki float int, malo transform je za objekt na kojem je skripta

    [SerializeField] Transform jedinicaZaStvaranje;
    [SerializeField] Transform tockaStvaranja;
    [SerializeField] Transform roditelj;
    [SerializeField] float vrijemeZaStvaranje = 4;
    float zadnjeStvaranje;



    void Start()
    {
        
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
