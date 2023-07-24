using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DigitalTurbine.OfferWall;
using Fyber;

public class OfferWallIntegration : MonoBehaviour {


    /// The app id and security token for each of the platforms should be replaced with the credentials from the apps configure in the console.
    public Text dtFairbidVersion;
    public const string USER_ID = "user_id";
    public static string SECURITY_TOKEN = "DEFAULT_SECURITY_TOKEN";
    public static string APP_ID = "DEFAULT_APP_ID";
    public const string IOS_APP_ID = "135708";
    public const string IOS_SECURITY_TOKEN = "sec_135708"; 
    public const string ANDROID_APP_ID = "135704";
    public const string ANDROID_SECURITY_TOKEN = "sec_135704";
    
    public void Start()
    {   
    
    setFairBidVersionTextView();
    
    // The function to set platform-specific variables depending on the target platform
    SetPlatformVariables();
    
    // You might want to provide your custom user ID to OfferWall SDK. If you do not provide any value here,
    // the OfferWall SDK will generate one for you. You can change this value at any time.
    OfferWall.UserId = USER_ID;
    
    OfferWall.SetLogLevel(LogLevel.Verbose);
    // The OfferWall SDK `start` method can accept the parameter that defines if the usage of GAID is limited.
    // This is only applicable for Android and will be ignored for iOS. 
    bool disableAdvertisingId = false;
    // If you don't need to disable the advertising ID, you can omit this parameter as well. It will
    // fall back to `false `by default.
    
    // SubscribeOfwDelegates is used for initialisation of callbacks for the offerwall delegates.
    // This will allow you to be notified of Offer Wall lifecycle events.
    SubscribeOfwDelegates();
    
    // If you want to leverage Digital Turbine's virtual currency hosting, you'll need to register the respective 
    //delegates and start the SDK with a valid security token. 
    SubscribeVirtualCurrencyDelegates();
    
    OfferWall.StartSDK(APP_ID, SECURITY_TOKEN, disableAdvertisingId);
    
    // If, on the other hand you prefer to do server side rewarding, you can simply start the SDK by passing only the APP_ID
    // OfferWall.StartSDK(APP_ID);
    
    }
    
    public void SubscribeOfwDelegates() {
        OfferWall.OfferWallShownEvent += OnOfwShown;
        OfferWall.OfferWallClosedEvent += OnOfwClosed;
        OfferWall.OfferWallFailedToShowEvent += OnOfwFailedToShow;
    }
    
    public void SubscribeVirtualCurrencyDelegates() {
        OfferWall.VirtualCurrencyResponseEvent += OnVirtualCurrencySuccess;
        OfferWall.VirtualCurrencyErrorEvent += OnVirtualCurrencyError;
    }
    
    public void OnOfwShown(string placementId)
    {
        Debug.Log("Offer Wall Shown for PlacementId: " + placementId);
        // pause game
    }
    
    public void OnOfwClosed(string placementId)
    {
        Debug.Log("Offer Wall Closed for PlacementId: " + placementId);
        // check if the user has completed an offer and is entitled to currency
        OnRequestVirtualCurrency();
        // resume game
    }
    
    public void OnOfwFailedToShow(string? placementId, OfferWallError error)
    {
        Debug.Log("Offer Wall Failed To Show for PlacementId: " + placementId + "with Error: " + error);
        // user msg: try again later
        // resume game
    }
    
    // VirtualCurrency
    public void OnVirtualCurrencySuccess(VirtualCurrencySuccessfulResponse response)
    {
        double coinsEarned = response.DeltaOfCoins;
        Debug.Log("Virtual Coins Earned: " + coinsEarned);
        // update user in-game currency with coins earned
        }
        
        public void OnVirtualCurrencyError(VirtualCurrencyErrorResponse error)
        {
        Debug.Log("Virtual Currency Error: " + error);
        // typically something wrong with the integration 
        // It could be a temporary server issue. Schedule a new request for a different point later in the game.
    }
    
    public void OnShowOfferwall()
    {
        // If you want to show the Offer Wall for the default placements and you don't want to configure 
        // any of its options, you can use the following method:
        // OfferWall.Show();
        
        // You can customize the OfferWall behavior using the ShowOptions class
        bool closeOnRedirect = true;
        Dictionary<string, string> customParameters = new Dictionary<string, string>();
        customParameters.Add("key", "value");
        
        OfferWallShowOptions showOptions = new OfferWallShowOptions(closeOnRedirect, customParameters);
        OfferWall.Show(showOptions);
        
        // If you need to show the Offer Wall for a given placement, you can call the same API:
        // OfferWall.Show(showOptions, "placement Id");
    }
    
    public void OnRequestVirtualCurrency()
    {
        // If you want to request for default currency, you simply call this method:
        // OfferWall.RequestCurrency();
        
        // If you need some behaviour customization or request for a specific custom currency,
        // you should use the VirtualCurrencyRequestOptions class, that accepts the following parameters:
        // - whether the toast message should appear upon the successful gratification,
        // - the ID of the currency that you want to request for. This parameter is optional.
        bool showToastOnReward = true;
        string currencyId = "coins";
        VirtualCurrencyRequestOptions options = new VirtualCurrencyRequestOptions(showToastOnReward, currencyId);
        OfferWall.RequestCurrency(options);
    }
    
    private void setFairBidVersionTextView()
    {
        Debug.Log($"DT FAIRBID {FairBid.Version}");
        dtFairbidVersion.text = "DT FAIRBID " + FairBid.Version;
    }
    
    public static void SetPlatformVariables()
    {
        #if UNITY_IOS
        APP_ID = IOS_APP_ID;
        SECURITY_TOKEN = IOS_SECURITY_TOKEN;
        #elif UNITY_ANDROID
        APP_ID = ANDROID_APP_ID;
        SECURITY_TOKEN = ANDROID_SECURITY_TOKEN;
        #else
        APP_ID = "DEFAULT_APP_ID";
        SECURITY_TOKEN = "DEFAULT_SECURITY_TOKEN";
        #endif
    }

}

