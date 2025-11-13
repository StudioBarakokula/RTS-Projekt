using UnityEngine;

public class Strategijsko : MonoBehaviour
{


    public Transform kutija;

    public int duljina;
    public int visina;

    public int kretanjeMaks;
    int kretanjeTrenutacno;





    void Start()
    {

        for (int i = 0; i < duljina; i++) 
        {

            for(int j = 0; j < visina; j++)
            {
                Instantiate(kutija, new Vector3(i - duljina/2, 0, j - visina/2), transform.rotation);
            }
            
        }
        
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.gameObject.layer == 19)
                {
                    Kretanje(hit.collider.transform.position + Vector3.up * 2);
                }

            }

        }

    }




    void Kretanje(Vector3 novaPozicija)
    {
        Vector3 trenutacno = FindAnyObjectByType<OsnovnaJedinica>().transform.position;

        Debug.Log(Vector3.Distance(trenutacno, novaPozicija));

        if(Vector3.Distance(trenutacno, novaPozicija) < 1.5f)
        {
            FindAnyObjectByType<OsnovnaJedinica>().transform.position = novaPozicija;
            kretanjeTrenutacno--;
        }

    }


}
