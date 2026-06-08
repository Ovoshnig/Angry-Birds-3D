using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PigDestroyerView : ObjectDestroyerView
{
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    public override void Damage(float _)
    {
        float maxHealth = Settings.MaxHealth;
        float health = HealthModel.Health;
        float normalizedHealth = Mathf.InverseLerp(0, maxHealth, health);

        _animator.SetFloat(PigAnimationConstants.HealthParameterId, normalizedHealth);
    }
}
