using System.Collections.Generic;
using Random = System.Random;

namespace Ovoshnig.Audio.Music.Queue;

public class MusicQueue<TKey>
{
    private readonly Queue<TKey> _clipKeysQueue = new();

    public void SetClipKeys(IEnumerable<TKey> clips)
    {
        Clear();

        foreach (var clip in clips)
            _clipKeysQueue.Enqueue(clip);
    }

    public bool TryGetNextClipKey(out TKey key) => _clipKeysQueue.TryDequeue(out key);

    public void ShuffleClipKeys()
    {
        TKey[] clips = [.. _clipKeysQueue];
        Random.Shared.Shuffle(clips);

        Clear();

        foreach (var clip in clips)
            _clipKeysQueue.Enqueue(clip);
    }

    public void Clear() => _clipKeysQueue.Clear();
}
