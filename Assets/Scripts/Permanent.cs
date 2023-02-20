public class Permanent : Singleton<Permanent>
{
    #region Unity Methods

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        DontDestroyOnLoad(this);
    }

    #endregion Unity Methods
}