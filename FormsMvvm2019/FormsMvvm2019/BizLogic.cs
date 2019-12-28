using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace FormsMvvm2019
{
    static class BizLogic
    {
        private static readonly ILogger logger = AppLogger.GetLogger(nameof(BizLogic));

        static public Command Command => new Command((args) =>
        {
            LogTrace($"START {args}");
            switch (args)
            {
                case MainViewModel mainViewModel:
                    Application.Current.MainPage.DisplayAlert("BizLogic", $"PassCode={mainViewModel.PassCode}", "OK");
                    logger.LogInformation($"Button event. {mainViewModel.PassCode}");
                    break;

                default:
                    Application.Current.MainPage.DisplayAlert("BizLogic", $"unknown args={args}", "OK");
                    break;
            }
            LogTrace($"END");
        });

        private static void LogTrace(string message, [CallerMemberName] string memberName = null)
        {
            logger.LogTrace($"{memberName} {message}");
        }
    }
}
