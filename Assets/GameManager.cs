using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]
    Image Fade;
    [SerializeField]
    string[] StoryOp;
    string Nick;
    [SerializeField]
    Text StoryT, Nickname;
    [SerializeField]
    GameObject StoryTab;
    Coroutine coroutine;
    int StoryNum = 0;
    /*
    enum StoryName
    {
        StoryOp,
        Story1
    }
    StoryName story = StoryName.StoryOp;
    */

    public bool eventOn = false, textOn = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {

        coroutine = StartCoroutine(Story(StoryOp)); //�̺�Ʈ ���� �ڷ�ƾ ����
    }

    // Update is called once per frame
    void Update()
    {
        if (!eventOn)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) //���콺 ���� Ŭ���̳� �����̽� �ٸ� ������ ��
        {
            if (textOn) //�ؽ�Ʈ�� �������̶��
            {
                StopCoroutine(coroutine); //�ؽ�Ʈ�� ������ �����ϰ�
                StoryT.text = StoryOp[StoryNum]; //�ؽ�Ʈ�� ���θ� �ѹ��� ���
                textOn = false; //�ؽ�Ʈ ��� �Ϸ� 
            }
            else //�ؽ�Ʈ�� ������ �Ϸ��ٸ�
            {
                StoryNum++; //���� �ؽ�Ʈ ������ �ٲٰ�
                if (StoryOp[StoryNum] == "") //���� ���� �ؽ�Ʈ�� ���ٸ�
                {
                    StoryTab.SetActive(false); //�ؽ�Ʈ â�� �����ϰ�
                    eventOn = false; //�̺�Ʈ �Ϸ�
                    return;
                }
                coroutine = StartCoroutine(Story(StoryOp)); //�ؽ�Ʈ�� �ٽ� ����
            }
        }
    }
    IEnumerator Story(string[] storytext) //���丮�� �ؽ�Ʈ
    {
        eventOn = true; //�̺�Ʈ ����
        var wait = new WaitForSeconds(0.2f);
        textOn = true; //�ؽ�Ʈ ��� ��
        for (int i = 0; i <= storytext[StoryNum].Length; i++) //�ؽ�Ʈ ���̸�ŭ ����
        {
            StoryT.text = storytext[StoryNum].Substring(0, i); //�ؽ�Ʈ ������ ������� ����
            yield return wait; //0.2�� ��ٸ�
        }
        textOn = false; //�ؽ�Ʈ ��� �Ϸ�
    }
    public void Scene(string SceneName, GameObject player)
    {
        StartCoroutine(fadeOut(SceneName, player));
    }

    #region ���̵�
    IEnumerator fadeOut(string SceneName, GameObject player)
    {
        var wait = new WaitForSeconds(0.03f);
        for (float i = 0; i < 1; i += 0.02f)
        {
            Fade.color = new Color(0, 0, 0, i);
            yield return wait;
        }
        SceneManager.LoadScene(SceneName);

        player.transform.position = new Vector3(7, 3.5f, -13.5f);
        player.transform.localEulerAngles = new Vector3(0, 0, 0);
        player.transform.GetChild(0).transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, 0);
        StartCoroutine(fadeIn());
    }
    IEnumerator fadeIn()
    {
        var wait = new WaitForSeconds(0.03f);
        for (float i = 1; i > 0; i -= 0.02f)
        {
            Fade.color = new Color(0, 0, 0, i);
            yield return wait;
        }
    }
    #endregion

    public IEnumerator wait(float second)
    {
        yield return new WaitForSeconds(second);
    }
}
