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

        coroutine = StartCoroutine(Story(StoryOp)); //이벤트 시작 코루틴 실행
    }

    // Update is called once per frame
    void Update()
    {
        if (!eventOn)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) //마우스 왼쪽 클릭이나 스페이스 바를 눌렀을 때
        {
            if (textOn) //텍스트가 실행중이라면
            {
                StopCoroutine(coroutine); //텍스트의 실행을 중지하고
                StoryT.text = StoryOp[StoryNum]; //텍스트의 전부를 한번에 출력
                textOn = false; //텍스트 출력 완료 
            }
            else //텍스트가 실행이 완료됬다면
            {
                StoryNum++; //다음 텍스트 순서로 바꾸고
                if (StoryOp[StoryNum] == "") //만약 다음 텍스트가 없다면
                {
                    StoryTab.SetActive(false); //텍스트 창을 종료하고
                    eventOn = false; //이벤트 완료
                    return;
                }
                coroutine = StartCoroutine(Story(StoryOp)); //텍스트를 다시 실행
            }
        }
    }
    IEnumerator Story(string[] storytext) //스토리의 텍스트
    {
        eventOn = true; //이벤트 시작
        var wait = new WaitForSeconds(0.2f);
        textOn = true; //텍스트 출력 중
        for (int i = 0; i <= storytext[StoryNum].Length; i++) //텍스트 길이만큼 실행
        {
            StoryT.text = storytext[StoryNum].Substring(0, i); //텍스트 길이의 순서대로 실행
            yield return wait; //0.2초 기다림
        }
        textOn = false; //텍스트 출력 완료
    }
    public void Scene(string SceneName, GameObject player)
    {
        StartCoroutine(fadeOut(SceneName, player));
    }

    #region 페이드
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
