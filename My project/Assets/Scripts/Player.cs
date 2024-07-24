using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadeMovimento = 5f;
    public float forcaPulo = 5f;
    private bool estaNoChao;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator; // Referência ao Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Inicializa o Animator
    }

    void Update()
    {
        // Movimentação horizontal
        float entradaMovimento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(entradaMovimento * velocidadeMovimento, rb.velocity.y);

        // Alternar direção do sprite baseado na direção da movimentação
        if (entradaMovimento > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("CorrerDireita", true);
            animator.SetBool("CorrerEsquerda", false);
        }
        else if (entradaMovimento < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("CorrerDireita", false);
            animator.SetBool("CorrerEsquerda", true);
        }
        else
        {
            animator.SetBool("CorrerDireita", false);
            animator.SetBool("CorrerEsquerda", false);
        }

        // Pular
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
        }
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
        }
    }

    private void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            estaNoChao = false;
        }
    }
}
