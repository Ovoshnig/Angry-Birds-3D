using LitMotion;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Renderer))]
public class BlockDestroyerView : ObjectDestroyerView
{
    [SerializeField] private BlockDestructionSettings _destructionSettings;

    private readonly Vector4[] _hitPoints = new Vector4[4];
    private readonly MotionHandle[] _motionHandles = new MotionHandle[4];

    private Renderer _renderer;
    private MaterialPropertyBlock _propertyBlock;
    private int _nextHitIndex = 0;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    public override void VisualizeDamage(Vector3 worldPoint, float health, float maxHealth)
    {
        if (maxHealth <= 0f)
            return;

        Vector3 localPoint = transform.InverseTransformPoint(worldPoint);

        float damageRatio = 1f - Mathf.Clamp01(health / maxHealth);
        float maxExpectedRadius = CalculateMaxRadius();
        float targetRadius = maxExpectedRadius * damageRatio;

        int index = _nextHitIndex;
        _nextHitIndex = (_nextHitIndex + 1) % _hitPoints.Length;

        _motionHandles[index].TryCancel();

        _hitPoints[index] = new Vector4(localPoint.x, localPoint.y, localPoint.z, 0f);

        _motionHandles[index] = LMotion.Create(0f, targetRadius, _destructionSettings.Duration)
            .WithEase(_destructionSettings.Ease)
            .Bind((Target: this, Index: index), (radius, state) =>
            {
                state.Target._hitPoints[state.Index].w = radius;
                state.Target.ApplyPropertiesToShader();
            })
            .AddTo(this);
    }

    private void ApplyPropertiesToShader()
    {
        if (_renderer == null)
            return;

        _renderer.GetPropertyBlock(_propertyBlock);

        for (int i = 0; i < _hitPoints.Length; i++)
            _propertyBlock.SetVector(BlockDestructionConstants.HitProperties[i], _hitPoints[i]);

        _renderer.SetPropertyBlock(_propertyBlock);
    }

    private float CalculateMaxRadius()
    {
        Bounds bounds = _renderer.bounds;
        return Vector3.Distance(bounds.min, bounds.max) * 0.5f;
    }
}
