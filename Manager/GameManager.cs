using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    
    protected GameManager() { }
    
    public Camera mainCamera;

    public GameObject Ball;
    public GameObject BallEffectDestruction;
    public Vector2 RandomDirectionY;
    public Vector2 RandomDirectionX;

    public Vector2 powerRange;
    public float powerMinMultiplicator = 1000;

    private int currentBallThrow = 0;
    private int currentEffectPlay = 0;
    public int SizeForballPoll = 10;
    private List<GameObject> listBall = new List<GameObject>();
    private List<GameObject> ListEffect = new List<GameObject>();

    public List<Transform> listTraget;
    
    private SoundManager soundM;
    private HUDManager HUDm;

    private int score;
    private int ballThrow;

    #region Timer
    [Header("In Seconds")]
    public float timeToPlay = 60;
    private float currentTime = 0;

    [Header("In Seconds")]
    public float timeToHelp = 5;
    private float currentTimeHelp =0;
    #endregion

    [HideInInspector]
    public bool IsPause = false;
    [HideInInspector]
    public bool IsEnd = false;
    
    public void Registertarget(Transform trans)
    {
        listTraget.Add(trans);
    }

    public void RemoveTarget(Transform trans)
    {
        listTraget.Remove(trans);
    }

	// Use this for initialization
	void Start ()
    {
        score = 0;
        ballThrow = 0;

        soundM = SoundManager.Instance;
        HUDm = HUDManager.instance;
        HUDm.EditScore(score);
        HUDm.EditBallThrow(ballThrow);

        currentTime = timeToPlay;
        currentTimeHelp = timeToHelp;

        for (int i = 0; i < SizeForballPoll; i++)
        {
            listBall.Add(Instantiate(Ball, transform.position, Quaternion.identity));
            listBall[i].GetComponent<BehaviourBall>().IDinList = i;
            listBall[i].SetActive(false);

            ListEffect.Add(Instantiate(BallEffectDestruction, transform.position, Quaternion.identity));
            //ListEffect[i].SetActive(false);
        }

        currentBallThrow = 0;
        currentEffectPlay = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!IsPause && !IsEnd)
        {
            currentTime -= Time.deltaTime;
            HUDm.SetChrono(currentTime);

            if (currentTime <= 0)
            {
                IsEnd = true;
                for (int i = 0; i < listBall.Count; i++)
                    if (listBall[i].activeSelf)
                        DestroyBall(i);

                HUDm.DisplayEndGame(ballThrow, score);
            }

            if (listTraget.Count > 0)
            {
                currentTimeHelp -= Time.deltaTime;
                if (currentTimeHelp <= 0)
                {
                    listTraget[Random.Range(0, listTraget.Count - 1)].GetComponent<BehaviourTarget>().activateHelp(true);
                    currentTimeHelp = timeToHelp;
                }
            }
        }
    }

    public void EditScore(int toAdd)
    {
        soundM.playerSoundOneTime(GameTennis.Sound_Type.PointMade, 0.5f);

        score += toAdd;
        HUDm.EditScore(score);
        currentTimeHelp = timeToHelp;
    }

    public void BallThrow()
    {
        ballThrow++;
        HUDm.EditBallThrow(ballThrow);
    }

    private List<Vector3> listDirection = new List<Vector3>();

    public void sendBall(float power, bool goodShoot)
    {
        if (currentBallThrow >= SizeForballPoll - 1)
            currentBallThrow = 0;

        GameObject tmp = listBall[currentBallThrow];
        if (tmp.activeSelf)
            DestroyBall(tmp.GetComponent<BehaviourBall>().IDinList);
        tmp.transform.position = mainCamera.transform.position;

        currentBallThrow++;

        float powerTMP = (powerRange.y - powerRange.x) * power + powerRange.x;

        Vector3 direction = mainCamera.transform.forward;
        if (!goodShoot)
        {
            float randomY = Random.Range(RandomDirectionY.x, RandomDirectionY.y);
            float randomX = Random.Range(RandomDirectionX.x, RandomDirectionX.y);

            Vector3 RandomPos = mainCamera.transform.position + mainCamera.transform.up * randomY + mainCamera.transform.right * randomX + mainCamera.transform.forward*10;

            direction = (RandomPos - mainCamera.transform.position).normalized;
            listDirection.Add(direction);
        }

        soundM.playerSoundOneTime(GameTennis.Sound_Type.ShootBall, 0.5f);

        tmp.SetActive(true);

        tmp.GetComponent<Rigidbody>().AddForce(direction * powerTMP * powerMinMultiplicator,ForceMode.Impulse);


        BallThrow();
    }

    public void DestroyBall(int id)
    {
        //soundM.PlaySoundOnObject(listBall[id].GetComponent<AudioSource>(), GameTennis.Sound_Type.BallExplose);
        listBall[id].SetActive(false);
        listBall[id].GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameObject tmp = ListEffect[currentEffectPlay];
        tmp.transform.position = listBall[id].transform.position;
        tmp.GetComponent<ParticleSystem>().Play();
        //listBall[currentEffect]
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < listDirection.Count; i++)
            Gizmos.DrawLine(mainCamera.transform.position, mainCamera.transform.position+listDirection[i]);
    }
}
