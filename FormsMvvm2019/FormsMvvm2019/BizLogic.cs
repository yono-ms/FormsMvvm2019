using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsMvvm2019
{
    static class BizLogic
    {
        private static readonly ILogger logger = AppLogger.GetLogger(nameof(BizLogic));

        public static Command ItemSelectedCommand => new Command((args) =>
        {
            var item = args as PrefItem;
            var page = new NextPage { Title = item.Name };
            var vm = page.BindingContext as NextViewModel;
            vm.Code = item.Code;
            MessagingCenter.Send(new PushEventArgs(page), Constants.MessageCoordinator);
        });

        public static Command Command => new Command((args) =>
        {
            LogTrace($"START {args}");
            switch (args)
            {
                case MainViewModel mainViewModel:
                    logger.LogInformation($"Button event. {mainViewModel.PassCode}");
                    MessagingCenter.Send(new AlertEventArgs("BizLogic", $"PassCode={mainViewModel.PassCode}", "OK"), Constants.MessageCoordinator);
                    break;

                default:
                    MessagingCenter.Send(new AlertEventArgs("BizLogic", $"unknown args={args}", "OK"), Constants.MessageCoordinator);
                    break;
            }
            LogTrace($"END");
        });

        private static void LogTrace(string message, [CallerMemberName] string memberName = null)
        {
            logger.LogTrace($"{memberName} {message}");
        }

        public static async Task InitializePrefItemsAsync()
        {
            LogTrace("START");
            try
            {
                var items = await App.Database.GetPrefItemsAsync();
                logger.LogTrace($"items.Count={items.Count}");
                if (items.Count == 0)
                {
                    logger.LogTrace($"InsertAll.");
                    var resourceID = "FormsMvvm2019.Assets.Pref.json";
                    using(var stream = Application.Current.GetType().GetTypeInfo().Assembly.GetManifestResourceStream(resourceID))
                    using (var reader = new StreamReader(stream))
                    {
                        var json = await reader.ReadToEndAsync();
                        logger.LogDebug(json);
                        items = JsonConvert.DeserializeObject<List<PrefItem>>(json);
                    }
                    var count = await App.Database.SaveItemsAsync(items);
                    logger.LogDebug($"SaveItemsAsync count={count}");
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "InitializePrefItems");
            }
            LogTrace("END");
        }

        public static void InitializeViewModel(object args)
        {
            LogTrace("START");
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    LogTrace("thread START");
                    switch (args)
                    {
                        case MainViewModel mainViewModel:
                            mainViewModel.PrefItems = await App.Database.GetPrefItemsAsync();
                            break;

                        default:
                            logger.LogError($"unknown args={args}");
                            break;
                    }
                    LogTrace("thread END");
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "InitializeViewModel");
                }
            });
            LogTrace("END");
        }
    }
}
