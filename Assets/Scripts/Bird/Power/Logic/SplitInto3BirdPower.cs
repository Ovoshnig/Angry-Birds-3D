using UnityEngine;

public class SplitInto3BirdPower : IBirdPower
{
    public BirdPowerType Type => BirdPowerType.SplitInto3;

    public void Activate(BirdEntityView birdEntityView, BirdPowerSettings powerSettings)
    {
        Vector3 position = birdEntityView.transform.position;
        Vector3 rotation = birdEntityView.transform.localEulerAngles;
        float velocityMagnitude = birdEntityView.FlyerView.Rigidbody.linearVelocity.magnitude;

        BirdEntityView firstClone = CreateClone(birdEntityView, position, rotation, velocityMagnitude,
            -powerSettings.SplitAngleDiff, powerSettings.SplitPositionDiff);

        BirdEntityView secondClone = CreateClone(birdEntityView, position, rotation, velocityMagnitude,
            powerSettings.SplitAngleDiff, -powerSettings.SplitPositionDiff);

        BirdDestroyerView destroyerView = birdEntityView.DestroyerView;
        destroyerView.AddClone(firstClone.DestroyerView);
        destroyerView.AddClone(secondClone.DestroyerView);

        BirdFlyerView flyerView = birdEntityView.FlyerView;
        flyerView.AddClone(firstClone.FlyerView);
        flyerView.AddClone(secondClone.FlyerView);
    }

    private BirdEntityView CreateClone(BirdEntityView original,
        Vector3 basePosition,
        Vector3 baseRotation,
        float velocityMagnitude,
        float angleOffset,
        float positionOffset)
    {
        Vector3 cloneRotation = baseRotation;
        cloneRotation.x += angleOffset;

        BirdEntityView clone = Object.Instantiate(original, basePosition, Quaternion.Euler(cloneRotation));

        clone.transform.position = basePosition + (positionOffset * clone.transform.up);
        clone.transform.localScale = Vector3.one;
        clone.FlyerView.Rigidbody.linearVelocity = velocityMagnitude * clone.transform.forward.normalized;

        return clone;
    }
}
