using UnityEngine;
using UnityEngine.Events;

//�ýű���Ϊ�¼������ڴ�����ȥ��������ȥ���꣬�Ƿ���Ҫ�������뵭��Ч����
[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    //�����¼��������ı��������г��������꣬boolֵ�ж��Ƿ���Ҫ���뵭��
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;
    //���У������¼��ķ���,���������LoadRequestEvent,locationToLoadΪҪ���صĳ�����posToGoΪplayer��Ŀ�����꣬fedeScreenΪ�����ĵ��뵭��
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad,Vector3 posToGo,bool fedeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo, fedeScreen); 
    }
}
