using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ElottieFormsGallery
{
    public class LottieDataModel
    {
        public string AnimationFile { get; set; }

        public static IList<LottieDataModel> All { set; get; }

        static LottieDataModel()
        {
            All = new ObservableCollection<LottieDataModel> {
                new LottieDataModel
                {
                    AnimationFile = "funky_chicken.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "a_mountain.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "happy.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "done.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "material_wave_loading.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "heart.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "loading.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "balloons_with_string.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "acrobatics.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "loading_rectangles.json"
                },
                new LottieDataModel
                {
                    AnimationFile = "telegram.json"
                }
            };
        }
    }
}