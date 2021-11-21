using UnityEngine;
using Yodo1.MAS;
public class AdsBanner : MonoBehaviour
{
#if UNITY_ANDROID
    private Yodo1U3dBannerAdView bannerAdView;
    
    public void Start()
    {
        Yodo1U3dMasCallback.SetInitializeDelegate((bool success, Yodo1U3dAdError error) => { });
        Yodo1U3dMas.InitializeSdk();

        RequestBanner();

        ShowBanner();
    }
    private void RequestBanner()
    {
        if (bannerAdView != null)
        {
            bannerAdView.Destroy();
        }
        bannerAdView = new Yodo1U3dBannerAdView(Yodo1U3dBannerAdSize.Banner, Yodo1U3dBannerAdPosition.BannerTop | Yodo1U3dBannerAdPosition.BannerHorizontalCenter);
    }
    public void ShowBanner()
    {
        bannerAdView.Show();
    }
#endif
}