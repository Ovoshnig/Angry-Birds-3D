using System.Collections.Generic;
using UnityEngine.Audio;

public class SFXCounter
{
    private readonly Dictionary<AudioResource, int> _countByResource = new();

    public int GetCount(AudioResource resource) => _countByResource.GetValueOrDefault(resource, 0);

    public void Increment(AudioResource resource)
    {
        if (_countByResource.TryGetValue(resource, out int count))
            _countByResource[resource] = count + 1;
        else
            _countByResource[resource] = 1;
    }

    public void Decrement(AudioResource resource)
    {
        if (!_countByResource.TryGetValue(resource, out int count))
            return;

        if (count <= 1)
            _countByResource.Remove(resource);
        else
            _countByResource[resource] = count - 1;
    }
}
