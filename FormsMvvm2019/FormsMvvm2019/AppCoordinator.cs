using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FormsMvvm2019
{
    /// <summary>
    /// 画面遷移処理を一元化する.
    /// </summary>
    public class AppCoordinator
    {
        readonly ILogger logger;

        public AppCoordinator()
        {
            logger = AppLogger.GetLogger(this);
        }

        NavigationPage navigationPage;

        public void Initialize()
        {
            MessagingCenter.Subscribe<AlertEventArgs>(this, Constants.MessageCoordinator, async (s) =>
            {
                logger.LogInformation($"DisplayAlert {s.Title} {s.Message}");
                await Application.Current.MainPage.DisplayAlert(s.Title, s.Message, s.Cancel);
            });

            MessagingCenter.Subscribe<PushEventArgs>(this, Constants.MessageCoordinator, async (s) =>
            {
                logger.LogInformation($"{s.Type} {s.NextPage}");
                if (s.Type == PushType.Push)
                {
                    await navigationPage.CurrentPage.Navigation.PushAsync(s.NextPage);
                }
                else
                {
                    await navigationPage.CurrentPage.Navigation.PushModalAsync(s.NextPage);
                }
            });

            MessagingCenter.Subscribe<PopEventArgs>(this, Constants.MessageCoordinator, async (s) =>
            {
                logger.LogInformation($"{s.Type}");
                if (s.Type == PushType.Push)
                {
                    await navigationPage.CurrentPage.Navigation.PopAsync();
                }
                else
                {
                    await navigationPage.CurrentPage.Navigation.PopModalAsync();
                }
            });

            navigationPage = new NavigationPage(new SplashPage());
            Application.Current.MainPage = navigationPage;
        }
    }

    public enum PushType
    {
        Push,
        PushModal,
    }

    /// <summary>
    /// Push用イベント引数
    /// </summary>
    public class PushEventArgs : EventArgs
    {
        public Page NextPage { get; set; }
        public PushType Type { get; set; }
        public PushEventArgs(Page page, PushType type=PushType.Push)
        {
            NextPage = page;
            Type = type;
        }
    }

    /// <summary>
    /// Pop用イベント引数
    /// </summary>
    public class PopEventArgs : EventArgs
    {
        public PushType Type { get; set; }
        public PopEventArgs(PushType type = PushType.Push)
        {
            Type = type;
        }
    }

    /// <summary>
    /// DisplayAlert用イベント引数.
    /// </summary>
    public class AlertEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Cancel { get; set; }

        /// <summary>
        /// コンストラクタ.
        /// 投げっぱなし用.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="cancel"></param>
        public AlertEventArgs(string title, string message, string cancel)
        {
            Title = title;
            Message = message;
            Cancel = cancel;
        }
    }
}
