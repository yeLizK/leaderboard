# leaderboard
This leaderboard is creator for a Live Creator Challenge that is part of Mastered Bootcamp. In Live Creator Challenges, we were expected to developed certain features for games in 6 hours. 
In this challenge, we were asked to create a leaderboard by either storing data locally or using PlayFab.

The following code can be used to login to PlayFab leaderboard and get/set entries.




    public class PlayFabManager : MonoBehaviour
    {
        private static PlayFabManager instance;
        public static PlayFabManager Instance { get { return instance; } }

        [SerializeField]
        private Transform EntryParent;
        [SerializeField]
        private GameObject EntryPrefab;


        private void Start()
        {
            Login();
        }
        private void Awake()
        {
            if(instance!=null && instance != this)
            {
                Destroy(instance);
            }
            else
            {
                instance = this;
            }
        }
        // Start is called before the first frame update
        public void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

        }

        private void OnSuccess(LoginResult result)
        {
            Debug.Log("Successful login/account create!");
        }
        private void OnError(PlayFabError error)
        {
            Debug.Log("Error while logging in/creatin account!");
            Debug.Log(error.GenerateErrorReport());
        }

        public void SendLeaderboard(int score)
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate> { new StatisticUpdate
                {
                    StatisticName = "MiniGameLeaderboard",
                    Value = score
                } }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
        }

        private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Successfull leaderboard sent");
        }

        public void GetLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "MiniGameLeaderboard",
                MaxResultsCount = 10
            };
            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        }

        private void OnLeaderboardGet(GetLeaderboardResult result)
        {
            foreach(Transform item in EntryParent)
            {
                Destroy(item.gameObject);
            }

            foreach(var item in result.Leaderboard)
            {
                GameObject newEntry = Instantiate(EntryPrefab, EntryParent);
                TMP_Text[] texts = newEntry.GetComponentsInChildren<TMP_Text>();
                texts[0].text = (item.Position + 1).ToString();
                texts[1].text = item.DisplayName;
                if(texts[1].text == null)
                {
                    texts[1].text = item.PlayFabId;
                }
                texts[2].text = item.StatValue.ToString();

                Debug.Log(string.Format("PLACE: {0} | ID: {1} | VALUE: {2}", item.Position, item.PlayFabId, item.StatValue));
            }
        }

    }
