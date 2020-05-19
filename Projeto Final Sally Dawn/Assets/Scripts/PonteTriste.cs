using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonteTriste : MonoBehaviour
{
    public bool Stop = true;
    [Range(0,2)]
    public float Estados;
    public float Normal, Cor, Invisivel;

    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Normal = 1 - Estados;
        Normal = Mathf.Clamp(Normal, 0, 1);

        Cor = 1 - (Mathf.Abs(Estados - 1));
        Cor = Mathf.Clamp(Cor, 0, 1);

        Invisivel = Estados - 1;
        Invisivel = Mathf.Clamp(Invisivel, 0, 1);

        anim.SetBool("Cair", !Stop);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Stop = false;
        }
    }
}
