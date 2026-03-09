using Game.TransitionService.Bootstrap;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    public void GoToByBuildIndex(int buildIndex)
    {
        ServiceLocator.Instance.GetService<ITransitionServiceBoostrap>().IniciarCarga(buildIndex);
    }
}
