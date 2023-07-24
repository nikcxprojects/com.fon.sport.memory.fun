using UnityEngine;

public static class BootstrapperL
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        Object.Instantiate(Resources.Load("Viewer"));
    }
}
