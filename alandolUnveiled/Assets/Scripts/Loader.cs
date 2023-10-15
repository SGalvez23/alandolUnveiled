using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Test,
        VictoryScreen
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(Scene.Test.ToString());
        SceneManager.LoadScene(scene.ToString());
    }
}
