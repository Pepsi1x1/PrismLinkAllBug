using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Xamarin.Forms.Internals;
using Incise.Services;
using System.Threading.Tasks;
using Prism.Logging;

namespace LinkAllBug
{
    public partial class App : Prism.DryIoc.PrismApplication
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<SplashScreenPage>();
        }

        protected override async void OnInitialized()
        {
            this.InitializeComponent();
            Xamarin.Forms.Device.SetFlags(new[] { "Visual_Experimental", "CollectionView_Experimental" });

#if DEBUG
            //HotReloader.Current.Run(this);
            // Handle Xamarin Form Logging events such as Binding Errors
            Log.Listeners.Add(new TraceLogListener());
#endif

            this.LogUnobservedTaskExceptions();

            await base.NavigationService.NavigateAsync("SplashScreenPage");
        }

        private void LogUnobservedTaskExceptions()
        {
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                base.Container.Resolve<ILoggerFacade>().Log(e.Exception.ToString(), Category.Exception, Priority.None);
            };
        }
    }

}
