using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //  public Controller2D target;
    public GameObject target;
    [Range(0, 20)] public float MenuDelta=10;
    [Range(20, 50)] public float NovoAfastamento = 25;
    private float Afasta = 25; 
    private float menudlt;
    private CapsuleCollider targetcollider;
    [SerializeField]
    private CapsuleCollider focuCollider;
    public float verticalOffset;
    public float AheadDstXPress = 6;
    public float AheadDstXSolt = 16;
    private float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;
    public bool Preso, AnteriorPreso;
    public CapsuleCollider FocusObj;
    FocusArea focusArea;

    public enum CameraEstado { estatica, MoveR, MovING }
    public CameraEstado estado;

    public float TempoPrender, TempoSoltar, TempoMove;
    [SerializeField]
    private Vector3 PontoInicial, PontoFinal, PontoAtual, PontoDelta, PontoSpeed;
  //  private Vector3 PontoA, PontoB, PontoC, PontoD, PontoT, PontoCorrecao, PontoTarget;
 //   [SerializeField]
  //  private Vector3 CenterFocoPlayer, CenterZero, DeltaCenter, AtualCenter, PosiPlayer, PosiZero;
    public Transform TheCollider;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

    private void Awake()
    {
        targetcollider = target.GetComponent<CapsuleCollider>();
        focuCollider = targetcollider;
        lookAheadDstX = AheadDstXSolt;
    }
    void Start()
    {
        focusArea = new FocusArea(targetcollider.bounds, focusAreaSize);
      
    }

    void Newfoco()
    {
        Preso = target.GetComponent<AndarPlayer>().CameraPressa;
        if (target.GetComponent<AndarPlayer>().NewFocus != null)
            FocusObj = target.GetComponent<AndarPlayer>().NewFocus.GetComponent<CapsuleCollider>();

        if(Preso!=AnteriorPreso)
        {
            estado = CameraEstado.MoveR;
            AnteriorPreso = Preso;
        }
        if (!Preso && estado == CameraEstado.estatica)
        {
            focuCollider = targetcollider;
            lookAheadDstX = AheadDstXSolt;
        } else
        {
            focuCollider = FocusObj;
            lookAheadDstX = AheadDstXPress;
        }
        switch (estado)
        {
            case CameraEstado.MoveR:
                if (Preso)
                {
                    NovosPonts(CalcutarPonto(target), Vector3.zero, TempoPrender);
         //           PontoInicial = CalcutarPonto(target);
           //         PontoFinal = Vector3.zero;
             //       TempoMove = TempoPrender;
                }
                else
                {
                    NovosPonts(CalcutarPonto(FocusObj.gameObject), CalcutarPonto(target), TempoSoltar);
//                    PontoInicial = CalcutarPonto(FocusObj.gameObject);
  //                  PontoFinal = CalcutarPonto(target);
    //                TempoMove = TempoSoltar;
                }
                PontoAtual = PontoInicial;
         //       PontoDelta = PontoFinal - PontoInicial;
           //     PontoSpeed = PontoDelta * Time.deltaTime / TempoMove;
                estado = CameraEstado.MovING;
                break;
            case CameraEstado.MovING:
                if(!Preso)
                    NovosPonts(Vector3.zero, CalcutarPonto(target), TempoMove);


                PontoAtual.x = Mathf.MoveTowards(PontoAtual.x, PontoFinal.x, Mathf.Abs(PontoSpeed.x));
                PontoAtual.y = Mathf.MoveTowards(PontoAtual.y, PontoFinal.y, Mathf.Abs(PontoSpeed.y));
                PontoAtual.z = Mathf.MoveTowards(PontoAtual.z, PontoFinal.z, Mathf.Abs(PontoSpeed.z));

                FocusObj.center = PontoAtual;

                if (PontoAtual == PontoFinal)//final==atual
                    estado = CameraEstado.estatica;
                break;
        }
        TheCollider.position = PontoAtual + FocusObj.gameObject.transform.position;

        //PontoInicial, PontoFinal, PontoAtual, PontoDelta, PontoSpeed;
        /*
                switch (estado)
                {
                    case CameraEstado.Solta:
                        focuCollider = targetcollider;
                        lookAheadDstX = AheadDstXSolt;
                        if (Preso)
                            estado = CameraEstado.PRENDER;
                        break;
                    case CameraEstado.Presa:
                        focuCollider = FocusObj;
                        lookAheadDstX = AheadDstXPress;
                        if (!Preso)
                            estado = CameraEstado.SOLTAR;
                        break;

                    case CameraEstado.PRENDER:
                        focuCollider = FocusObj;
                        lookAheadDstX = AheadDstXPress;
                        PontoA = CalcutarPonto(target);
                        PontoB = CalcutarPonto(FocusObj.gameObject);
                        PontoD = PontoA - PontoB;
                        PontoCorrecao = CalcutarPonto(FocusObj.gameObject);
                        PontoTarget = CalcutarPonto(FocusObj.gameObject);
                        estado = CameraEstado.ing; 
                        break;
                    case CameraEstado.SOLTAR:
                        focuCollider = FocusObj;
                        lookAheadDstX = AheadDstXPress;
                        PontoA = CalcutarPonto(FocusObj.gameObject);
                        PontoB = CalcutarPonto(target);
                        PontoD = PontoA - PontoB;
                        PontoCorrecao = CalcutarPonto(FocusObj.gameObject);
                        estado = CameraEstado.ing;
                        break;

                    case CameraEstado.ing:
                        float t;
                        GameObject gb;

                        if (Preso)
                        {
                            t = TempoPrender;
                            if (PontoC == PontoT)
                                estado = CameraEstado.Presa;
                        }
                        else
                        {
                            t = TempoSoltar;
                            PontoTarget = CalcutarPonto(target);
                            if (PontoC == PontoT)
                                estado = CameraEstado.Solta;
                        }

                        PontoC = PontoCentral(PontoA, PontoB,  t);
                        TheCollider.position = PontoC + FocusObj.gameObject.transform.position;
                        break;
                }
                //FocusObj.center = PontoC;*/

    }
    Vector3 CalcutarPonto(GameObject P)
    {
        Debug.Log(P.name + " " + P.transform.position + " " + P.GetComponent<CapsuleCollider>().center + ".");
        return P.transform.position + P.GetComponent<CapsuleCollider>().center - FocusObj.gameObject.transform.position;
    }
    
    void NovosPonts(Vector3 Pi, Vector3 Pf, float t)
    {
        PontoInicial = Pi;
        PontoFinal = Pf;
        TempoMove = t;
        
        PontoDelta = PontoFinal - PontoInicial;
        PontoSpeed = PontoDelta * Time.deltaTime / TempoMove;
    }

    void LateUpdate()
    {
        Newfoco();
        focusArea.Update(focuCollider.bounds);

        Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;
        #region "Calma"
        if (focusArea.velocity.x != 0)
        {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
            //    if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
            //    {
            //        lookAheadStopped = false;
            targetLookAheadX = lookAheadDirX * lookAheadDstX;
            //    }
            //    else
            //    {
            //        if (!lookAheadStopped)
            //        {
            //            lookAheadStopped = true;
            //            targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
            //        }
            //    }
        }
        #endregion
        //

        #region smooth
        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;
        #endregion

        Afasta = Mathf.MoveTowards(Afasta, NovoAfastamento, 0.5f);

        transform.position = (Vector3)focusPosition + Vector3.forward * -1 * Afasta;
        if (GameMaster.gm.PAUSADO)
            menudlt = Mathf.MoveTowards(menudlt, MenuDelta, 1);
        else
            menudlt = Mathf.MoveTowards(menudlt, 0, 1);

        Vector3 pp = transform.position;
        pp.x += menudlt;
        transform.position = pp;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .1f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    #region Caixa 
    struct FocusArea
    {

        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        #region criar struct
        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
        }
        #endregion
        public void Update(Bounds targetBounds)
        {
            #region Atualiza X
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }

            left += shiftX;
            right += shiftX;
            #endregion
            #region Atualiza Y
            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            #endregion
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
    #endregion
}