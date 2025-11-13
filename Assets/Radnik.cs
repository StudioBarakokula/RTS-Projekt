using UnityEngine;

public class Radnik : OsnovnaJedinica
{




    void Start()
    {
        
    }

    void Update()
    {
        
    }



    override protected void Trazenje()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, domet);
        for (int i = 0; i < colls.Length; i++)
        {
            Debug.Log("SQEDA");
            if (colls[i].gameObject.layer == 8 ||colls[i].gameObject.layer == 9)
            {
                Debug.Log("asddsa " + colls[i].transform.name);
                meta = colls[i].transform; i = colls.Length;
            }
        }
    }



}
