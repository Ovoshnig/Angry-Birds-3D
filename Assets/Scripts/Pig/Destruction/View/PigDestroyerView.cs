using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PigDestroyerView : ObjectDestroyerView
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public override void VisualizeDamage(Vector3 _, float health, float maxHealth)
    {
        float normalizedHealth = Mathf.InverseLerp(0, maxHealth, health);
        _animator.SetFloat(PigAnimationConstants.HealthParameterId, normalizedHealth);
    }
}
