using ElmSharp;
using Tizen.NET.MaterialComponents;
using System.IO;
using Tizen;

namespace MaterialGallery
{
    class FloatingActionButtonPage : BaseGalleryPage
    {
        public override string Name => "FloatingActionButton";

        MConformant _conformant;

        public override Conformant CreateComformant(Window window)
        {
            _conformant = new MConformant(window);
            _conformant.Show();
            return _conformant;
        }

        public override EvasObject CreateContent(EvasObject parent)
        {
            if (_conformant == null)
                return null;

            Box box = new ColoredBox(parent);
            box.Show();

            var rect = new Rectangle(parent)
            {
                WeightX = 1,
                WeightY = 1,
                AlignmentX = -1,
                AlignmentY = -1,
            };

            box.PackEnd(rect);

            #region FABs
            MFloatingActionButton fab = new MFloatingActionButton(_conformant);
            fab.Show();
            fab.Resize(180, 176);
            fab.Move(90, 20);

            Image img = new Image(parent);
            //The source of icon resources is https://materialdesignicons.com/
            img.Load(Path.Combine(MaterialWatchGallery.ResourceDir, "alarm.png"));
            img.Show();
            fab.Icon = img;

            MFloatingActionButton fab2 = new MFloatingActionButton(_conformant);
            fab2.Show();
            fab2.Resize(180, 176);
            fab2.Move(90, 200);

            Image img2 = new Image(parent);
            //The source of icon resources is https://materialdesignicons.com/
            img2.Load(Path.Combine(MaterialWatchGallery.ResourceDir, "airplane.png"));
            img2.Show();
            fab2.Icon = img2;

            MFloatingActionButton fab3 = new MFloatingActionButton(_conformant);
            fab3.Show();
            fab3.Resize(180, 176);
            fab3.Move(90, 400);

            Image img3 = new Image(parent);
            //The source of icon resources is https://materialdesignicons.com/
            img3.Load(Path.Combine(MaterialWatchGallery.ResourceDir, "bluetooth.png"));
            img3.Show();
            fab3.Icon = img3;
            #endregion

            fab.Clicked += (s, e) =>
            {
                parent.Unrealize();
            };
            return box;
        }
    }
}
