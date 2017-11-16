using System;
using Xamarin.Core.Infrastructure.Interfaces;
using Xamarin.Forms;
using XamarinForms.Core.Droid.Renderers;

//[assembly: Xamarin.Forms.Dependency(typeof(AdvInterstitial))]
//namespace XamarinForms.Core.Droid.Renderers
//{
//    public class AdvInterstitial : IAdvInterstitial
//    {
//        InterstitialAd _ad;

//        #region IAdMobInterstitial implementation

//        public void Show(string adUnit)
//        {
//            var context = Forms.Context;
//            _ad = new InterstitialAd(context);
//            _ad.AdUnitId = adUnit;



//            var intlistener = new InterstitialAdListener(_ad);
//            intlistener.OnAdLoaded();
//            _ad.AdListener = intlistener;

//            try
//            {
//                var requestbuilder = new AdRequest.Builder();
//                _ad.LoadAd(requestbuilder.Build());
//            }
//            catch (Exception)
//            {
//                _ad.Dispose();
//            }
//        }

//        #endregion

//    }

//    public class InterstitialAdListener : AdListener
//    {
//        readonly InterstitialAd _ad;

//        public InterstitialAdListener(InterstitialAd ad)
//        {
//            _ad = ad;
//        }

//        public override void OnAdLoaded()
//        {
//            base.OnAdLoaded();

//            if (_ad.IsLoaded)
//                _ad.Show();

//        }
//    }
//}

