using ElmSharp;
using ElmSharp.Wearable;

namespace MaterialGallery
{
    public abstract class BaseGalleryPage
    {
        public abstract string Name { get; }

        public CircleSurface CircleSurface { get; set; }

        public abstract EvasObject CreateContent(EvasObject parent);

        public virtual void Run(Window window)
        {
            Conformant comformant = CreateComformant(window);
            var content = CreateContent(window);
            comformant.SetContent(content);
        }

        public virtual Conformant CreateComformant(Window window)
        {
            Conformant conformant = new Conformant(window);
            conformant.Show();
            return conformant;
        }

        public virtual EvasObject GetTitleLabel(EvasObject parent)
        {
            var label = new Label(parent)
            {
                Text = "<span align='center'><b>" + Name+"</b></span>",
                Color = Color.FromHex("6200EE"),
                AlignmentX = 0.5,
                AlignmentY = 0.5,
                WeightX = 1,
                WeightY = 1,
                MinimumWidth = 360,
            };
            label.Show();
            return label;
        }

        public virtual void TearDown() { }
    }
}
