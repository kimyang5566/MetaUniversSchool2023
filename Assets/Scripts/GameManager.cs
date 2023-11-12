using Invector.vCharacterController;         //引入第三人称插件库
//using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


namespace Com.yoyo.MetaSchool
{
    public class GameManager : MonoBehaviour
    {
        #region public Fields
        public static GameManager instance;     //定义脚本实例 
        public vThirdPersonInput control;       //定义控制组件
        public GameObject storyUI;              //定义剧情UI
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
            instance = this;                    //实例初始化
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
            //if (TitleManager.newGame == false)    //游戏初始化时判断是否为新游戏
            //{
            //    LoadGame();                         //如果不是新游戏，就读档
            //}
            StoryManager.instance.Hide();           //隐藏剧情UI
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


        public void StartStory()                    //触发剧情
        {
            storyUI.SetActive(true);                //把剧情UI打开
            control.gameObject.SetActive(false);    //关闭角色
            StoryManager.instance.Show();           //显示对话框
        }

        public void StopStory()                    //停止剧情
        {
            control.gameObject.SetActive(true);    //打开角色
            StoryManager.instance.Hide();          //隐藏对话框
        }

        public void SaveGame()            //保存游戏
        {
            PlayerPrefs.SetFloat("charaX", control.transform.position.x);    //保存角色x坐标
            PlayerPrefs.SetFloat("charaY", control.transform.position.y);    //保存角色y坐标
            PlayerPrefs.SetFloat("charaZ", control.transform.position.z);    //保存角色z坐标

            for (int i = 0; i < PackManager.instance.itemGet.Length; i++)     //从0到50循环
            {
                PlayerPrefs.SetString("item_" + i, PackManager.instance.itemGet[i].ToString());
                //将每个item的获得状态存到每个存档中
            }
        }

        public void LoadGame()            //读取游戏
        {
            control.transform.position =
            new Vector3(PlayerPrefs.GetFloat("charaX"), PlayerPrefs.GetFloat("charaY"), PlayerPrefs.GetFloat("charaZ"));
            //把存档中的xyz坐标给角色坐标

            for (int i = 0; i < PackManager.instance.itemGet.Length; i++)      //从0到50循环
            {
                if (PlayerPrefs.GetString("item_" + i) == "True")              //如果当前道具的存档内容为true
                {
                    PackManager.instance.itemGet[i] = true;                    //将当前道具改为true
                    PackManager.instance.ChangeItem(i, true);                  //包管理器中获得道具
                }
                else                                                           //否则
                {
                    PackManager.instance.itemGet[i] = false;                   //将当前道具改为false
                    PackManager.instance.ChangeItem(i, false);                  //包管理器中获得道具
                }

                Debug.Log(PlayerPrefs.GetString("item_" + i));                 //读取50个道具状态存档
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
