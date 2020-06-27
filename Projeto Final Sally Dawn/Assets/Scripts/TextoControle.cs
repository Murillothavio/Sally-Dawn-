using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoControle : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private Collider colli;
    [Range(0,60)]
    public float TimeLife;
    private float life;
    public bool Aparecer, Sumir;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        colli = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Tuto")
            if (colli != null)
                colli.enabled = (!GameMaster.gm.PAUSADO);

        if (Aparecer && (!GameMaster.gm.PAUSADO))
            TimeLife -= Time.deltaTime;
        if (TimeLife <= 0)
        {
            Sumir = true;
            Aparecer = false;
            Destroy(gameObject, 2f);
        }       

        anim.SetBool("Aparecer", Aparecer);
        anim.SetBool("Sumir", Sumir);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Aparecer = true;
    }
}
