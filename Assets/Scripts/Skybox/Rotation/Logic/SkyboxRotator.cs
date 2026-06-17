using LitMotion;
using UnityEngine;
using VContainer.Unity;
using Random = System.Random;

public class SkyboxRotator : IStartable
{
    private readonly SkyboxSettings _skyboxSettings;
    private readonly Material _skybox;
    private readonly Random _random = new();

    public SkyboxRotator(SkyboxSettings skyboxSettings)
    {
        _skyboxSettings = skyboxSettings;

        _skybox = new Material(RenderSettings.skybox);
        RenderSettings.skybox = _skybox;
    }

    public void Start()
    {
        float startValue = SkyboxRotationConstants.MinValue;
        float endValue = SkyboxRotationConstants.MaxValue;
        int randomDirection = _random.Next(0, 2);

        if (randomDirection == 1)
            (startValue, endValue) = (endValue, startValue);

        float randomDegree = Mathf.Lerp(
            SkyboxRotationConstants.MinValue,
            SkyboxRotationConstants.MaxValue,
            (float)_random.NextDouble());

        LMotion.Create(startValue, endValue, _skyboxSettings.LoopDuration)
            .WithLoops(-1, LoopType.Incremental)
            .Bind(value =>
            {
                float rotation = (value + randomDegree) % SkyboxRotationConstants.MaxValue;
                _skybox.SetFloat(SkyboxRotationConstants.RotationProperty, rotation);
            });
    }
}
