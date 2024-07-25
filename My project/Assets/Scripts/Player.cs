using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadeMovimento = 5f;
    public float forcaPulo = 5f;
    private bool estaNoChao;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimentação horizontal
        float entradaMovimento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(entradaMovimento * velocidadeMovimento, rb.velocity.y);

        // Atualizar o parâmetro de velocidade no Animator
        animator.SetFloat("VelocidadeHorizontal", entradaMovimento);

        // Verificar direção para espelhar o sprite
        if (entradaMovimento != 0)
        {
            // Se entradaMovimento for maior que 0, virar para direita, se menor, para esquerda
            transform.localScale = new Vector3(Mathf.Sign(entradaMovimento), 1, 1);
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
