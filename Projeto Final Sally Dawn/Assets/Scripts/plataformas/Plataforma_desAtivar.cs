using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_desAtivar : MonoBehaviour
{
    public bool Visivel;
    private bool Mudar;
    public Collider cl;
    [SerializeField]
    private float TargetDisolve, ShDisolve;
    public Shader sh;
    // Start is called before the first frame update
    void Start()
    {
        //  gameObject.SetActive(Visivel);
        cl = GetComponent<Collider>();
        sh = GetComponent<Shader>();
        Mudar = Visivel;
    }

    // Update is called once per frame
    void Update()
    {
        TargetDisolve = (Visivel) ? 1 : -1;
        ShDisolve = Mathf.MoveTowards(ShDisolve, TargetDisolve, Time.deltaTime);

        if (Mudar!=Visivel)
        {
            Mudar = Visivel;
            Invoke("MudarCollider", 1);
        }
    }

    void MudarCollider()
    {
        cl.enabled = Visivel;
    }
}
