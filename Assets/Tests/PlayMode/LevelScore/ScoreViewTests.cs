using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class ScoreViewTests
{
    private readonly WaitForSeconds _waitForOneSecond = new(1f);

    private ScoreView _scoreView;
    private TMP_Text _textComponent;

    [SetUp]
    public void Setup()
    {
        var scoreViewObject = new GameObject("TestScoreView");
        _textComponent = scoreViewObject.AddComponent<TextMeshProUGUI>();
        _scoreView = scoreViewObject.AddComponent<ScoreView>();

        scoreViewObject.SendMessage("Awake");
        scoreViewObject.SendMessage("Start");
    }

    [TearDown]
    public void Teardown()
    {
        if (_scoreView != null)
            Object.Destroy(_scoreView.gameObject);
    }

    [UnityTest]
    public IEnumerator SetScoreSmoothly_UpdatesTextAfterDuration()
    {
        _scoreView.SetScoreSmoothly(150);

        yield return _waitForOneSecond;

        Assert.AreEqual("Score: 00150", _textComponent.text);
    }

    [UnityTest]
    public IEnumerator SetScoreSmoothly_NegativeTarget_HandledByView()
    {
        _scoreView.SetScoreSmoothly(-10);

        yield return _waitForOneSecond;

        Assert.AreEqual("Score: -00010", _textComponent.text);
    }
}
