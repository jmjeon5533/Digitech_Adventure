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
    public bool eventOn = false;

    private void Awake()
    {
        if(instance == null)
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Scene(string SceneName)
    {
        StartCoroutine(fadeOut(SceneName));
    }

    #region 페이드
    IEnumerator fadeOut(string SceneName)
    {
        var wait = new WaitForSeconds(0.03f);
        for (float i = 0; i < 1; i += 0.02f)
        {
            Fade.color = new Color(0, 0, 0, i);
            yield return wait;
        }
        SceneManager.LoadScene(SceneName);
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

    public void Stage3(GameObject player)
    {
        #region 위치,각도 고정
        eventOn = true;
        player.transform.position = new Vector3(0, 5, 0);
        player.transform.localEulerAngles = new Vector3(0, -175, 0);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.GetChild(0).gameObject.transform.
            GetChild(0).gameObject.transform.localEulerAngles 
            = new Vector3(0,0,0);
        #endregion

    }
    public IEnumerator wait(float second)
    {
        yield return new WaitForSeconds(second);
    }
}
