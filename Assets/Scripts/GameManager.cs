using Invector.vCharacterController;         //��������˳Ʋ����
//using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


namespace Com.yoyo.MetaSchool
{
    public class GameManager : MonoBehaviour
    {
        #region public Fields
        public static GameManager instance;     //����ű�ʵ�� 
        public vThirdPersonInput control;       //����������
        public GameObject storyUI;              //�������UI
        public GameObject[] tableModels;
        public PlayerManager localPlayer;
        float[] tableModelScales;
        public GameObject portalEffect;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;
        public Transform playerBorn;
        public GameObject postBlur;

        //public static object Instance { get; internal set; }
        #endregion

        #region Photon Callbacks


        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        
        public void OnLeftRoom()
        {
            //SceneManager.LoadScene(0);
            Application.Quit();
        }
        
        /*
        public override void OnPlayerEnteredRoom(Player other)
        {
            //Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }
        */

        private void Awake()
        {
            instance = this;                    //ʵ����ʼ��
            //PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            /*
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    localPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, playerBorn.position + new Vector3(Random.Range(-1.0f,1.0f), 0f, Random.Range(-1.0f, 1.0f)), playerBorn.rotation, 0).GetComponent<PlayerManager>();
                    
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
            */
            //if (TitleManager.newGame == false)    //��Ϸ��ʼ��ʱ�ж��Ƿ�Ϊ����Ϸ
            //{
            //    LoadGame();                         //�����������Ϸ���Ͷ���
            //}
            StoryManager.instance.Hide();           //���ؾ���UI
            tableModelScales = new float[tableModels.Length];
            for (int i = 0; i < tableModels.Length; i++)
            {
                tableModelScales[i] = tableModels[i].transform.localScale.x;
            }
            InvokeRepeating("SwitchTableModel", 1.0f, 5.0f);
        }

        void SwitchTableModel()
        {
            for (int i = 0; i < tableModels.Length; i++)
            {
                if (tableModels[i].activeSelf)
                {
                    //item.transform.localScale = new Vector3(0, 0, 0);
                    tableModels[i].SetActive(false);
                }
                else
                {
                    //item.transform.localScale = new Vector3(0, 0, 0);
                    tableModels[i].SetActive(true);
                    tableModels[i].transform.localScale = new Vector3(0, 0, 0);
                    tableModels[i].transform.DOScale(tableModelScales[i], 0.5f);
                }
            }
                
        }


        #endregion

        #region Public Methods

        

        public void LeaveRoom()
        {
            //PhotonNetwork.LeaveRoom();
            Application.Quit();
        }


        public void StartStory()                    //��������
        {
            storyUI.SetActive(true);                //�Ѿ���UI��
            control.gameObject.SetActive(false);    //�رս�ɫ
            StoryManager.instance.Show();           //��ʾ�Ի���
        }

        public void StopStory()                    //ֹͣ����
        {
            control.gameObject.SetActive(true);    //�򿪽�ɫ
            StoryManager.instance.Hide();          //���ضԻ���
        }

        public void SaveGame()            //������Ϸ
        {
            PlayerPrefs.SetFloat("charaX", control.transform.position.x);    //�����ɫx����
            PlayerPrefs.SetFloat("charaY", control.transform.position.y);    //�����ɫy����
            PlayerPrefs.SetFloat("charaZ", control.transform.position.z);    //�����ɫz����

            for (int i = 0; i < PackManager.instance.itemGet.Length; i++)     //��0��50ѭ��
            {
                PlayerPrefs.SetString("item_" + i, PackManager.instance.itemGet[i].ToString());
                //��ÿ��item�Ļ��״̬�浽ÿ���浵��
            }
        }

        public void LoadGame()            //��ȡ��Ϸ
        {
            control.transform.position =
            new Vector3(PlayerPrefs.GetFloat("charaX"), PlayerPrefs.GetFloat("charaY"), PlayerPrefs.GetFloat("charaZ"));
            //�Ѵ浵�е�xyz�������ɫ����

            for (int i = 0; i < PackManager.instance.itemGet.Length; i++)      //��0��50ѭ��
            {
                if (PlayerPrefs.GetString("item_" + i) == "True")              //�����ǰ���ߵĴ浵����Ϊtrue
                {
                    PackManager.instance.itemGet[i] = true;                    //����ǰ���߸�Ϊtrue
                    PackManager.instance.ChangeItem(i, true);                  //���������л�õ���
                }
                else                                                           //����
                {
                    PackManager.instance.itemGet[i] = false;                   //����ǰ���߸�Ϊfalse
                    PackManager.instance.ChangeItem(i, false);                  //���������л�õ���
                }

                Debug.Log(PlayerPrefs.GetString("item_" + i));                 //��ȡ50������״̬�浵
            }
        }

        public void OpenMap()
        {

        }

        public void ChangeView()
        {
            localPlayer.ChangeView();
        }
        #endregion

        #region Private Methods


        void LoadArena()
        {
            /*
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for 1");// + PhotonNetwork.CurrentRoom.PlayerCount);
            */
        }


        #endregion

    }
}
