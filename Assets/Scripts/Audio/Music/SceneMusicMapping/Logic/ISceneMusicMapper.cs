using Ovoshnig.Scene.Switching;

namespace Ovoshnig.Audio.Music.SceneMusicMapping
{
    public interface ISceneMusicMapper
    {
        public MusicCategory GetMusicCategory(SceneType sceneType);
    }
}
