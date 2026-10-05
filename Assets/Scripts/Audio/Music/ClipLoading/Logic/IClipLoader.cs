using Cysharp.Threading.Tasks;
using Ovoshnig.Audio.Music.SceneMusicMapping;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Ovoshnig.Audio.Music.ClipLoading
{
    public interface IClipLoader
    {
        UniTask<Dictionary<MusicCategory, IEnumerable<object>>> LoadClipKeysAsync(CancellationToken token);

        UniTask<AudioClip> LoadClipAsync(object address, CancellationToken cancellationToken);

        void UnloadClip(AudioClip clip);
    }
}
