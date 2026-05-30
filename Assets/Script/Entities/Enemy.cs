using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable<EnemyProfile>
{
    public EnemyProfile profile { get; private set; }

    private bool initialized = false;

    public Transform playerTarget = null;

    private SpriteRenderer sr;
    private EnemyStats stats;
    public void SetTarget(Transform playerTarget) => this.playerTarget = playerTarget;

    public System.Action<Enemy> OnDeath;

    //OnHit flash
    private Material materialInstance;
    private Tween flashTween;
    private static readonly int FlashAmount = Shader.PropertyToID("_FlashAmount");

    public void OnSpawn(EnemyProfile profile) 
    {
        if (!initialized)
        {
            this.profile = profile;

            sr = gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = profile.worldSprite;
                materialInstance = sr.material;
            }

            Animator anim = gameObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.runtimeAnimatorController = profile.animatorController;
            }

            stats = gameObject.GetComponent<EnemyStats>();
            if (stats != null)
            {
                stats.Init(profile);
            }

            EnemyInputHandler inputHandler = gameObject.GetComponent<EnemyInputHandler>();
            if(inputHandler != null)
            {
                inputHandler.Init(profile.enemyType, playerTarget);
            }    

            initialized = true;
        }
    }

    public void OnDespawn()
    {

    }

    void Update()
    {
    }    

    public void Die()
    {
        PlayerLevelManager.Instance.IncreaseEXP(1f);
        OnDeath?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Projectile
        if (collision.TryGetComponent(out ProjectileStats projectileStats))
        {
            //Trừ HP
            OnHitFlash();
            stats.OnHPChanged(-projectileStats.damage);
        }
    }

    public void OnHitFlash()
    {
        flashTween?.Kill();

        // Set trắng hoàn toàn
        materialInstance.SetFloat(FlashAmount, 1f);

        // Fade trắng -> bình thường
        flashTween = DOTween.To(
            () => materialInstance.GetFloat(FlashAmount),
            x => materialInstance.SetFloat(FlashAmount, x),
            0f,
            0.2f
        );
    }
}
