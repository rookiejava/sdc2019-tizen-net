using System;
using System.Runtime.InteropServices;

namespace PrettyWeather.Tizen.Interop
{
    class LocalInterop
    {
        [DllImport("libevas.so.1")]
        internal static extern IntPtr evas_map_new(int count);

        [DllImport("libevas.so.1")]
        internal static extern void evas_map_util_points_populate_from_object_full(IntPtr map, IntPtr obj, int z);

        [DllImport("libevas.so.1")]
        internal static extern void evas_map_point_color_set(IntPtr map, int idx, int r, int g, int b, int a);

        [DllImport("libevas.so.1")]
        internal static extern void evas_object_map_set(IntPtr obj, IntPtr map);

        [DllImport("libevas.so.1")]
        internal static extern void evas_map_util_points_color_set(IntPtr map, int r, int g, int b, int a);

        [DllImport("libevas.so.1")]
        internal static extern void evas_object_map_enable_set(IntPtr obj, bool enabled);

        [DllImport("libevas.so.1")]
        internal static extern int evas_map_count_get(IntPtr map);
    }
}
