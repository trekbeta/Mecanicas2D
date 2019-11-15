using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    private Rigidbody2D         playerRb;
    private Animator            playerAnimator;

    public  Transform           groundCheck;
    public  Transform           hitBox;

    public GameObject           hitBoxPrefab;

    private bool                isGrounded; // VARIAVEL ESTAR CAINDO
    private bool                isWalk;     // VARIAVEL ESTAR ANDANDO
    public  bool                isLookLeft; // VERIAVEL LADO QUE PERSONAGEM ESTA OLHANDO
    private bool                isAtack;    // VARIAVEL ATACAR

    
    public float                forcaPulo;
    public float                velocidadeMovimento;                


    // Start is called before the first frame update
    void Start()
    {
        //GetComponent - para ter acesso aos compotenetes
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){

        float velocidadeY = playerRb.velocity.y;
        
        // PERSONAGEM ANDAR
        float horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal != 0)
        {
            isWalk = true;
            // CHAMANDO FUNÇÃO PARA ONDE PERSONAGEM ESTA OLHANDO
            if(horizontal > 0 && isLookLeft == true)
            {
                flip();
            }
            else if(horizontal < 0 && isLookLeft == false)
            {
                flip();
            }
            // CHAMANDO FUNÇÃO PARA ONDE PERSONAGEM ESTA OLHANDO FIM

        }
        else
        {
            isWalk = false;
        }
   
        
        
        // PERSONAGEM ANDAR FIM

        // PERSONAGEM ATACAR
        if(Input.GetButtonDown("Fire1")&& isGrounded == true && isAtack == false)
        {
            isAtack = true;
            playerRb.velocity = new Vector2(0,0); // COMANDO PARA PERSONAGEM PARA QUANDO FOR ATACAR
            playerAnimator.SetTrigger("atack");
        }
        // PERSONAGEM ATACAR FIM


        // PERSONAGEM PULAR
        if(Input.GetButtonDown("Jump") && isGrounded == true && isAtack == false)
        {
            playerRb.AddForce(new Vector2(0, forcaPulo));
        }
        // PERSONAGEM PULAR FIM
        
        
        if (isAtack == false)
            {
        playerRb.velocity = new Vector2(horizontal * velocidadeMovimento, velocidadeY);
            }

        //PASANDO COMANDO PARA ANIMAÇÃO
        playerAnimator.SetBool("isGrounded", isGrounded); 
        playerAnimator.SetFloat("speedY", playerRb.velocity.y);
        playerAnimator.SetBool("walk", isWalk);
        //PASANDO COMANDO PARA ANIMAÇÃO FIM


    }

void FixedUpdate() 
{
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
}

    // FUNÇÃO PARA ONDE PERSONAGEM ESTA OLHANDO
    void flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    // PARA ONDE PERSONAGEM ESTA OLHANDO FIM

    public void OnAtackEnded()
    {
        isAtack = false;
    }

    public void OnHitBox()
    {
        GameObject hit = Instantiate(hitBoxPrefab, hitBox.position, hitBox.localRotation);
        //Debug.LogError("pause");
        Destroy(hit.gameObject, 0.03f);    
    }

    //ANIMÇÃO HIT QUANDO ENCOSTA NO INIMIGO
    void OnTriggerEnter2D(Collider2D col) 
    {
        switch(col.gameObject.tag)
        {
        case "inimigo":
            playerAnimator.SetTrigger("hit");
            print("Tomou 10 de Dano");
            break;    
        }
    }

}
