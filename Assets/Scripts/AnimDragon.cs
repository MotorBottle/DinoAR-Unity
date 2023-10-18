using UnityEngine;

public class AnimaDragon : MonoBehaviour
{
    //public Sprite[] sprites;
    public Animator dragonAnimator;

    //private SpriteRenderer spriteRenderer;
    //private int frame;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteAnimator.Play("idle");
        //spriteAnimator.speed = 1.5f;
    }

    private void OnEnable()
    {
        //Invoke(nameof(Animate), 0f);
    }

    public void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            dragonAnimator.Play("flying");
            dragonAnimator.speed = GameManager.Instance.gameSpeed / GameManager.Instance.initialGameSpeed;
        }
        else if (GameManager.Instance.CurrentState == GameManager.GameState.GameOver)
        {
            dragonAnimator.Play("idle");
            dragonAnimator.speed = 0f;
        }
        else
        {
            dragonAnimator.Play("idle");
            dragonAnimator.speed = 0f;
        }
    }

    private void OnDisable()
    {
        //CancelInvoke();
        //spriteAnimator.Play("idle");
        //spriteAnimator.speed = 1.5f;
    }

    //private void Animate()
    //{
    //    frame++;

    //    if (frame >= sprites.Length)
    //    { 
    //        frame = 0;
    //    }

    //    if (frame >= 0 && frame < sprites.Length)
    //    {
    //        spriteRenderer.sprite = sprites[frame];
    //    }

    //    Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    //}

}

