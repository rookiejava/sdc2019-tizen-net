using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Tizen.Applications;
using ElmSharp;
using ElmSharp.Wearable;
using Tizen.NET.MaterialComponents;
using ElottieSharp;

namespace MaterialGallery
{
    class MaterialWatchGallery : CoreUIApplication
    {
        Window _window;
        int _screenWidth;
        int _animationloopCount;
        int _pageIndex;

        public static string ResourceDir { get; private set; }

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            ResourceDir = DirectoryInfo.Resource;
            MaterialWatchGallery.ResourceDir = DirectoryInfo.Resource;
            ThemeLoader.Initialize(ResourceDir);

            _window = new Window("WatchMaterialGallery")
            {
                AvailableRotations = DisplayRotation.Degree_0 | DisplayRotation.Degree_180 | DisplayRotation.Degree_270 | DisplayRotation.Degree_90
            };
            _window.BackButtonPressed += (s, e) =>
            {
                Exit();
            };
            _window.Show();

            _screenWidth = _window.ScreenSize.Width;
            CreateGalleryPage(_window);
        }

        IEnumerable<BaseGalleryPage> GetGalleryPage()
        {
            Assembly asm = typeof(MaterialWatchGallery).GetTypeInfo().Assembly;
            Type pageType = typeof(BaseGalleryPage);

            var pages = from page in asm.GetTypes()
                        where pageType.IsAssignableFrom(page) && !page.GetTypeInfo().IsInterface && !page.GetTypeInfo().IsAbstract
                        select Activator.CreateInstance(page) as BaseGalleryPage;

            return from page in pages
                   orderby page.Name
                   select page;
        }

        void CreateGalleryPage(Window window)
        {
            var conformant = new Conformant(window);
            conformant.Show();

            var surface = new CircleSurface(conformant);
            var circleScroller = new CircleScroller(conformant, surface)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
                VerticalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible,
                HorizontalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Auto,
                ScrollBlock = ScrollBlock.Vertical,
                HorizontalPageScrollLimit = 1,
            };
            ((IRotaryActionWidget)circleScroller).Activate();
            circleScroller.SetPageSize(1.0, 1.0);
            conformant.SetContent(circleScroller);
            circleScroller.Show();

            var box = new Box(window)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
                IsHorizontal = true,
                IsHomogeneous = true,
            };
            box.Show();
            box.PackEnd(CreateFirstPage(box));
            box.PackEnd(CreateThemePage(box));

            foreach (var tc in GetGalleryPage())
            {
                tc.CircleSurface = surface;
                if (tc is FloatingActionButtonPage fabPage)
                {
                    box.PackEnd(CreateNewWindow(box, tc));
                }
                else
                {
                    var view = tc.CreateContent(box);
                    box.PackEnd(view);
                }
            }
            circleScroller.SetContent(box);

            var animationView = new LottieAnimationView(window)
            {
                AutoPlay = true,
                AutoRepeat = true,
            };
            var path = Path.Combine(DirectoryInfo.Resource, "material_wave_loading.json");
            animationView.SetAnimation(path);
            animationView.Move(50, 200);
            animationView.Resize(250, 250);
            animationView.Show();


            animationView.Finished += (s, e) =>
            {
                if (_animationloopCount == 10)
                {
                    _animationloopCount = 0;
                    animationView.AutoRepeat = false;
                    animationView.Stop();
                    return;
                }
                _animationloopCount++;
            };

            _pageIndex = -1;
            circleScroller.Scrolled += (s, e) => 
            {
                if (_pageIndex == circleScroller.HorizontalPageIndex)
                {
                    return;
                }
                _pageIndex = circleScroller.HorizontalPageIndex;
                if (_pageIndex == 0)
                {
                    animationView.AutoRepeat = true;
                    animationView.Show();
                    animationView.Play();
                }
                else
                {
                    _animationloopCount = 0;
                    animationView.AutoRepeat = false;
                    animationView.Hide();
                    animationView.Stop();
                }
            };
        }

        EvasObject CreateNewWindow(EvasObject parent, BaseGalleryPage page)
        {
            var box = new ColoredBox(parent)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
            };
            box.Show();

            var button = new MButton(parent)
            {
                AlignmentX = -1,
                AlignmentY = 0.5,
                WeightX = 1,
                WeightY = 1,
                Text = "click"
            };
            button.Show();

            button.Clicked += (s, e) =>
            {
                Window window = new Window(page.Name);
                window.Show();
                window.BackButtonPressed += (sender, args) =>
                {
                    page.TearDown();
                    window.Hide();
                    window.Unrealize();
                };
                page.Run(window);
            };

            box.PackEnd(button);
            return box;
        }

        EvasObject CreateFirstPage(EvasObject parent)
        {
            var box = new Box(parent)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
            };
            box.Show();

            var label = new Label(parent)
            {
                Text = "<span align='center'>MaterialGallery<br>for Galaxy Watch</span>",
                AlignmentX = 0.5,
                AlignmentY = 0.5,
                WeightX = 1,
                WeightY = 1,
                MinimumWidth = _screenWidth,
            };
            label.Show();
            box.PackEnd(label);
            return box;
        }

        public EvasObject CreateThemePage(EvasObject parent)
        {
            var label = new Label(parent)
            {
                Text = "<span align='center'><br><b>Choose Theme</b></span>",
                Color = Color.FromHex("6200EE"),
                AlignmentX = 0.5,
                AlignmentY = 0.5,
                WeightX = 1,
                WeightY = 1,
                MinimumWidth = _screenWidth,
            };
            label.Show();

            Box box = new ColoredBox(parent)
            {
                IsHorizontal = false,
                WeightX = 1,
                WeightY = 1,
                AlignmentX = -1,
                AlignmentY =-1,
            };
            box.Show();

            var defaultColor = new MButton(parent)
            {
                Text = "default",
                MinimumWidth = 200,
                WeightY = 1,
                AlignmentY = 0.5
            };
            var light = new MButton(parent)
            {
                Text = "light",
                BackgroundColor = Color.Silver,
                MinimumWidth = 200,
                WeightY = 1,
                AlignmentY = 0.5
            };
            var dark = new MButton(parent)
            {
                Text = "Dark",
                BackgroundColor = Color.Black,
                MinimumWidth = 200,
                WeightY = 1,
                AlignmentY = 0.5
            };
            defaultColor.Show();
            light.Show();
            dark.Show();
            box.PackEnd(label);
            box.PackEnd(defaultColor);
            box.PackEnd(light);
            box.PackEnd(dark);

            defaultColor.Clicked += (s, e) => MColors.Current = MColors.Default;
            light.Clicked += (s, e) => MColors.Current = MColors.Light;
            dark.Clicked += (s, e) => MColors.Current = MColors.Dark;
            return box;
        }

        static void Main(string[] args)
        {
            Elementary.Initialize();
            Elementary.ThemeOverlay();

            CoreUIApplication app;
            app = new MaterialWatchGallery();
            app.Run(args);
        }
    }
}