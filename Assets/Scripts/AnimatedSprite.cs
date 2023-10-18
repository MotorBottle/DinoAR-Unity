using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    //public Sprite[] sprites;
    public Animator spriteAnimator;

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
            spriteAnimator.Play("run");
            spriteAnimator.speed = GameManager.Instance.gameSpeed / GameManager.Instance.initialGameSpeed;
        }
        else if (GameManager.Instance.CurrentState == GameManager.GameState.GameOver)
        {
            spriteAnimator.Play("idle");
            spriteAnimator.speed = 1f;
        }
        else
        {
            spriteAnimator.Play("idle");
            spriteAnimator.speed = 1.5f;
        }
    }

    private void OnDisable()
    {
        //CancelInvoke();
        spriteAnimator.Play("idle");
        spriteAnimator.speed = 1.5f;
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
