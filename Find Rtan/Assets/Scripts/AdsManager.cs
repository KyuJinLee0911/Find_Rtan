using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    public static AdsManager instance;

    string adType;
    string gameId;
    void Awake()
    {
        if (instance == null)
            instance = this;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            adType = "Rewarded_iOS";
            gameId = "5361561";
        }
        else
        {
            adType = "Rewarded_Android";
            gameId = "5361560";
        }

        Advertisement.Initialize(gameId, true);
        _showAdButton.interactable = false;
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(adType))
        {
            // 클릭 시 ShowAd() 메서드를 호출하도록 버튼을 설정합니다.
            _showAdButton.onClick.AddListener(ShowRewardAd);
            // 유저가 클릭할 수 있도록 버튼을 활성화합니다.
            _showAdButton.interactable = true;
        }
    }

    public void ShowRewardAd()
    {
        Advertisement.Show(adType, this);
    }

    void ResultAds(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.LogError("광고 보기에 실패했습니다.");
                break;
            case ShowResult.Skipped:
                Debug.Log("광고를 스킵했습니다.");
                break;
            case ShowResult.Finished:
                // 광고 보기 보상 기능 
                Debug.Log("광고 보기를 완료했습니다.");
                break;
        }
    }

    // Show Listener의 OnUnityAdsShowComplete 콜백 메서드를 구현하여 유저가 보상을 받을지 결정합니다.
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(adType) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // 보상을 줍니다.
        }
    }

    // Load 및 Show 리스너 오류 콜백을 구현합니다.
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // 오류 세부 정보를 사용하여 또 다른 광고를 로드할지 여부를 결정합니다.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // 오류 세부 정보를 사용하여 또 다른 광고를 로드할지 여부를 결정합니다.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
