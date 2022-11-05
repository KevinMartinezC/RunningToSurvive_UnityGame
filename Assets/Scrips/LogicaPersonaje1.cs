using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPersonaje1 : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;//indicar que tan rapido se movera el personaje
    public float velocidadRotacion = 200.0f;//velocidad para rotar el personaje
    private Animator anim; //para obtener el componente de animator del personaje
    public float x, y;//para saber si el personaje se esta moviendo o esta quieto
    // Start is called before the first frame update
    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool puedoSaltar;
    [SerializeField] AudioSource jumpSound;

    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();//componente animator
       
    }

    void FixedUpdate()
    {
        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);
    }

    // Update is called once per frame
    void Update()
    {
       
       x = Input.GetAxis("Horizontal");
       y = Input.GetAxis("Vertical");

       transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0 ); //para la rotacion
       transform.Translate(0, 0 , y * Time.deltaTime * velocidadMovimiento );

       anim.SetFloat("VelX", x);
       anim.SetFloat("VelY", y);

        Jump();
       
    }
    public void EstoyCayendo()
    {

        anim.SetBool("tocoSuelo",false);
        anim.SetBool("salte",false);
    }
    public void Jump(){
        if(puedoSaltar){
            if(Input.GetKeyDown(KeyCode.Space)){
            anim.SetBool("salte",true);
             jumpSound.Play();
            rb.AddForce(new Vector3(0,fuerzaDeSalto,0),ForceMode.Impulse);
        }
        anim.SetBool("tocoSuelo",true);
    }else{
        EstoyCayendo();
    }
    }
      private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy Head"))
        {
          Destroy(collision.transform.parent.gameObject);
          Jump();
          
        }
    }
   
}
