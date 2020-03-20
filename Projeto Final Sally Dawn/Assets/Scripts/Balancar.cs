using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancar : MonoBehaviour
{
    private Transform Target;
    public float Angulo, CurrentAng;
    [Range(0, 5)]
    public float Coeficiente;
    [Range(1, 50)]
    public float MaxSpeed, currentSpeed, MinSpeed;
    private Vector3 PontoDelta;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).gameObject.name == "AngulloTarget")
                Target = transform.GetChild(i);

        if (Target == null)
            Debug.LogError("Algunlo target not found");

        PontoDelta = Target.position - transform.position;
        Angulo = Mathf.Atan(PontoDelta.y / PontoDelta.x) * Mathf.Rad2Deg;
        Angulo -= 90;
    }

    // Update is called once per frame
    void Update()
    {
        Target.position = transform.position + PontoDelta;

        if (CurrentAng == Angulo)
            Angulo = -Angulo;

        currentSpeed = ClacularSpeed();
        CurrentAng = Mathf.MoveTowards(CurrentAng, Angulo, currentSpeed * Time.deltaTime);

        transform.eulerAngles= new Vector3(0, 0, CurrentAng);
    }

    float ClacularSpeed()
    {
        float DeltaSpeed = MinSpeed - MaxSpeed;
        int k = (int)Coeficiente;
        float speed =Mathf.Abs(Mathf.Pow(CurrentAng,k)) * (DeltaSpeed / Mathf.Abs(Mathf.Pow(Angulo,k))) + MaxSpeed;


        return Mathf.Clamp(speed, MinSpeed, MaxSpeed);
    }
}
