
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing.Security;
using UnityEngine.Purchasing;
[Serializable]
public class InAppItem{
	public string InAppName;
	public string InAppString;
	public ProductType producttype;

}

public class InApp_Manager : MonoBehaviour, IStoreListener
{
	public static InApp_Manager instance_;
	public static InApp_Manager instance{
		get{
			if (!instance_)
				instance_ = GameObject.FindObjectOfType<InApp_Manager> ();

			return instance_;
		}
	}
	//[Header("Enter Google Play Key ")]
	//public string GooglePlayKey;
	//[Space]
	[Header("Enter InApp strings ")]
	public InAppItem[] InAppIds=null;
	public static event EventHandler consumable_events;
	private static IStoreController m_StoreController;          
	private static IExtensionProvider m_StoreExtensionProvider; 
	public static bool check_Unlockall=false;
	public static string No_AdsInGame = "noads";
	private static string kProductNameAppleSubscription =  "com.unity3d.subscription.new";
	private static string kProductNameGooglePlaySubscription =  "com.unity3d.subscription.original";
	private static byte[] StringKey;
	private GameObject Acknowledgement;
    private Vector2 anchor_Min, anchor_Mix;
	private Canvas AcknowledgementCanvas;
	private Vector3 PanelPosition;
	private int ProductNumber;
	void Awake(){
	//	StringKey = System.Convert.FromBase64String(GooglePlayKey);
		DontDestroyOnLoad (instance);
	}
	void Start()
	{
		if (m_StoreController == null)
		{
			
			InitializePurchasing();
		}
	
	}

	public void Buy_UnlockAll_Removeads(){
		Buy_Product (0);
	}

	public void Buy_UnlockAll_Levels(){
		Buy_Product (1);
	}


	public void Buy_UnlockAll_Players()
    {
        Buy_Product(2);
    }
	public void Buy_UnlockAll_All()
	{
		Buy_Product(3);
	}


	public void InitializePurchasing() 
	{
		if (IsInitialized())
		{

			// ... we are done here.
			return;
		}


		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        for (int i = 0; i < InAppIds.Length; i++)
		{
			builder.AddProduct(InAppIds[i].InAppString,InAppIds[i].producttype);
		}
		 UnityPurchasing.Initialize(this, builder);
	}


	public bool IsInitialized()
	{
		print ("Pass");
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

 public void Buy_Product(int iapID){
		if (IsInitialized () && InternetStatus()) {
			
			if (InAppIds[iapID].producttype == ProductType.NonConsumable) {
				if (!CheckProductID_Status (InAppIds[iapID].InAppString)) {
					ProductNumber = iapID;
					BuyProductID (InAppIds[iapID].InAppString);
				}
			} else {
				BuyProductID (InAppIds[iapID].InAppString);
			}
			
		}
		
	}

	public bool CheckProductID_Status(string productId){
		Product product = m_StoreController.products.WithID(productId);
		if (product != null && product.hasReceipt) {

			return true;
		} else {
			return false;
		}
	}

	void BuyProductID(string productId)
	{
		
		if (IsInitialized() && InternetStatus())
		{
			Product product = m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				m_StoreController.InitiatePurchase(product);
			}
			else
			{
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}

		else
		{
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
#if UNITY_EDITOR
		Debug.Log("Call InApp Produc Id = " + productId);
#endif

	}
public void RestorePurchases()
	{

		if (!IsInitialized() && InternetStatus())
		{
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}


		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{

			Debug.Log("RestorePurchases started ...");


			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();

			apple.RestoreTransactions((result) => {

				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}

		else
		{

			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}


	//  
	// --- IStoreListener
	//

	void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		m_StoreController = controller;

		m_StoreExtensionProvider = extensions;
		if (IsInitialized ()) {
			
		}
		
	}

	 void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
	{

		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs args) 
	{

		//%%%%%%%%%%%%%%%%%%%%%%%%%%% InApp Call Back %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
		
		ShowAcknowledgement(InAppIds[ProductNumber].InAppName + " Is Completed ");
		if (String.Equals(args.purchasedProduct.definition.id, InAppIds[0].InAppString, StringComparison.Ordinal))//RemoveAds
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			PlayerPrefManager.Instance.RemoveAds();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, InAppIds[1].InAppString, StringComparison.Ordinal))//RemoveAds
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			PlayerPrefManager.Instance.unlocklevels();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, InAppIds[2].InAppString, StringComparison.Ordinal))//RemoveAds
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			PlayerPrefManager.Instance.unlockplayers();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, InAppIds[3].InAppString, StringComparison.Ordinal))//RemoveAds
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			PlayerPrefManager.Instance.RemoveAds();
			PlayerPrefManager.Instance.unlocklevels();
			PlayerPrefManager.Instance.unlockplayers();
		}
	    
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else 
		{
			
			
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		
		return PurchaseProcessingResult.Complete;
	}


	void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		ShowAcknowledgement(InAppIds[ProductNumber].InAppName + " Is FAIL ");
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	public void give_CosumeEvent(){
		if (consumable_events != null)
			consumable_events (null, null);
	}

	public void removeall_ConsumeEvent(){
		consumable_events = null;
	}
	public bool InternetStatus()
	{


		if (Application.internetReachability != NetworkReachability.NotReachable)
			return true;
		else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
			return true;
		else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
			return true;
		else
			return false;
	}


	public void ShowAcknowledgement(string name)
	{
		if (AcknowledgementCanvas == null)
		{
			if (AdmobAdsManager.Instance)
				AdmobAdsManager.Instance.hideMediumBanner();
			 GameObject TempCanvas = new GameObject();
			TempCanvas.name = "Acknowledgement";
			TempCanvas.AddComponent<Canvas>();
			TempCanvas.AddComponent<GraphicRaycaster>();
			TempCanvas.AddComponent<CanvasScaler>();
			CanvasScaler canvasScaler = TempCanvas.GetComponent<CanvasScaler>();
			canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			canvasScaler.matchWidthOrHeight = 1f;
			AcknowledgementCanvas = TempCanvas.GetComponent<Canvas>();
			AcknowledgementCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
			AcknowledgementCanvas.sortingOrder = 100;

			var LoadingAdBg = new GameObject("AcknowledgementBg");
			var Bg1 = LoadingAdBg.AddComponent<Image>();
			Bg1.transform.parent = AcknowledgementCanvas.transform;
			Bg1.rectTransform.sizeDelta = new Vector2(700f, 400f);
			Bg1.rectTransform.localScale = new Vector3(1, 1, 1);
			Bg1.color = new Color(0f, 0f, 0f, 0.7f);
			Bg1.rectTransform.anchoredPosition = SetPanelPosition();
			Bg1.rectTransform.anchorMin = new Vector2(0f, 0f);
			Bg1.rectTransform.anchorMax = new Vector2(1f, 1f);
			Bg1.raycastTarget = true;
			



			var NativeAdBg = new GameObject("AcknowledgementPanel");
			var Bg = NativeAdBg.AddComponent<Image>();
			Bg.transform.parent = AcknowledgementCanvas.transform;
			Bg.rectTransform.sizeDelta = new Vector2(700f, 400f);
			Bg.rectTransform.localScale = new Vector3(1, 1, 1);

			Bg.rectTransform.anchoredPosition = SetPanelPosition();
			Bg.rectTransform.anchorMin = anchor_Min;
			Bg.rectTransform.anchorMax = anchor_Mix;
			Bg.raycastTarget = false;
			Bg.color = Color.grey;
			Acknowledgement = Bg.gameObject;



			var HeadLine = new GameObject("HeadLineText");
			var _HeadLine = HeadLine.AddComponent<Text>();
			_HeadLine.transform.parent = Bg.transform;
			_HeadLine.rectTransform.anchorMin = new Vector2(0.015f, 0.25f);
			_HeadLine.rectTransform.anchorMax = new Vector2(0.98f, 1f);
			_HeadLine.rectTransform.sizeDelta = new Vector2(0f, 0f);
			_HeadLine.rectTransform.anchoredPosition = new Vector3(0f, 0f, 0f);
			_HeadLine.rectTransform.localScale = new Vector3(1, 1, 1);
			_HeadLine.text = "Purchase Process " + name;
			_HeadLine.font = Resources.FindObjectsOfTypeAll<Font>()[0];
			_HeadLine.fontSize = 20;
			_HeadLine.alignment = TextAnchor.MiddleCenter;
			_HeadLine.resizeTextForBestFit = true;
			_HeadLine.color = Color.black;


			var buttonObject = new GameObject("Okbutton");
			var image = buttonObject.AddComponent<Image>();
			image.transform.parent = Bg.transform;
			image.rectTransform.anchorMin = new Vector2(0f, 0f);
			image.rectTransform.anchorMax = new Vector2(1f, 0.25f);
			image.rectTransform.sizeDelta = new Vector2(0f, 0f);
			image.rectTransform.anchoredPosition = new Vector3(0f, 0f, 0f);
			image.rectTransform.localScale = new Vector3(1, 1, 1);
			image.color = Color.green;
			var button = buttonObject.AddComponent<Button>();
			button.targetGraphic = image;

			var CallAction = new GameObject("Text");
			CallAction.transform.parent = buttonObject.transform;
			var _CallAction = CallAction.AddComponent<Text>();
			_CallAction.rectTransform.anchorMin = new Vector2(0f, 0f);
			_CallAction.rectTransform.anchorMax = new Vector2(1f, 1f);
			_CallAction.rectTransform.sizeDelta = new Vector2(0f, 0f);
			_CallAction.rectTransform.anchoredPosition = new Vector2(0f, 0f);
			_CallAction.rectTransform.localScale = new Vector3(1, 1, 1);
			_CallAction.text = "Close";
			_CallAction.color = Color.black;
			_CallAction.font = Resources.FindObjectsOfTypeAll<Font>()[0];
			_CallAction.fontSize = 50;
			_CallAction.alignment = TextAnchor.MiddleCenter;
			_CallAction.resizeTextForBestFit = true;
			button.onClick.AddListener(TaskOnClick);
			Invoke("TaskOnClick",3f);
		}
	}
	private Vector3 SetPanelPosition()
          {
		
			PanelPosition = new Vector3(0f, 0f, 0f);
			anchor_Min = new Vector2(0.5f, 0.5f);
			anchor_Mix = new Vector2(0.5f, 0.5f);
		    return PanelPosition;
		}
	void TaskOnClick()
	{
		if (AcknowledgementCanvas)
		Destroy(AcknowledgementCanvas.gameObject);
	}

}

